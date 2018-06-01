using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MvLocalProject.Controller;
using MvLocalProject.Model;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace MvLocalProject.Bo
{
    internal sealed class MvItToolBo
    {
        public static string[] mvLanDefault = new string[] { "192.168.111", "192.168.121", "192.168.122", "192.168.141", "192.168.142", "192.168.151", "192.168.161" };
        //public static string[] mvIpLanRange = new string[] { "192.168.161", "192.168.161.9", "192.168.151.107" };
        private static string[] mvPCListHeader = new string[] { "NetworkIP", "PCName", "IsSamePCName", "Action", "Remark" };

        /// <summary>
        /// 掃描網段內所有IP是否有回應
        /// </summary>
        /// <param name="ipRange">IP網段</param>
        /// <returns></returns>
        public DataTable scanComputerByIpRange(string[] ipRange, bool isOnlyReturnSuccess = true)
        {
            DataTable resultData = new DataTable("Result");
            // 定義result data的header
            foreach (string header in mvPCListHeader)
            {
                resultData.Columns.Add(header);
            }

            Hashtable hashByIp = new Hashtable();
            // 先用IP掃描所有網段
            foreach (string ipLan in ipRange)
            {
                // 取得IP Lan 範圍
                IPEnumeration IPList;
                IPList = getIpRange(ipLan);

                // 執行掃描作業
                string hostName = string.Empty;
                foreach (IPAddress ip in IPList)
                {
                    DataRow newDr = resultData.NewRow();
                    bool result = MvNetworker.isPingAlive(ip);
                    // ping 沒回應, 繼續下個IP
                    if (result == false)
                    {
                        if (isOnlyReturnSuccess == false)
                        {
                            newDr["NetworkIP"] = ip.ToString();
                            newDr["PCName"] = "--";
                            newDr["IsSamePCName"] = "--";
                            newDr["Action"] = "Fail";
                            newDr["Remark"] = "--";
                            resultData.Rows.Add(newDr);

                        }
                        continue;
                    }

                    try
                    {
                        // 取host name
                        hostName = string.Empty;
                        IPHostEntry host = Dns.GetHostEntry(ip);
                        hostName = host.HostName;
                        hashByIp.Add(ip.ToString(), hostName);
                    }
                    catch (SocketException)
                    {
                        hashByIp.Add(ip.ToString(), hostName);

                    }
                    newDr["NetworkIP"] = ip.ToString();
                    newDr["PCName"] = hostName;
                    newDr["IsSamePCName"] = "--";
                    newDr["Action"] = "Success";
                    newDr["Remark"] = "--";
                    resultData.Rows.Add(newDr);
                }
            }
            return resultData;
        }

        /// <summary>
        /// 取得Mv網段 alive PC 與DB Table PCList的差異
        /// </summary>
        /// <returns></returns>
        public DataTable compareMvNetworkByPcName(string[] ipRange)
        {
            DataTable resultData = new DataTable("Result");
            // 定義result data的header
            foreach (string header in mvPCListHeader)
            {
                resultData.Columns.Add(header);
            }
            // 忽略大小寫
            Hashtable hashByIp = new Hashtable(StringComparer.OrdinalIgnoreCase);
            Hashtable hashByName = new Hashtable(StringComparer.OrdinalIgnoreCase);
            // 先用IP掃描所有網段
            foreach (string ipLan in ipRange)
            {
                // 取得IP Lan 範圍
                IPEnumeration IPList = null;
                IPList = getIpRange(ipLan);

                string hostName = string.Empty;
                foreach (IPAddress ip in IPList)
                {
                    bool result = MvNetworker.isPingAlive(ip);
                    // ping 沒回應, 繼續下個IP
                    if (result == false) { continue; }
                    
                    try
                    {
                        // 取host name
                        IPHostEntry host = Dns.GetHostEntry(ip);
                        hostName = host.HostName;

                        hashByIp.Add(ip.ToString(), hostName);
                        // 只有取的到hostname的, 才放入hashByName, 供後續比對使用
                        hashByName.Add(hostName, ip.ToString());
                    }
                    catch (SocketException)
                    {
                        hashByIp.Add(ip.ToString(), hostName);
                    }
                }
            }
            // 用itReportLog內的資料判斷是否相同及存在
            DataTable pcList = MvDbDao.queryData_ItReportLog();
            // 因為IP會一直改變, 但DomainName不會變
            // 此部份的判斷邏輯會以DomainName為主
            string ipInTable = string.Empty;
            string pcNameInTable = string.Empty;
            string pcNameInTableWithoutDomain = string.Empty;
            string pcNameInHash = string.Empty;
            string ipInHashByName = string.Empty;

            // 1. 由Table PCList 比對 掃描網段的資料
            foreach (DataRow dr in pcList.Rows)
            {
                ipInTable = dr["NetworkIP"].ToString();
                pcNameInTableWithoutDomain = dr["PCName"].ToString();
                pcNameInTable = string.Format("{0}.{1}.{2}", dr["PCName"].ToString(), MvAdConnector.DomainOffice, MvAdConnector.DomainMv);

                DataRow newDr = resultData.NewRow();
                // 先用DomainName判斷是否存在Hash Table內
                // 不存在, 直接判斷該record資料已失效
                if (hashByName.Contains(pcNameInTable) == false)
                {
                    newDr["NetworkIP"] = ipInTable;
                    newDr["PCName"] = pcNameInTable;
                    newDr["IsSamePCName"] = "N";
                    newDr["Action"] = "Delete";
                    newDr["Remark"] = string.Empty;
                    resultData.Rows.Add(newDr);
                    continue;
                }
                // 存在, 取出IP做後續判斷
                ipInHashByName = hashByName[pcNameInTable].ToString();
                if (ipInHashByName.Equals(dr["NetworkIP"]) == true)
                {
                    // 判斷IP相同, 回傳不需修改
                    newDr["NetworkIP"] = ipInTable;
                    newDr["PCName"] = pcNameInTable;
                    newDr["IsSamePCName"] = "Y";
                    newDr["Action"] = string.Empty;
                    newDr["Remark"] = string.Empty;
                    hashByIp.Remove(ipInTable);

                }
                else
                {
                    // 判斷IP不同, 將要變更IP的結果存至Action
                    newDr["NetworkIP"] = ipInTable;
                    newDr["PCName"] = pcNameInTable;
                    newDr["IsSamePCName"] = "Y";
                    newDr["Action"] = "ChangeIp";
                    newDr["Remark"] = ipInHashByName ;
                    // 並將要變更的IP從hash中移除
                    hashByIp.Remove(ipInHashByName);
                }
                resultData.Rows.Add(newDr);
            }
            // 2. 未被移除的資料, 即為新增, 需放入Result data
            foreach (DictionaryEntry de in hashByIp) 
            {
                DataRow newDr = resultData.NewRow();
                newDr["NetworkIP"] = de.Key;
                newDr["PCName"] = de.Value.ToString().Replace(string.Format(".{0}.{1}", MvAdConnector.DomainOffice, MvAdConnector.DomainMv), "");
                newDr["IsSamePCName"] = "N";
                newDr["Action"] = "New";
                newDr["Remark"] = string.Empty;
                resultData.Rows.Add(newDr);
            }
            return resultData;
        }
        private IPEnumeration getIpRange(string ipLan)
        {
            // 每個IP Lan前置作業
            if (ipLan.Split('.').Length <= 2)
            {
                throw new FormatException("Un support define formation " + ipLan);
            }
            else if (ipLan.Split('.').Length > 3)
            {
                return new IPEnumeration(ipLan, ipLan);
            }
            else
            {
                string ipStar = ipLan + "." + "0";
                string ipEnd = ipLan + "." + "255";

                return new IPEnumeration(ipStar, ipEnd);
            }
        }
    }
}
