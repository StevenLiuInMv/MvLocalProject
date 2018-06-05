using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvLocalProject.Model;
using MvLocalProject.Controller;
using System.Collections;
using System.Reflection;
using NCalc;

namespace MvLocalProject.Bo
{
    class MvBomCompareBo
    {
        internal DataSet CollectSourceDsBomP09Process(string[] selectedList)
        {
            DataSet sourceDs = new DataSet();

            sourceDs = MvDbDao.collectData_Bom09(selectedList);
            return sourceDs;
        }

        internal DataSet CollectSourceDsBomP07Process(string[] selectedList)
        {
            DataSet sourceDs = new DataSet();

            sourceDs = MvDbDao.collectData_Bom07(selectedList);
            return sourceDs;
        }


        internal DataSet CollectBomThinDsProcess(string[] bomList)
        {
            DataSet sourceDs = new DataSet();

            sourceDs = MvDbDao.collectData_BomThin(bomList);
            return sourceDs;

        }

        internal int CompareProcess(string[] selectedList, ref DataSet sourceDs, ref DataSet compareAB, ref DataSet compareBA, ref DataTable illegalData, ref DataTable summaryDt)
        {
            // 取得Bom資料
            // 有任何錯誤, 回傳 -1
            sourceDs = CollectSourceDsBomP09Process(selectedList).Copy();
            if (sourceDs.Tables.Count <= 1) { return -1; }

            // 取得比對結果 A -> B
            // 有任何錯誤, 回傳 -2
            compareAB = compareBomByRulePur(sourceDs.Tables[0], sourceDs.Tables[1]);
            if ((compareAB == null) || (compareAB.Tables.Count == 0)) { return -2; }
            illegalData = compareAB.Tables["Illegal"].Copy();

            // 取得比對結果 A -> B
            // 有任何錯誤, 回傳 -3
            compareBA = compareBomByRulePur(sourceDs.Tables[1], sourceDs.Tables[0], true);
            if ((compareBA == null) || (compareBA.Tables.Count == 0)) { return -3; }

            // Merge illegal的talbe 資料
            illegalData.Merge(compareBA.Tables["Illegal"].Copy());

            // 產生summary, 變更料件的部份, 以A->B為主
            summaryDt = generateSummaryTable(compareAB.Tables["Different"], true);
            summaryDt.Merge(generateSummaryTable(compareBA.Tables["Different"]));

            // rename table's column name
            int i = 0;
            foreach (MvHeader header in CompareBom09Header.Values)
            {
                compareAB.Tables["Same"].Columns[i].ColumnName = header.AliasName();
                compareAB.Tables["Different"].Columns[i].ColumnName = header.AliasName();
                compareBA.Tables["Same"].Columns[i].ColumnName = header.AliasName();
                compareBA.Tables["Different"].Columns[i].ColumnName = header.AliasName();
                i++;
            }

            // rename source table and illegal talbe column's name
            i = 0;
            foreach (string header in DefinedHeader.Bom09HeaderAliasNameNoPrice)
            {
                sourceDs.Tables[0].Columns[i].ColumnName = header;
                sourceDs.Tables[1].Columns[i].ColumnName = header;
                illegalData.Columns[i].ColumnName = header;
                i++;
            }

            // rename summary table's column name, and fill ItemNo
            i = 0;
            foreach (DataRow dr in summaryDt.Rows)
            {
                dr["No"] = (i + 1);
                i++;
            }

            i = 0;
            foreach (string header in DefinedHeader.Bom09SummaryHeaderAliasNameNoPrice)
            {
                summaryDt.Columns[i].ColumnName = header;
                i++;
            }

            return 0;
        }

        internal DataSet compareBomByRulePur(DataTable tableA, DataTable tableB, bool isReverseDisplay = false)
        {
            DataSet majorSet = new DataSet("CompareResult");
            DataTable tSame = new DataTable("Same");
            DataTable tDiff = new DataTable("Different");
            DataTable tIllegal = null;

            // 設定table header
            foreach (MvHeader header in CompareBom09Header.Values)
            {
                tSame.Columns.Add(header.Name());
                tDiff.Columns.Add(header.Name());
            }

            // 複製tableA的columns
            tIllegal = tableA.Clone();
            tIllegal.TableName = "Illegal";

            string itemNumA = "";
            int amountA = 0;
            string itemNumB = "";
            int amountB = 0;

            bool isIncludeVersion = false;
            DataRow tempDr = null;

            foreach (DataRow rowA in tableA.Rows)
            {
                // 取得Table A品號
                itemNumA = rowA["A8"].ToString().Trim();
                amountA = Convert.ToInt32(rowA["MD006"]);
                isIncludeVersion = false;
                bool isMatchRule = false;
                // 判斷是否為合法品名
                // 目前沒有長度>12 的品名
                if (itemNumA.Length > 12)
                {
                    tempDr = tIllegal.NewRow();
                    tempDr.ItemArray = rowA.ItemArray.Clone() as object[];
                    tIllegal.Rows.Add(tempDr);
                    continue;
                }
                else if (itemNumA.Length == 11) // 當長度為11碼時, 需額外處理
                {
                    isIncludeVersion = true;
                }

                foreach (DataRow rowB in tableB.Rows)
                {
                    itemNumB = rowB["A8"].ToString().Trim();
                    amountB = Convert.ToInt32(rowB["MD006"]);

                    if ((itemNumA.CompareTo(itemNumB) == 0) && (amountA == amountB))
                    {
                        // 品號及數量均相同, 不分11碼或8-10碼
                        // 將資料填入SameTable
                        tSame.Rows.Add(convertToResultTalbe(tSame, rowA, isReverseDisplay));
                        isMatchRule = true;
                        break;
                    }
                    else if ((itemNumA.CompareTo(itemNumB) == 0) && (amountA != amountB))
                    {
                        // 品號相同, 但數量不相同
                        if (isIncludeVersion == true)
                        {
                            // 11碼
                            tDiff.Rows.Add(convertToResultTalbe(tDiff, rowA, rowB, true, true, false, isReverseDisplay));
                            isMatchRule = true;
                            break;
                        }
                        else
                        {
                            // 8-10碼
                            tDiff.Rows.Add(convertToResultTalbe(tDiff, rowA, rowB, false, true, false, isReverseDisplay));
                            isMatchRule = true;
                            break;
                        }
                    }
                    else if ((itemNumA.CompareTo(itemNumB) != 0) && (amountA == amountB))
                    {
                        // 品號不同, 但數量相同
                        if (isIncludeVersion == true)
                        {
                            // 11碼
                            // 判斷去尾碼後的是否相同
                            string itemNoVerA = itemNumA.Substring(0, itemNumA.Length - 1);
                            string itemNoVerB = itemNumB.Substring(0, itemNumB.Length - 1);

                            if (itemNoVerA.CompareTo(itemNoVerB) == 0)
                            {
                                // 去尾碼後相同, 判斷為品號相同
                                // 此部份算版本變更
                                tDiff.Rows.Add(convertToResultTalbe(tDiff, rowA, rowB, true, true, true, isReverseDisplay));
                                isMatchRule = true;
                                break;
                            }
                            else
                            {
                                // 去尾碼後, 判斷品號版本不同
                                // 此部份不處理
                                continue;
                            }
                        }
                        else
                        {
                            // 8-10 碼
                            // 此部份不處理
                            continue;
                        }
                    }
                }

                if (isMatchRule == false)
                {
                    // 不在上述條件的, 一律為新增
                    tDiff.Rows.Add(convertNewItemToResultTalbe(tDiff, rowA, isReverseDisplay));
                    continue;
                }

                Console.WriteLine(itemNumA);
            }

            majorSet.Tables.Add(tSame);
            majorSet.Tables.Add(tDiff);
            majorSet.Tables.Add(tIllegal);

            return majorSet;
        }

        internal DataTable generateSummaryTable(DataTable sourceDt, bool isIncludeChange = false)
        {
            DataTable dt = new DataTable();

            // 定義 column
            foreach (string name in DefinedHeader.Bom09SummaryHeaderNameNoPrice)
            {
                dt.Columns.Add(name);
            }
            foreach (DataRow dr in sourceDt.Rows)
            {
                DataRow newDr = dt.NewRow();
                if (dr["CompareA8"].ToString().Equals("新增料件"))
                {
                    // 新增料件
                    newDr["Add"] = "V";
                    newDr["CompareA8"] = "N/A";
                    newDr["A8"] = dr["A8"];
                    newDr["Column4"] = dr["Column4"];
                    newDr["OrgMD006"] = "--";
                    newDr["MD006"] = dr["MD006"];
                }
                else if (dr["CompareA8"].ToString().Equals("刪除料件"))
                {
                    // 刪除料件
                    newDr["Delete"] = "V";
                    newDr["CompareA8"] = dr["A8"];
                    newDr["A8"] = "N/A";
                    newDr["Column4"] = dr["Column4"];
                    newDr["OrgMD006"] = dr["MD006"];
                    newDr["MD006"] = "--";
                }
                else
                {
                    // 變更料件
                    // 判斷是否需要新增變更料件資料
                    if (isIncludeChange == false)
                    {
                        // 如果不include Change, 直接continue
                        continue;
                    }
                    newDr["Change"] = "V";
                    newDr["CompareA8"] = dr["A8"];
                    newDr["A8"] = dr["CompareA8"];
                    newDr["Column4"] = dr["Column4"];
                    newDr["OrgMD006"] = dr["CompareMD006"];

                    string finalMD006 = dr["CompareMD006"].ToString();
                    int md006Index = finalMD006.IndexOf("-->");
                    if (md006Index < 0)
                    {
                        // 版本相同
                        newDr["MD006"] = dr["MD006"];
                    }
                    else
                    {
                        finalMD006 = finalMD006.Substring(md006Index + 3);
                        newDr["MD006"] = finalMD006;
                    }
                }
                dt.Rows.Add(newDr);
            }
            return dt;
        }

        private DataRow convertToResultTalbe(DataTable targetTable, DataRow source, bool isReverseDisplay)
        {
            return convertToResultTalbe(targetTable, source, source, false, true, true, isReverseDisplay);
        }

        private DataRow convertNewItemToResultTalbe(DataTable targetTable, DataRow source, bool isReverseDisplay)
        {
            return convertToResultTalbe(targetTable, source, source, false, false, false, isReverseDisplay);
        }

        private DataRow convertToResultTalbe(DataTable targetTable, DataRow source, DataRow target, bool isIncludeVer, bool isSameItemNum, bool isSameAmount, bool isReverseDisplay)
        {
            DataRow dr = targetTable.NewRow();

            // 先填入輔助資訊
            dr["LV"] = "L" + source["LV"].ToString().Replace(".", "");
            dr["Column4"] = source["Column4"];
            dr["Column8"] = source["Column8"];
            dr["MB003"] = source["MB003"];
            dr["MB004"] = source["MB004"];

            // 版本變更, 11碼不同, 但10碼相同
            string verSrc = source["A8"].ToString().Trim();
            string verTar = target["A8"].ToString().Trim();
            string displayPattern = (isReverseDisplay == true) ? "{1}<--{0}" : "{0}-->{1}";

            if (isSameItemNum == true && isSameAmount == true)
            {
                // 考慮品號是否為11碼
                dr["CompareLV"] = "--";
                if (isIncludeVer == true)
                {
                    // 版本變更, 11碼不同, 但10碼相同
                    dr["CompareA8"] = string.Format(displayPattern, verSrc.Substring(verSrc.Length - 1), verTar.Substring(verTar.Length - 1));
                }
                else
                {
                    // 完全相同, 8-10碼相同
                    dr["CompareA8"] = "--";
                }

                dr["CompareMD006"] = "--";
                dr["A8"] = source["A8"];
                dr["MD006"] = source["MD006"];
            }
            else if (isSameItemNum == true)
            {
                // 數量變更
                dr["CompareLV"] = "--";
                dr["CompareA8"] = "--";
                dr["CompareMD006"] = string.Format(displayPattern, source["MD006"], target["MD006"]);
                dr["A8"] = source["A8"];
                dr["MD006"] = source["MD006"];
            }
            else if (isSameAmount == true)
            {
                // 版本變更
                dr["CompareLV"] = "--";
                dr["CompareA8"] = string.Format(displayPattern, verSrc.Substring(verSrc.Length - 1), verTar.Substring(verTar.Length - 1));
                dr["CompareMD006"] = "--";
                dr["A8"] = source["A8"];
                dr["MD006"] = source["MD006"];
            }
            else
            {
                if (isIncludeVer == true)
                {
                    // 品號及數量均有變更
                    dr["CompareLV"] = "--";
                    dr["CompareA8"] = string.Format(displayPattern, verSrc.Substring(verSrc.Length - 1), verTar.Substring(verTar.Length - 1));
                    dr["CompareMD006"] = string.Format(displayPattern, source["MD006"], target["MD006"]);
                    dr["A8"] = source["A8"];
                    dr["MD006"] = source["MD006"];
                }
                else
                {
                    // New Item
                    dr["CompareLV"] = "--";
                    dr["CompareA8"] = string.Format("{0}料件", isReverseDisplay == true ? "刪除" : "新增");
                    dr["CompareMD006"] = "--";
                    dr["A8"] = source["A8"];
                    dr["MD006"] = source["MD006"];
                }
            }
            return dr;
        }

        public void filterBomP09ColumnByRdRule(ref DataTable sourceDt)
        {
            sourceDt.Columns.Remove("SUBPN");
            sourceDt.Columns.Remove("Column1");
            sourceDt.Columns.Remove("Column2");
            sourceDt.Columns.Remove("Column3");
            sourceDt.Columns.Remove("Column5");
            sourceDt.Columns.Remove("Column6");
            sourceDt.Columns.Remove("Column7");
            sourceDt.Columns.Remove("Column8");
            sourceDt.Columns.Remove("Column10");
            sourceDt.Columns.Remove("Column11");
            sourceDt.Columns.Remove("MB003");
            sourceDt.Columns.Remove("MD016X");
            sourceDt.Columns.Remove("YN");
            sourceDt.Columns.Remove("CNC");
            sourceDt.Columns.Remove("YEAR3");
            sourceDt.Columns.Remove("FOREIGN_YN");
            sourceDt.Columns.Remove("ONLY_ONE");
            sourceDt.Columns.Remove("APP_CODE");
        }

        public void filterBomP07ColumnByRdRule(ref DataTable sourceDt)
        {
            sourceDt.Columns.Remove("SUBPN");
            sourceDt.Columns.Remove("Column1");
            sourceDt.Columns.Remove("Column2");
            sourceDt.Columns.Remove("Column3");
            sourceDt.Columns.Remove("Column5");
            sourceDt.Columns.Remove("Column6");
            sourceDt.Columns.Remove("Column7");
            sourceDt.Columns.Remove("Column8");
            sourceDt.Columns.Remove("Column10");
            sourceDt.Columns.Remove("Column11");
            sourceDt.Columns.Remove("Column12");
            sourceDt.Columns.Remove("MB003");
            sourceDt.Columns.Remove("YN");
            sourceDt.Columns.Remove("MB037");
            sourceDt.Columns.Remove("MB209");
            sourceDt.Columns.Remove("MB077");
            sourceDt.Columns.Remove("MD011X");
            sourceDt.Columns.Remove("MD013X");
            sourceDt.Columns.Remove("FOREIGN_YN");
            sourceDt.Columns.Remove("ONLY_ONE");
        }

        public DataTable filterDataByRdRule(DataTable sourceDt, bool removeAmountIsZero)
        {
            DataTable dt = sourceDt.Copy();
            string[] buyTypeList = new string[7];
            string parentBuyType = string.Empty;
            string nowBuyType = string.Empty;
            string strNowLv = string.Empty;
            float amount = float.MinValue;
            int preLv = 0;
            int nowLv = 1;

            for (int i = 0; i < 7; i++)
            {
                buyTypeList[i] = string.Empty;
            }

            Hashtable hash = new Hashtable();
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                strNowLv = dr["LV"].ToString().Trim().Replace(".", "");
                nowLv = int.Parse(strNowLv);
                nowBuyType = dr["MB025X"].ToString().Trim();
                amount = float.Parse(dr["MD006"].ToString());
                string preBuyType = string.Empty;   // preBuyType跟階層有關, 後續判斷再取值

                if (nowLv == 1) // Lv1
                {
                    // 代表在root層, 要換掉buyType
                    buyTypeList[preLv] = nowBuyType;
                    if (nowBuyType.Equals("虛設"))
                    {
                        // 當層是虛設, 移除該列
                        hash.Add(index, true);
                    }
                }
                else if (preLv < nowLv) // 1 -> 2
                {
                    // 判斷上一層是否為再製 or 虛設 or ""
                    preBuyType = buyTypeList[preLv];
                    buyTypeList[nowLv] = nowBuyType;

                    if (preBuyType.Equals("再製") == true || preBuyType.Equals("虛設") == true || preBuyType.Equals("再製件") == true)
                    {
                        // 上層是再製或虛設, 該列移除
                        hash.Add(index, true);
                    }
                    else if (nowBuyType.Equals("虛設"))
                    {
                        // 當層是虛設, 移除該列
                        hash.Add(index, true);
                    }
                    else
                    {
                        // 當層不是再製或虛設, 保留
                    }
                }
                else if (preLv == nowLv)
                {
                    preBuyType = buyTypeList[nowLv - 1];
                    buyTypeList[nowLv] = nowBuyType;

                    if (preBuyType.Length == 0)
                    {
                        // 代表在root層, 要換掉buyType
                        buyTypeList[preLv] = nowBuyType;
                    }
                    else if (nowBuyType.Equals("虛設"))
                    {
                        // 當層是虛設, 移除該列
                        hash.Add(index, true);
                    }
                    else if (nowBuyType.Equals("再製") || nowBuyType.Equals("再製件"))
                    {
                        // 當層是再製, 保留
                    }
                    else if (preBuyType.Equals("再製") || nowBuyType.Equals("虛設") || preBuyType.Equals("再製件"))
                    {
                        // 上層是再製或虛設, 移除該列
                        hash.Add(index, true);
                    }
                    else
                    {
                        // 當層不是再製或虛設, 保留
                    }

                }
                else if (preLv > nowLv) // 2 -> 1
                {
                    // preBuyType 要取now的上一層
                    preBuyType = buyTypeList[nowLv - 1];
                    buyTypeList[nowLv] = nowBuyType;
                    // 因為已跳回上層, child層資料直接清空
                    buyTypeList[preLv] = string.Empty;

                    if (preBuyType.Length == 0)
                    {
                        // 在root層, 要換掉buyType
                        buyTypeList[nowLv - 1] = nowBuyType;
                    }
                    else if (preBuyType.Equals("再製") || preBuyType.Equals("虛設") || preBuyType.Equals("再製件"))
                    {
                        // 在上二層是再製或虛設, 該列移除
                        hash.Add(index, true);
                    }
                    else if (nowBuyType.Equals("虛設"))
                    {
                        // 當層是虛設, 移除該列
                        hash.Add(index, true);
                    }
                    else
                    {
                        // 當層不是再製或虛設, 保留
                    }
                }

                // 加判, 如果amount為0, 直接移除
                if (removeAmountIsZero == true && amount == 0)
                {
                    // 如果該值已經被設定, 不用再重覆放至hash table內
                    try
                    {
                        hash.Add(index, true);
                    }
                    catch (ArgumentException)
                    {

                    }

                }
                preLv = nowLv;
                index++;
            }

            for (index = dt.Rows.Count; index >= 0; index--)
            {
                if (hash.Contains(index) == true)
                {
                    dt.Rows.RemoveAt(index);
                }
            }

            return dt;
        }
        /// <summary>
        /// 擴增Space欄位
        /// 擴增的條件是將每個經過的node記錄起來, 建制出node展開路徑
        /// </summary>
        /// <remarks>
        /// 當品號屬性為"選配"時, 在計錄AmountSpace的部份不可列入計算
        /// </remarks>
        /// <param name="sourceBom"></param>
        /// <param name="isIncludeLv1ModuleName"></param>
        /// <returns>DataTable</returns>
        public DataTable extendBomNameSpace(DataTable sourceBom, bool isIncludeLv1ModuleName = false)
        {
            DataTable resultDt = sourceBom.Copy();
            resultDt.Columns.Add("NameSpace");
            resultDt.Columns.Add("NameSpaceNoVer");
            resultDt.Columns.Add("AmountSpace");

            if (isIncludeLv1ModuleName == true)
            {
                resultDt.Columns.Add("ModuleLv1");
            }

            string[] nameSpaceList = new string[7];
            string[] nameSpaceListNoVer = new string[7];
            string[] amountSpaceList = new string[7];

            int preLv = -1;
            int nowLv = -1;
            string parentName = string.Empty;
            string parentNameNoVer = string.Empty;
            string nowName = string.Empty;
            string nowNameNoVer = string.Empty;
            string nowNameVer = string.Empty;
            string nowNameSpace = string.Empty;
            string nowNameSpaceNoVer = string.Empty;
            string nowBuyType = string.Empty;


            string nowAmount = string.Empty;
            string parentAmount = string.Empty;

            // initial nameSpaceList
            nowLv = 0;
            foreach (string name in nameSpaceList)
            {
                nameSpaceList[nowLv] = string.Empty;
                nameSpaceListNoVer[nowLv] = string.Empty;
                amountSpaceList[nowLv] = string.Empty;
                nowLv++;
            }

            foreach (DataRow dr in resultDt.Rows)
            {

                nowLv = int.Parse(dr["LV"].ToString().Trim().Replace(".", ""));
                nowName = dr["A8"].ToString().Trim();
                nowAmount = dr["MD006"].ToString().Trim();
                nowBuyType = dr["MB025X"].ToString().Trim();

                if (nowName.Equals("250E05ZC12A"))
                {
                    Console.WriteLine("debug");
                }


                if (nowName.Length > 10)
                {
                    nowNameNoVer = nowName.Substring(0, nowName.Length - 1);
                }
                else
                {
                    nowNameNoVer = nowName;
                }


                if (nowLv == 1)
                {
                    // 代表在root層 ,沒有parent
                    preLv = 0;
                    parentName = string.Empty;
                    parentNameNoVer = string.Empty;
                    nameSpaceList[preLv] = string.Empty;
                    nameSpaceListNoVer[preLv] = string.Empty;
                    parentAmount = string.Empty;
                    amountSpaceList[preLv] = string.Empty;
                    nameSpaceList[nowLv] = nowName;
                    nameSpaceListNoVer[nowLv] = nowNameNoVer;
                    amountSpaceList[nowLv] = nowAmount;
                    dr["NameSpace"] = nowName;
                    dr["NameSpaceNoVer"] = nowNameNoVer;
                    dr["AmountSpace"] = nowAmount;
                }
                else if (nowLv > preLv)
                {
                    // 1 -> 2
                    // 長度 >10 要去除版本
                    parentName = nameSpaceList[preLv];
                    parentNameNoVer = nameSpaceListNoVer[preLv];
                    parentAmount = amountSpaceList[preLv];
                    nowNameSpace = string.Format("{0}.{1}", parentName, nowName);
                    nowNameSpaceNoVer = string.Format("{0}.{1}", parentNameNoVer, nowNameNoVer);
                    nowAmount = string.Format("{0}*{1}", parentAmount, nowAmount);
                    dr["NameSpace"] = nowNameSpace;
                    dr["NameSpaceNoVer"] = nowNameSpaceNoVer;
                    dr["AmountSpace"] = nowAmount;

                    nameSpaceList[nowLv] = nowNameSpace;
                    nameSpaceListNoVer[nowLv] = nowNameSpaceNoVer;
                    amountSpaceList[nowLv] = nowAmount;
                }
                else if (nowLv == preLv)
                {
                    // 2 -> 2
                    parentName = nameSpaceList[nowLv - 1];
                    parentNameNoVer = nameSpaceListNoVer[nowLv - 1];
                    parentAmount = amountSpaceList[nowLv - 1];
                    nowNameSpace = string.Format("{0}.{1}", parentName, nowName);
                    nowNameSpaceNoVer = string.Format("{0}.{1}", parentNameNoVer, nowNameNoVer);
                    nowAmount = string.Format("{0}*{1}", parentAmount, nowAmount);
                    dr["NameSpace"] = nowNameSpace;
                    dr["NameSpaceNoVer"] = nowNameSpaceNoVer;
                    dr["AmountSpace"] = nowAmount;
                    
                    nameSpaceList[nowLv] = nowNameSpace;
                    nameSpaceListNoVer[nowLv] = nowNameSpaceNoVer;
                    amountSpaceList[nowLv] = nowAmount;
                }
                else if (nowLv < preLv)
                {
                    // 2 -> 1
                    parentName = nameSpaceList[nowLv - 1];
                    parentNameNoVer = nameSpaceListNoVer[nowLv - 1];
                    parentAmount = amountSpaceList[nowLv - 1];
                    nowNameSpace = string.Format("{0}.{1}", parentName, nowName);
                    nowNameSpaceNoVer = string.Format("{0}.{1}", parentNameNoVer, nowNameNoVer);
                    nowAmount = string.Format("{0}*{1}", parentAmount, nowAmount);
                    dr["NameSpace"] = nowNameSpace;
                    dr["NameSpaceNoVer"] = nowNameSpaceNoVer;
                    dr["AmountSpace"] = nowAmount;

                    nameSpaceList[nowLv] = nowNameSpace;
                    nameSpaceListNoVer[nowLv] = nowNameSpaceNoVer;
                    amountSpaceList[nowLv] = nowAmount;
                    nameSpaceList[preLv] = string.Empty;
                    nameSpaceListNoVer[preLv] = string.Empty;
                    amountSpaceList[preLv] = string.Empty;
                }
                // fill ModuleLv1
                if (isIncludeLv1ModuleName == true)
                {
                    if (nowLv == 1)
                    {
                        dr["ModuleLv1"] = sourceBom.TableName;
                    }
                    else
                    {
                        int findDotIndex = nowNameSpace.IndexOf(".");
                        dr["ModuleLv1"] = nowNameSpace.Substring(0, findDotIndex);
                    }
                }
                preLv = nowLv;
            }
            return resultDt;
        }

        internal DataSet compareBomByRuleRd_v2(DataTable tableA, DataTable tableB, bool isOnlyRturnDiffNewItem)
        {
            DataSet majorSet = new DataSet("CompareResult");
            DataTable tSame = new DataTable("Same");
            DataTable tDiff = new DataTable("Different");
            DataTable findResult = new DataTable("findResult");

            Hashtable hashUsedSortedTableB = new Hashtable();
            Hashtable cacheFindResult = new Hashtable();

            DataTable sortedTableA = tableA.Clone();
            DataTable sortedTableB = tableB.Clone();

            // 設定table header
            foreach (string header in DefinedHeader.BomCompareHeaderName)
            {
                tSame.Columns.Add(header);
                tDiff.Columns.Add(header);
            }

            // 複製tableA的columns
            findResult = tableA.Clone();

            string itemNameA = string.Empty;
            string itemNameANoVer = string.Empty;
            string itemNameASpaceNoVer = string.Empty;
            int amountA = 0;
            int itemLvA = 0;
            string itemNameB = string.Empty;
            string itemNameBNoVer = string.Empty;
            string itemNameBSpaceNoVer = string.Empty;
            int amountB = 0;
            int itemLvB = 0;
            string tmpNameSpace = string.Empty;
            int tmpFindResultIndex = 0;

            bool isIncludeVersion = false;
            DataRow tempDr = null;

            // 把TableA / TableB由最大LV排至最小LV
            for (int lv = DefinedHeader.Bom09MaxLv; lv >= DefinedHeader.Bom09MinLv; lv--)
            {
                foreach (DataRow dr in tableA.Rows)
                {
                    itemLvA = int.Parse(dr["LV"].ToString().Trim().Replace(".", ""));
                    if (itemLvA == lv)
                    {
                        sortedTableA.Rows.Add(dr.ItemArray);
                    }
                }
            }

            for (int lv = DefinedHeader.Bom09MaxLv; lv >= DefinedHeader.Bom09MinLv; lv--)
            {
                foreach (DataRow dr in tableB.Rows)
                {
                    itemLvB = int.Parse(dr["LV"].ToString().Trim().Replace(".", ""));
                    if (itemLvB == lv)
                    {
                        sortedTableB.Rows.Add(dr.ItemArray);
                    }
                }
            }

            for (int indexRowA = 0; indexRowA < sortedTableA.Rows.Count; indexRowA++)
            {
                DataRow rowA = sortedTableA.Rows[indexRowA];
                // 取得Table A品號
                itemNameA = rowA["A8"].ToString().Trim();
                itemNameANoVer = itemNameA;
                itemNameASpaceNoVer = rowA["NameSpaceNoVer"].ToString();
                amountA = Convert.ToInt32(rowA["MD006"]);
                itemLvA = int.Parse(rowA["LV"].ToString().Trim().Replace(".", ""));
                tempDr = null;

                if (itemNameA.Equals("2211205014"))
                {
                    Console.WriteLine("debug1");
                }

                isIncludeVersion = false;
                //bool isMatchSearch = false;
                // 判斷是否為合法品名
                // 目前沒有長度>12 的品名, 直接跳過
                if (itemNameA.Length > 12)
                {
                    continue;
                }
                else if (itemNameA.Length == 11) // 當長度為11碼時, 需額外處理
                {
                    itemNameANoVer = itemNameANoVer.Substring(0, itemNameANoVer.Length - 1);
                    isIncludeVersion = true;
                }

                // 先組出要尋找的name space
                if (itemLvA <= 2)
                {
                    tmpNameSpace = itemNameASpaceNoVer;
                }
                else
                {
                    // 當LV >2, 往後取最後兩層當比對的Namespace
                    tmpNameSpace = itemNameASpaceNoVer.Substring(itemNameASpaceNoVer.Substring(0, itemNameASpaceNoVer.LastIndexOf(".")).LastIndexOf(".") + 1);
                }

                tmpFindResultIndex = 0;
                findResult.Clear();
                cacheFindResult.Clear();
                for (int hashIndex = 0; hashIndex < tableB.Rows.Count; hashIndex++)
                {
                    DataRow rowB = tableB.Rows[hashIndex];
                    itemNameBSpaceNoVer = rowB["NameSpaceNoVer"].ToString();
                    // 不符合的, 跳過
                    if (itemNameBSpaceNoVer.LastIndexOf(tmpNameSpace) < 0)
                    {
                        continue;
                    }
                    // 已被使用過的, 跳過
                    if (hashUsedSortedTableB.Contains(hashIndex) == true)
                    {
                        continue;
                    }
                    findResult.Rows.Add(rowB.ItemArray);
                    cacheFindResult.Add(tmpFindResultIndex, hashIndex);
                    tmpFindResultIndex++;
                }

                if (findResult.Rows.Count == 0)
                {
                    // 代表沒有找到符合的, 直接視為新增或刪除
                    tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, rowA, false, false, false, false, isOnlyRturnDiffNewItem));
                    continue;
                }

                // 代表有找到符合的, 開始交差比對
                // 1. ItemA 去找是否存在 ItemB集合內
                List<string> tmpSplitList = itemNameASpaceNoVer.Split('.').ToList<string>();
                string searchPattern = itemNameASpaceNoVer;
                bool hasFindMatchRow = false;
                for (int i = 0; i < tmpSplitList.Count; i++)
                {
                    hasFindMatchRow = false;
                    if (i != 0)
                    {
                        // 由前頭逐項遞減
                        searchPattern = searchPattern.Replace(tmpSplitList[i - 1] + ".", "");
                    }
                    // 已找出searchPattern, 開始比對
                    //tmpFindResultIndex
                    for (int findIndex = 0; findIndex < findResult.Rows.Count; findIndex++)
                    {
                        DataRow dr = findResult.Rows[findIndex];
                        itemNameB = dr["A8"].ToString().Trim();
                        itemNameBNoVer = itemNameB;
                        itemNameBSpaceNoVer = dr["NameSpaceNoVer"].ToString();
                        amountB = Convert.ToInt32(dr["MD006"]);
                        itemLvB = int.Parse(dr["LV"].ToString().Trim().Replace(".", ""));

                        if (itemNameB.Equals("290E05LK08"))
                        {
                            Console.WriteLine("debug1");
                        }


                        // 最後一定會找到的
                        if (itemNameBSpaceNoVer.Equals(searchPattern) == false)
                        {
                            continue;
                        }
                        // 找到了, 記錄該row已被使用過並跳離
                        tmpFindResultIndex = (int)cacheFindResult[findIndex];
                        hashUsedSortedTableB[tmpFindResultIndex] = true;
                        hasFindMatchRow = true;
                        tempDr = dr;
                        break;
                    }
                    if (hasFindMatchRow == true)
                    {
                        break;
                    }
                }

                // 2. 由 ItemA -> ItemB找不到, 才再用FindResult內的所有Item逐項減排去找是否有Match ItemA的項目
                if (hasFindMatchRow == false)
                {
                    searchPattern = itemNameASpaceNoVer;
                    for (int findIndex = 0; findIndex < findResult.Rows.Count; findIndex++)
                    {
                        DataRow dr = findResult.Rows[findIndex];
                        itemNameB = dr["A8"].ToString().Trim();
                        itemNameBNoVer = itemNameB;
                        itemNameBSpaceNoVer = dr["NameSpaceNoVer"].ToString();
                        amountB = Convert.ToInt32(dr["MD006"]);
                        itemLvB = int.Parse(dr["LV"].ToString().Trim().Replace(".", ""));

                        // 最後一定會找到的
                        if (itemNameBSpaceNoVer.IndexOf(searchPattern) < 0)
                        {
                            continue;
                        }
                        // 找到了, 記錄該row已被使用過並跳離
                        tmpFindResultIndex = (int)cacheFindResult[findIndex];
                        hashUsedSortedTableB[tmpFindResultIndex] = true;
                        hasFindMatchRow = true;
                        tempDr = dr;
                        break;
                    }
                }

                //
                // 當交差比對完, 還是沒找到
                // 這個要另外判斷
                if (hasFindMatchRow == false)
                {
                    // 先判斷料件長度是否>11碼, 如果沒有大於11碼, 含版本, 就視為相異
                    if (isIncludeVersion == false)
                    {
                        // 真的不一樣, 視為新增
                    }
                    else
                    {
                        throw new Exception(string.Format("Cant' find the matches {0}RowA name space : {1}{0}RowB name space : {2}", Environment.NewLine, itemNameASpaceNoVer, itemNameBSpaceNoVer));
                    }
                }

                // 因為沒有版本, 所以用完全相符來比對
                // 由多至少逐項比對, 取到的就離開
                Console.WriteLine("itemNameBSpaceNoVer = {0}", itemNameBSpaceNoVer);

                bool tmpAmount = (amountA == amountB);
                bool tmpLv = (itemLvA == itemLvB);

                // 看是否還需要比對版本
                if (isIncludeVersion == false)
                {
                    // 8-10碼 不需要再比對
                    if (itemNameB.Equals(itemNameA) == true && tmpLv == true && tmpAmount == true)
                    {
                        // 如果沒有找到的話, 視為新增或刪除
                        if (hasFindMatchRow == true)
                        {
                            tSame.Rows.Add(convertNewItemToResultTalbeByRd(tSame, rowA, rowA, false, tmpLv, true, tmpAmount, isOnlyRturnDiffNewItem));
                        }
                        else
                        {
                            tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, rowA, false, false, false, false, isOnlyRturnDiffNewItem));
                        }
                    }
                    else
                    {
                        if (isOnlyRturnDiffNewItem == false)
                        {
                            tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, tempDr, isIncludeVersion, tmpLv, true, tmpAmount, isOnlyRturnDiffNewItem));
                        }
                    }
                }
                else
                {
                    // 11碼, 取版本再出來比對
                    if (itemNameB.Equals(itemNameA) == true && tmpLv == true && tmpAmount == true)
                    {
                        tSame.Rows.Add(convertNewItemToResultTalbeByRd(tSame, rowA, rowA, false, tmpLv, true, tmpAmount, isOnlyRturnDiffNewItem));
                    }
                    else if (itemNameB.Equals(itemNameA) == true)
                    {
                        if (isOnlyRturnDiffNewItem == false)
                        {
                            tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, tempDr, isIncludeVersion, tmpLv, true, tmpAmount, isOnlyRturnDiffNewItem));
                        }
                    }
                    else
                    {
                        if (isOnlyRturnDiffNewItem == false)
                        {
                            tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, tempDr, isIncludeVersion, tmpLv, false, tmpAmount, isOnlyRturnDiffNewItem));
                        }
                    }
                }
            }

            // 還有一段還沒有寫
            // 當模組的數量有變的時候, 所有的child都要視為變動

            majorSet.Tables.Add(tSame);
            majorSet.Tables.Add(tDiff);

            return majorSet;

        }

        internal DataSet compareBomByRuleRd_v1(DataTable tableA, DataTable tableB, bool isDefineAdd)
        {
            DataSet majorSet = new DataSet("CompareResult");
            DataTable tSame = new DataTable("Same");
            DataTable tDiff = new DataTable("Different");

            Hashtable hashUsedTableB = new Hashtable();

            // 設定table header
            foreach (string header in DefinedHeader.BomCompareHeaderName)
            {
                tSame.Columns.Add(header);
                tDiff.Columns.Add(header);
            }

            // 複製tableA的columns

            string itemNameA = string.Empty;
            string itemNameANoVer = string.Empty;
            string itemNameASpaceNoVer = string.Empty;
            int amountA = 0;
            int itemLvA = 0;
            string itemNameB = string.Empty;
            string itemNameBNoVer = string.Empty;
            string itemNameBSpaceNoVer = string.Empty;
            int amountB = 0;
            int itemLvB = 0;
            string tmpNameSpace = string.Empty;

            bool isIncludeVersion = false;
            DataRow tempDr = null;

            foreach (DataRow rowA in tableA.Rows)
            {
                // 取得Table A品號
                itemNameA = rowA["A8"].ToString().Trim();
                itemNameANoVer = itemNameA;
                itemNameASpaceNoVer = rowA["NameSpaceNoVer"].ToString();
                amountA = Convert.ToInt32(rowA["MD006"]);
                itemLvA = int.Parse(rowA["LV"].ToString().Trim().Replace(".", ""));


                if (itemNameA.Equals("250E05BC67B"))
                {
                    Console.WriteLine("debug1");
                }

                isIncludeVersion = false;
                bool isMatchSearch = false;
                // 判斷是否為合法品名
                // 目前沒有長度>12 的品名, 直接跳過
                if (itemNameA.Length > 12)
                {
                    continue;
                }
                else if (itemNameA.Length == 11) // 當長度為11碼時, 需額外處理
                {
                    itemNameANoVer = itemNameANoVer.Substring(0, itemNameANoVer.Length - 1);
                    isIncludeVersion = true;
                }


                // 先組出要尋找的name space
                if (itemLvA <= 2)
                {
                    tmpNameSpace = itemNameASpaceNoVer;
                }
                else
                {
                    // 當LV > 2, 往後取最後兩層當比對的Namespace
                    tmpNameSpace = itemNameASpaceNoVer.Substring(itemNameASpaceNoVer.Substring(0, itemNameASpaceNoVer.LastIndexOf(".")).LastIndexOf(".") + 1);
                }

                for (int hashIndex = 0; hashIndex < tableB.Rows.Count; hashIndex++)
                {
                    DataRow rowB = tableB.Rows[hashIndex];

                    // 已被使用過的, 直接跳過
                    if (hashUsedTableB.Contains(hashIndex) == true)
                    {
                        continue;
                    }

                    itemNameB = rowB["A8"].ToString().Trim();
                    itemNameBNoVer = itemNameB;
                    itemNameBSpaceNoVer = rowB["NameSpaceNoVer"].ToString();
                    amountB = Convert.ToInt32(rowB["MD006"]);
                    itemLvB = int.Parse(rowB["LV"].ToString().Trim().Replace(".", ""));

                    if (itemNameB.Equals("250E05BC67C"))
                    {
                        Console.WriteLine("debug1");
                    }


                    if (itemNameB.Length > 12)
                    {
                        continue;
                    }
                    else if (itemNameB.Length == 11) // 當長度為11碼時, 需額外處理
                    {
                        itemNameBNoVer = itemNameBNoVer.Substring(0, itemNameBNoVer.Length - 1);
                    }

                    // 比對是否存在於itemB的namespace內
                    if (itemNameBSpaceNoVer.LastIndexOf(tmpNameSpace) < 0)
                    {
                        continue;
                    }

                    // 先用ItemA品名長度決定是完全比對, 或是比對前10碼
                    if (isIncludeVersion == false)
                    {
                        // 品名為8-10碼, 完全比對
                        if (itemNameA.Equals(itemNameB) == false)
                        {
                            // 不符合, 繼續下一筆
                            continue;
                        }

                        bool tmpAmount = (amountA == amountB);
                        bool tmpLv = (itemLvA == itemLvB);

                        if (tmpAmount == true && tmpLv == true)
                        {
                            // 代表資料完全一樣
                            // 比對上一層是否相同, 如果也相同, 才視為完全相同
                            if (itemNameASpaceNoVer.Equals(itemNameBSpaceNoVer))
                            {
                                tempDr = convertNewItemToResultTalbeByRd(tSame, rowA, rowB, isIncludeVersion, true, true, true, isDefineAdd);
                                tSame.Rows.Add(tempDr);
                                // 已被使用過的要記錄
                                hashUsedTableB.Add(hashIndex, true);
                                isMatchSearch = true;
                                break;
                            }
                            else
                            {
                                // 比對上一層不相同, 不是同一個模組, 繼續搜尋
                                continue;
                            }
                        }

                        if (itemNameASpaceNoVer.Equals(itemNameBSpaceNoVer))
                        {
                            tempDr = convertNewItemToResultTalbeByRd(tSame, rowA, rowB, isIncludeVersion, true, true, true, isDefineAdd);
                            tSame.Rows.Add(tempDr);
                            // 已被使用過的要記錄
                            hashUsedTableB.Add(hashIndex, true);
                            isMatchSearch = true;
                            break;
                        }
                        tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, true, tmpAmount, isDefineAdd);
                        tDiff.Rows.Add(tempDr);
                        // 已被使用的要記錄
                        hashUsedTableB.Add(hashIndex, true);
                        isMatchSearch = true;
                        break;
                    }
                    else
                    {
                        // 品為為11碼, 含完全比對及模糊比對
                        // 先用完全比對
                        bool tmpItem = itemNameA.Equals(itemNameB);
                        int tmpItemNoVer = itemNameANoVer.CompareTo(itemNameB);
                        if (tmpItem == false && tmpItemNoVer != 0)
                        {
                            // 不符合, 繼續下一筆
                            continue;
                        }
                        bool tmpAmount = (amountA == amountB);
                        bool tmpLv = (itemLvA == itemLvB);
                        if (tmpItem == true && tmpAmount == true && tmpLv == true)
                        {
                            // 完全比對有符合項目, 含數量及LV均相同
                            tempDr = convertNewItemToResultTalbeByRd(tSame, rowA, rowB, false, true, true, true, isDefineAdd);
                            tSame.Rows.Add(tempDr);
                        }
                        else if (tmpItem == true)
                        {
                            // 完全比對有符合項目, 但數量或LV不相同
                            tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, true, tmpAmount, isDefineAdd);
                            tDiff.Rows.Add(tempDr);
                        }
                        else if (tmpItemNoVer == 0)
                        {
                            // 前10碼比對有相符項目
                            tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, false, tmpAmount, isDefineAdd);
                            tDiff.Rows.Add(tempDr);
                        }
                        // 已被使用的要記錄
                        hashUsedTableB.Add(hashIndex, true);
                        isMatchSearch = true;
                        break;
                    }
                }

                // 不在上述條件, 才會走到此流程
                if (isMatchSearch == false)
                {
                    // 不在上述條件的, 一律為新增
                    tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, rowA, false, false, false, false, isDefineAdd));
                    continue;
                }

            }

            majorSet.Tables.Add(tSame);
            majorSet.Tables.Add(tDiff);

            return majorSet;

        }

        internal DataSet compareBomByRuleRd_Org(DataTable tableA, DataTable tableB, bool isDefineAdd)
        {
            DataSet majorSet = new DataSet("CompareResult");
            DataTable tSame = new DataTable("Same");
            DataTable tDiff = new DataTable("Different");

            Hashtable hashUsedTableB = new Hashtable();

            // 設定table header
            foreach (string header in DefinedHeader.BomCompareHeaderName)
            {
                tSame.Columns.Add(header);
                tDiff.Columns.Add(header);
            }

            // 複製tableA的columns

            string itemNameA = string.Empty;
            string itemNameANoVer = string.Empty;
            int amountA = 0;
            int itemLvA = 0;
            string itemNameB = string.Empty;
            string itemNameBNoVer = string.Empty;
            int amountB = 0;
            int itemLvB = 0;

            bool isIncludeVersion = false;
            DataRow tempDr = null;

            foreach (DataRow rowA in tableA.Rows)
            {
                // 取得Table A品號
                itemNameA = rowA["A8"].ToString().Trim();
                itemNameANoVer = itemNameA;
                amountA = Convert.ToInt32(rowA["MD006"]);
                itemLvA = int.Parse(rowA["LV"].ToString().Trim().Replace(".", ""));


                if (itemNameA.Equals("250E05BC40C"))
                {
                    Console.WriteLine("debug1");
                }

                isIncludeVersion = false;
                bool isMatchSearch = false;
                // 判斷是否為合法品名
                // 目前沒有長度>12 的品名, 直接跳過
                if (itemNameA.Length > 12)
                {
                    continue;
                }
                else if (itemNameA.Length == 11) // 當長度為11碼時, 需額外處理
                {
                    itemNameANoVer = itemNameANoVer.Substring(0, itemNameANoVer.Length - 1);
                    isIncludeVersion = true;
                }

                for (int hashIndex = 0; hashIndex < tableB.Rows.Count; hashIndex++)
                {
                    DataRow rowB = tableB.Rows[hashIndex];

                    // 已被使用過的, 直接跳過
                    if (hashUsedTableB.Contains(hashIndex) == true)
                    {
                        continue;
                    }

                    itemNameB = rowB["A8"].ToString().Trim();
                    itemNameBNoVer = itemNameB;
                    amountB = Convert.ToInt32(rowB["MD006"]);
                    itemLvB = int.Parse(rowB["LV"].ToString().Trim().Replace(".", ""));
                    if (itemNameB.Length > 12)
                    {
                        continue;
                    }
                    else if (itemNameB.Length == 11) // 當長度為11碼時, 需額外處理
                    {
                        itemNameBNoVer = itemNameBNoVer.Substring(0, itemNameBNoVer.Length - 1);
                    }

                    // 先用ItemA品名長度決定是完全比對, 或是比對前10碼
                    if (isIncludeVersion == false)
                    {
                        // 品名為8-10碼, 完全比對
                        if (itemNameA.Equals(itemNameB) == false)
                        {
                            // 不符合, 繼續下一筆
                            continue;
                        }

                        bool tmpAmount = (amountA == amountB);
                        bool tmpLv = (itemLvA == itemLvB);

                        if (tmpAmount == true && tmpLv == true)
                        {
                            // 代表資料完全一樣
                            tempDr = convertNewItemToResultTalbeByRd(tSame, rowA, rowB, isIncludeVersion, true, true, true, isDefineAdd);
                            tSame.Rows.Add(tempDr);
                            // 已被使用過的要記錄
                            hashUsedTableB.Add(hashIndex, true);
                            isMatchSearch = true;
                            break;
                        }
                        tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, true, tmpAmount, isDefineAdd);
                        tDiff.Rows.Add(tempDr);
                        // 已被使用的要記錄
                        hashUsedTableB.Add(hashIndex, true);
                        isMatchSearch = true;
                        break;
                    }
                    else
                    {
                        // 品為為11碼, 含完全比對及模糊比對
                        // 先用完全比對
                        bool tmpItem = itemNameA.Equals(itemNameB);
                        int tmpItemNoVer = itemNameANoVer.CompareTo(itemNameB);
                        if (tmpItem == false && tmpItemNoVer != 0)
                        {
                            // 不符合, 繼續下一筆
                            continue;
                        }
                        bool tmpAmount = (amountA == amountB);
                        bool tmpLv = (itemLvA == itemLvB);
                        if (tmpItem == true && tmpAmount == true && tmpLv == true)
                        {
                            // 完全比對有符合項目, 含數量及LV均相同
                            tempDr = convertNewItemToResultTalbeByRd(tSame, rowA, rowB, false, true, true, true, isDefineAdd);
                            tSame.Rows.Add(tempDr);
                        }
                        else if (tmpItem == true)
                        {
                            // 完全比對有符合項目, 但數量或LV不相同
                            tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, true, tmpAmount, isDefineAdd);
                            tDiff.Rows.Add(tempDr);
                        }
                        else if (tmpItemNoVer == 0)
                        {
                            // 前10碼比對有相符項目
                            tempDr = convertNewItemToResultTalbeByRd(tDiff, rowA, rowB, isIncludeVersion, tmpLv, false, tmpAmount, isDefineAdd);
                            tDiff.Rows.Add(tempDr);
                        }
                        // 已被使用的要記錄
                        hashUsedTableB.Add(hashIndex, true);
                        isMatchSearch = true;
                        break;
                    }
                }

                // 不在上述條件, 才會走到此流程
                if (isMatchSearch == false)
                {
                    // 不在上述條件的, 一律為新增
                    tDiff.Rows.Add(convertNewItemToResultTalbeByRd(tDiff, rowA, rowA, false, false, false, false, isDefineAdd));
                    continue;
                }

            }

            majorSet.Tables.Add(tSame);
            majorSet.Tables.Add(tDiff);

            return majorSet;

        }

        private DataRow convertNewItemToResultTalbeByRd(DataTable targetTable, DataRow source, DataRow target, bool needConsideVer, bool isSameLv, bool isSameItemName, bool isSameAmount, bool isDefineAdd)
        {
            DataRow dr = targetTable.NewRow();

            // 先填入輔助資訊
            dr["CompareLV"] = "--";
            dr["CompareA8"] = "--";
            dr["CompareMD006"] = "--";
            dr["LV"] = "L" + source["LV"].ToString().Trim().Replace(".", "");
            dr["A8"] = source["A8"];
            dr["MB025X"] = source["MB025X"];
            dr["Column4"] = source["Column4"];
            dr["MB004"] = source["MB004"];
            dr["MD006"] = source["MD006"];
            dr["Column9"] = source["Column9"];

            string tmpNameSpace = (isDefineAdd == true ? target["NameSpace"].ToString() : source["NameSpace"].ToString());
            int findDotIndex = tmpNameSpace.IndexOf(".");
            if (findDotIndex < 0)
            {
                dr["ModuleLv1"] = tmpNameSpace;
            }
            else
            {
                dr["ModuleLv1"] = tmpNameSpace.Substring(0, findDotIndex);
            }

            // 版本變更, 11碼不同, 但10碼相同
            string verSrc = string.Empty;
            string verTar = string.Empty;
            if (needConsideVer == true)
            {
                // 取得版號
                verSrc = source["A8"].ToString().Trim();
                verSrc = verSrc.Substring(verSrc.Length - 1);
                verTar = target["A8"].ToString().Trim();
                verTar = verTar.Substring(verTar.Length - 1);
            }
            string displayPattern = "{0}-->{1}";

            if (isSameLv == true && isSameItemName == true && isSameAmount == true)
            {
                dr["CompareLV"] = "--";
                dr["CompareA8"] = "--";
                dr["CompareMD006"] = "--";
                // 考慮品號是否為11碼
                if (needConsideVer == true)
                {
                    // 版本變更, 11碼不同, 但10碼相同
                    dr["CompareA8"] = string.Format(displayPattern, verSrc, verTar);
                }
                return dr;
            }
            else if (isSameLv == false && isSameItemName == false && isSameAmount == false)
            {
                // New Item
                dr["CompareLV"] = "--";
                dr["CompareA8"] = (isDefineAdd == true ? "料件新增" : "料件刪除");
                dr["CompareMD006"] = "--";
                return dr;
            }

            // 未在上述條件回傳的, 均視為變更
            if (isSameLv == false)
            {
                // 完全相同, 8-10碼相同
                dr["CompareLV"] = string.Format(displayPattern, source["LV"].ToString().Trim().Replace(".", ""), target["LV"].ToString().Trim().Replace(".", ""));
            }

            if (isSameItemName == false)
            {
                dr["CompareA8"] = string.Format(displayPattern, verSrc.Substring(verSrc.Length - 1), verTar.Substring(verTar.Length - 1));
            }

            if (isSameAmount == false)
            {
                dr["CompareMD006"] = string.Format(displayPattern, source["MD006"], target["MD006"]);
            }

            return dr;
        }

        internal DataTable generateSummaryTableByRd(DataTable sourceDt, bool isIncludeChangeLv = false)
        {
            DataTable dt = new DataTable();

            dt = new DataTable("Summary");

            foreach (string header in DefinedHeader.BomCompareReportHeaderName)
            {
                dt.Columns.Add(header);
            }

            string compareLv = string.Empty;
            string compareA8 = string.Empty;
            string compareMD006 = string.Empty;
            string orgA8 = string.Empty;
            int rowId = 0;
            int compareIndex = compareMD006.IndexOf("-->");

            foreach (DataRow dr in sourceDt.Rows)
            {
                compareLv = dr["CompareLV"].ToString();
                compareA8 = dr["CompareA8"].ToString();
                compareMD006 = dr["CompareMD006"].ToString();
                orgA8 = dr["A8"].ToString();

                // 判斷是否要呈現只有變更LV的部份, 如果沒有到顯示, 不需要再往下執行
                if (isIncludeChangeLv == false && compareA8.Equals("--") && compareMD006.Equals("--"))
                {
                    continue;
                }

                DataRow newDr = dt.NewRow();
                rowId++;
                newDr["No"] = rowId;
                newDr["ModuleLv1"] = dr["ModuleLv1"];
                newDr["Column4"] = dr["Column4"];

                if(compareA8.Equals("料件刪除"))
                {
                    // 刪除
                    newDr["Delete"] = "V";
                    newDr["OrgA8"] = dr["A8"];
                    newDr["NewA8"] = "N/A";
                    newDr["OrgMD006"] = dr["MD006"];
                    newDr["NewMD006"] = "0";
                }
                else if (compareA8.Equals("料件新增"))
                {
                    // 新增
                    newDr["Add"] = "V";
                    newDr["OrgA8"] = "N/A";
                    newDr["NewA8"] = dr["A8"];
                    newDr["OrgMD006"] = "0";
                    newDr["NewMD006"] = dr["MD006"];
                }
                else
                {
                    // 變更
                    newDr["Change"] = "V";
                    compareIndex = compareMD006.IndexOf("-->");
                   
                    if (compareIndex < 0)
                    {
                        // 數量相同
                        newDr["OrgMD006"] = dr["MD006"];
                        newDr["NewMD006"] = dr["MD006"];
                    }
                    else
                    {
                        compareMD006 = compareMD006.Substring(compareIndex + 3);
                        newDr["OrgMD006"] = dr["MD006"];
                        newDr["NewMD006"] = compareMD006;
                    }

                    compareIndex = compareA8.IndexOf("-->");
                    if (compareIndex < 0)
                    {
                        // 版本沒有, 參考數量是否相同, 及要不要呈現LV
                        newDr["OrgA8"] = "N/A";
                        newDr["NewA8"] = dr["A8"];
                    }
                    else
                    {
                        // 版本變更
                        compareA8 = orgA8.Substring(0, orgA8.Length - 1) + compareA8.Substring(compareIndex + 3);
                        newDr["OrgA8"] = dr["A8"];
                        newDr["NewA8"] = compareA8;
                    }
                    if (isIncludeChangeLv == true)
                    {
                        newDr["CompareLV"] = dr["CompareLV"];
                    }
                }
                dt.Rows.Add(newDr);
            }

            if (isIncludeChangeLv == false)
            {
                dt.Columns.Remove("CompareLV");
            }

            return dt;
        }
        internal DataTable generateSummaryTableByPur(DataTable sourceDt)
        {
            DataTable dt = generateSummaryTableByRd(sourceDt, false);
            DataTable resultDt = new DataTable();

            resultDt = dt.Clone();

            // 開始執行filter 及總算
            // 因為狀態有3種, 新增 / 刪除 / 變更, 這3種狀態的數字不可以混在一起算
            // key 用 原品名+變更品名+模組, 如果相同, amount的值才可以相加減

            resultDt.Merge(caculateSummary(dt, "Add"));
            resultDt.Merge(caculateSummary(dt, "Change"));
            resultDt.Merge(caculateSummary(dt, "Delete"));

            // 處理完後, 再重新定義No
            int no = 0;
            foreach(DataRow dr in resultDt.Rows)
            {
                no++;
                dr["No"] = no;
            }

            return resultDt;
        }
        private DataTable caculateSummary(DataTable dt, string caculeType)
        {
            Dictionary<string, float> hashOrgAmount = new Dictionary<string, float>();
            Dictionary<string, float> hashNewAmount = new Dictionary<string, float>();
            Dictionary<string, DataRow> cacheRows = new Dictionary<string, DataRow>();
            DataTable resultDt = new DataTable();

            string hashKey = string.Empty;
            string OrgA8 = string.Empty;
            string NewA8 = string.Empty;
            string ModuleLv1 = string.Empty;
            string tmpState = string.Empty;
            float orgAmount = float.MinValue;
            float newAmount = float.MinValue;

            resultDt = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                OrgA8 = dr["OrgA8"].ToString();
                NewA8 = dr["NewA8"].ToString();
                ModuleLv1 = dr["ModuleLv1"].ToString();
                tmpState = dr[caculeType].ToString();

                if (tmpState.Equals("V") == false)
                {
                    continue;
                }

                DataRow newDr;
                hashKey = string.Format("{0}.{1}.{2}", OrgA8, NewA8, ModuleLv1);
                orgAmount = float.Parse(dr["OrgMD006"].ToString());
                newAmount = float.Parse(dr["NewMD006"].ToString());

                if (cacheRows.ContainsKey(hashKey) == false)
                {
                    // 新找到的row
                    newDr = dr;
                    hashOrgAmount.Add(hashKey, orgAmount);
                    hashNewAmount.Add(hashKey, newAmount);
                    cacheRows.Add(hashKey, newDr);
                    continue;
                }
                // 已有相同的row
                orgAmount = float.Parse(dr["OrgMD006"].ToString());
                newAmount = float.Parse(dr["NewMD006"].ToString());

                hashOrgAmount[hashKey] += orgAmount;
                hashNewAmount[hashKey] += newAmount;
            }

            // 將資料倒回
            foreach (KeyValuePair<string, DataRow> d in cacheRows)
            {
                DataRow newDr = (DataRow)d.Value;
                hashKey = d.Key;
                orgAmount = hashOrgAmount[hashKey];
                newAmount = hashNewAmount[hashKey];
                newDr["OrgMD006"] = orgAmount;
                newDr["NewMD006"] = newAmount;
                resultDt.Rows.Add(newDr.ItemArray);
            }
            return resultDt;
        }

        public DataSet CompareProcessByDev(string[] selectedList)
        {

            MvLogger.write("run {0}.{1}", new object[] { System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name });
            // 判斷是否選取相同的Bom表
            if (selectedList[0].Equals(selectedList[1]) == true)
            {
                MvLogger.write("same tables name : " + selectedList[0]);
                return null;
            }

            DataSet resultDs = new DataSet("ResultSet");
            DataSet sourceDs = new DataSet();
            DataTable tmpDt = new DataTable();
            MvBomCompareBo bo = new MvBomCompareBo();

            // Collect bom source by bom id
            sourceDs = bo.CollectSourceDsBomP09Process(selectedList.ToArray<string>()).Copy();
            MvLogger.write("finished function CollectSourceDsProcess");

            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            DataTable sourceDt2 = sourceDs.Tables[1].Copy();
            DataSet summaryDs = new DataSet();
            DataSet tmpDs = new DataSet();

            // filter data
            bo.filterBomP09ColumnByRdRule(ref sourceDt1);
            bo.filterBomP09ColumnByRdRule(ref sourceDt2);
            sourceDt1 = bo.filterDataByRdRule(sourceDt1, true);
            sourceDt2 = bo.filterDataByRdRule(sourceDt2, true);

            // extend namespace
            sourceDt1 = bo.extendBomNameSpace(sourceDt1);
            sourceDt2 = bo.extendBomNameSpace(sourceDt2);
            resultDs.Tables.Add(sourceDt1);
            resultDs.Tables.Add(sourceDt2);

            // get summary result
            // A --> B
            summaryDs = bo.compareBomByRuleRd_v2(sourceDt1, sourceDt2, false);
            // B --> A
            tmpDs = bo.compareBomByRuleRd_v2(sourceDt1, sourceDt2, true);

            // 顯示Detail Summary相關資訊
            // 相同的資料不用再呈現差異的比對資訊
            DataTable summaryDt;
            summaryDt = summaryDs.Tables["Different"];
            summaryDt.Merge(tmpDs.Tables["Different"]);

            resultDs.Tables.Add(summaryDs.Tables["Same"].Copy());
            resultDs.Tables.Add(summaryDs.Tables["Different"].Copy());
            MvLogger.write("finished to get compare tables");
            // 顯示SummaryBom相關資訊 for RD
            tmpDt = new DataTable();
            tmpDt = bo.generateSummaryTableByRd(summaryDt, true);
            tmpDt.TableName = "SummaryRD";
            resultDs.Tables.Add(tmpDt);
            MvLogger.write("finished to get table : SummaryRD");

            // 整理SummaryBom For Pur
            tmpDt = new DataTable();
            tmpDt = bo.generateSummaryTableByPur(summaryDt);
            tmpDt.TableName = "SummaryPUR";
            resultDs.Tables.Add(tmpDt);
            MvLogger.write("finished to get table : SummaryPUR");

            // Release object
            tmpDt.Dispose(); sourceDt1.Dispose(); sourceDt2.Dispose();
            summaryDs.Dispose(); tmpDs.Dispose(); sourceDs.Dispose();

            bo = null; tmpDt = null; sourceDt1 = null; sourceDt2 = null;
            summaryDs = null; tmpDs = null; sourceDs = null;

            return resultDs;
        }

        public DataSet GetBomP07InfoByDev(string bomId, bool isExtendNameSpace)
        {

            MvLogger.write("run {0}.{1}", new object[] { System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name });

            DataSet resultDs = new DataSet("ResultSet");
            DataSet sourceDs = new DataSet();
            MvBomCompareBo bo = new MvBomCompareBo();

            // Collect bom source by bom id
            sourceDs = bo.CollectSourceDsBomP07Process(new string[] { bomId }).Copy();
            MvLogger.write("finished function CollectSourceDsProcess");

            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            DataTable filterDt = sourceDs.Tables[0].Copy();
            // filter data
            bo.filterBomP07ColumnByRdRule(ref sourceDt1);
            bo.filterBomP07ColumnByRdRule(ref filterDt);
            filterDt = bo.filterDataByRdRule(filterDt, true);
            MvLogger.write("finished function filterDataByRdRule");

            // extend namespace , get module LV and amount space
            filterDt = bo.extendBomNameSpace(filterDt, true);
            MvLogger.write("finished function extendBomNameSpace");
            if (isExtendNameSpace == false)
            {
                filterDt.Columns.Remove("NameSpace");
                filterDt.Columns.Remove("NameSpaceNoVer");
            }
            filterDt.TableName += "_Filter";

            resultDs.Tables.Add(sourceDt1);
            resultDs.Tables.Add(filterDt);
            MvLogger.write("finished function add datatable to dataset");

            // Release object
            filterDt.Dispose(); sourceDt1.Dispose(); sourceDs.Dispose();

            bo = null; filterDt = null; sourceDt1 = null; sourceDs = null;

            return resultDs;
        }


        public DataSet GetBomP09InfoByDev(string bomId, bool isExtendNameSpace)
        {

            MvLogger.write("run {0}.{1}", new object[] { System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name });

            DataSet resultDs = new DataSet("ResultSet");
            DataSet sourceDs = new DataSet();
            MvBomCompareBo bo = new MvBomCompareBo();

            // Collect bom source by bom id
            sourceDs = bo.CollectSourceDsBomP09Process(new string[] { bomId }).Copy();
            MvLogger.write("finished function CollectSourceDsProcess");
           
            DataTable sourceDt1 = sourceDs.Tables[0].Copy();
            DataTable filterDt = sourceDs.Tables[0].Copy();
            // filter data
            bo.filterBomP09ColumnByRdRule(ref sourceDt1);
            bo.filterBomP09ColumnByRdRule(ref filterDt);
            filterDt = bo.filterDataByRdRule(filterDt, true);
            MvLogger.write("finished function filterDataByRdRule");

            // extend namespace and get module lv1
            filterDt = bo.extendBomNameSpace(filterDt, true);
            MvLogger.write("finished function extendBomNameSpace");
            if (isExtendNameSpace == false)
            {
                filterDt.Columns.Remove("NameSpace");
                filterDt.Columns.Remove("NameSpaceNoVer");
            }
            filterDt.TableName += "_Filter";

            resultDs.Tables.Add(sourceDt1);
            resultDs.Tables.Add(filterDt);
            MvLogger.write("finished function add datatable to dataset");

            // Release object
            filterDt.Dispose(); sourceDt1.Dispose(); sourceDs.Dispose();

            bo = null; filterDt = null; sourceDt1 = null; sourceDs = null;

            return resultDs;
        }


        public DataTable convertBomToMoc(DataTable bomTable)
        {
            DataTable resultDt = bomTable.Copy();

            resultDt.Columns.Add("RealAmount");
            foreach(DataRow dr in resultDt.Rows)
            {
                try
                {
                    string expression = dr["AmountSpace"].ToString();
                    if (expression.Length == 0)
                    {
                        expression = "0";
                    }
                    Expression e = new Expression(expression);
                    if (!e.HasErrors())
                    {
                        dr["RealAmount"] = e.Evaluate();
                    }
                }
                catch (EvaluationException)
                {
                    dr["RealAmount"] = "";
                }
            }

            return resultDt;
        }
    }
}
