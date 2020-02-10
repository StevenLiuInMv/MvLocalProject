using PrimS.Telnet;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MvLocalProject.Bo
{
    internal sealed class MvItCiscoBo : IDisposable
    {
        SocketException exceptionSE = null;

        public SocketException getSocketException()
        {
            return exceptionSE;
        }

        private async Task<string> getPortStatusByTelnet(string routerIp)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;


            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1" + Environment.NewLine + "en" + Environment.NewLine + "manager1" + Environment.NewLine + "conf t";
                await client.WriteLine(strCommand);

                strCommand = "do sh int status";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "    ";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "exit" + Environment.NewLine + "exit";
                await client.WriteLine(strCommand);
            }
            return strResult;
        }

        private async Task<string> getPortStatus(string routerIp)
        {
            string result = string.Empty;
            string strBuffer = string.Empty;
            string[] bufferArray = null;

            strBuffer = await getPortStatusByTelnet(routerIp);
            // 重新組合資訊, 濾除不要資訊
            bufferArray = strBuffer.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string tmpLine in bufferArray)
            {
                if (tmpLine.IndexOf("Gi0/") == 0 || tmpLine.IndexOf("routed") > 0 || (tmpLine.IndexOf("Po") == 0 && tmpLine.IndexOf("a-") > 0))
                {
                    result += string.Format("{0,-16}{1}{2}", routerIp, tmpLine, Environment.NewLine);
                }
            }

            return result; 
        }

        public async Task<string> getPortStatus(string[] routerIps)
        {
            string result = "IP              Port      Name               Status       Vlan       Duplex  Speed Type" + Environment.NewLine;
            string strBuffer = string.Empty;

            foreach (string ip in routerIps)
            {
                result += await getPortStatus(ip);
            }

            return result;
        }

        public async Task<DataTable> getPortStatusTable(string[] routerIps)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IP");
            dt.Columns.Add("Port");
            dt.Columns.Add("Name");
            dt.Columns.Add("Status");
            dt.Columns.Add("Vlan");
            dt.Columns.Add("Duplex");
            dt.Columns.Add("Speed");
            dt.Columns.Add("Type");

            string strBuffer = string.Empty;
            string[] tmpArray = null;
            DataRow dr = null; 
            foreach (string ip in routerIps)
            {
                //strBuffer = await getPortStatus(ip);
                try
                {
                    strBuffer = await getPortStatus(ip);
                }
                catch (SocketException se)
                {
                    exceptionSE = se;
                    continue;
                }
                tmpArray = strBuffer.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                foreach (string tmpLine in tmpArray)
                {
                    if (tmpLine.Length==0) { continue; }

                    dr = dt.NewRow();
                    string[] tmpSplitArray = tmpLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    dr["IP"] = tmpSplitArray[0];
                    dr["Port"] = tmpSplitArray[1];
                    if (tmpSplitArray.Length == 8 && tmpSplitArray[7].IndexOf("Present")==0)
                    {
                        dr["Name"] = "";
                        dr["Status"] = tmpSplitArray[2];
                        dr["Vlan"] = tmpSplitArray[3];
                        dr["Duplex"] = tmpSplitArray[4];
                        dr["Speed"] = tmpSplitArray[5];
                        dr["Type"] = tmpSplitArray[6] + " " + tmpSplitArray[7];
                    }
                    else if (tmpSplitArray.Length == 8)
                    { 
                        dr["Name"] = tmpSplitArray[2];
                        dr["Status"] = tmpSplitArray[3];
                        dr["Vlan"] = tmpSplitArray[4];
                        dr["Duplex"] = tmpSplitArray[5];
                        dr["Speed"] = tmpSplitArray[6];
                        dr["Type"] = tmpSplitArray[7];
                    }
                    else if (tmpSplitArray.Length == 7 && (tmpSplitArray[1].IndexOf("Gi0") == 0 || tmpSplitArray[1].IndexOf("Fa") == 0))
                    {
                        dr["Name"] = "";
                        dr["Status"] = tmpSplitArray[2];
                        dr["Vlan"] = tmpSplitArray[3];
                        dr["Duplex"] = tmpSplitArray[4];
                        dr["Speed"] = tmpSplitArray[5];
                        dr["Type"] = tmpSplitArray[6];

                    }
                    else if (tmpSplitArray.Length == 7 && tmpSplitArray[1].IndexOf("Po") == 0)
                    {
                        dr["Name"] = tmpSplitArray[2];
                        dr["Status"] = tmpSplitArray[3];
                        dr["Vlan"] = tmpSplitArray[4];
                        dr["Duplex"] = tmpSplitArray[5];
                        dr["Speed"] = tmpSplitArray[6];
                        dr["Type"] = "";
                    }
                    else
                    {
                        // 不在處理規則內
                        dr["IP"] = tmpLine;
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public async void enablePortByTelnet(string routerIp, string port)
        {
            string strCommand = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = string.Format("manager1{0}en{0}manager1{0}conf t{0}int {1} {0}no shutdown{0}exit{0}exit{0}wr{0}exit{0}", Environment.NewLine, port);
                await client.WriteLine(strCommand);
            }
        }

        public async void disablePortByTelnet(string routerIp, string port)
        {
            string strCommand = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = string.Format("manager1{0}en{0}manager1{0}conf t{0}int {1} {0}shutdown{0}exit{0}exit{0}wr{0}exit{0}", Environment.NewLine, port);
                await client.WriteLine(strCommand);
            }
        }

        public async Task<string> getMacAddressOnPortByTelnet(string routerIp, string port)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1" + Environment.NewLine + "en" + Environment.NewLine + "manager1";
                await client.WriteLine(strCommand);

                strCommand = string.Format("show running-config interface {0}", port);
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "     ";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "exit";
                await client.WriteLine(strCommand);
            }
            int tmpIndex = strResult.IndexOf("interface GigabitEthernet");
            // 代表取不到Mac
            if (tmpIndex == -1)
            {
                return strResult;
            }
            strResult = strResult.Remove(0, tmpIndex);
            tmpIndex = strResult.LastIndexOf("end");
            strResult = strResult.Remove(tmpIndex+3);
            return strResult;
        }

        private async Task<string> getMacAddressOnPort(string routerIp, string port)
        {
            string strBuffer = string.Empty;
            strBuffer = await getMacAddressOnPortByTelnet(routerIp, port);
            string[] tmpArray = strBuffer.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            strBuffer = string.Empty;
            foreach (string newLine in tmpArray)
            {
                int macAddressIndex = newLine.IndexOf("switchport port-security mac-address");
                if (macAddressIndex < 0) {
                    continue;
                }
                strBuffer += newLine.Replace("switchport port-security mac-address", "").Trim() + ",";
            }

            // 移除逗號
            if (strBuffer.Length > 0)
            {
                strBuffer = strBuffer.Remove(strBuffer.Length - 1);
            }

            return strBuffer;
        }

        public async Task<string> addMacAddressOnPortByTelnet(string routerIp, string port, string macAddress, string descriptionName, bool isDebug = false)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "en";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "conf t";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("int {0}", port);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "switchport port-security";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("switchport port-security mac-address {0}", macAddress);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("description {0}", descriptionName);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "no shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "wr";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();
            }
            if (isDebug == true)
            {
                return strResult;
            }
            else
            {
                return string.Empty;
            }

        }

        public async Task<string> removeMacAddressOnPortByTelnet(string routerIp, string port, string macAddress, string descriptionName, bool isDebug = false)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "en";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "manager1";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "conf t";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("int {0}", port);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "switchport port-security";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("no switchport port-security mac-address {0}", macAddress);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("description {0}", descriptionName);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "no shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "wr";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();
            }
            if (isDebug == true)
            {
                return strResult;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> getRouterLogsByTelnet(string routerIp)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1" + Environment.NewLine + "en" + Environment.NewLine + "manager1";
                await client.WriteLine(strCommand);

                strCommand = "show log ";
                await client.WriteLine(strCommand);
                strResult = await client.ReadAsync(new TimeSpan(0, 0, 2));

                strCommand = "     ";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 2));

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));
            }
            return strResult;
        }

        public async Task<string> setMacAddressNumberOnPortByTelnet(string routerIp, string port, string number, bool isDebug = false)
        {
            string strCommand = string.Empty;
            string strResult = string.Empty;

            using (Client client = new Client(routerIp, 23, new System.Threading.CancellationToken()))
            {
                strCommand = "manager1" + Environment.NewLine + "en" + Environment.NewLine + "manager1";
                await client.WriteLine(strCommand);

                strCommand = "conf t";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = string.Format("int {0}", port);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = string.Format("switchport port-security maximum {0}", number);
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "no shutdown";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync(new TimeSpan(0, 0, 1));

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "wr";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();

                strCommand = "exit";
                await client.WriteLine(strCommand);
                strResult += await client.ReadAsync();
            }
            if (isDebug == true)
            {
                return strResult;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<DataTable> getStatusTableIncludeMacAddress(string[] routerIps)
        {
            MvItCiscoBo bo = new MvItCiscoBo();

            DataTable tmpDt = null;
            DataTable finalDt = null;

            tmpDt = await bo.getPortStatusTable(routerIps);
            tmpDt.Columns.Remove("Duplex");
            tmpDt.Columns.Remove("Speed");
            tmpDt.Columns.Remove("Type");
            tmpDt.Columns.Add("MacAddress");
            tmpDt.Columns.Add("MaxPort");
            finalDt = tmpDt.Clone();

            string ip = string.Empty;
            string port = string.Empty;
            string name = string.Empty;
            string vlan = string.Empty;
            string macAddress = string.Empty;
            string status = string.Empty;
            string tmpString = string.Empty;
            string[] tmpArrary = null;
            int tmpIndex = 1;

            foreach (DataRow dr in tmpDt.Copy().Rows)
            {
                tmpIndex = 1;

                DataRow newDr = finalDt.NewRow();
                newDr.ItemArray = dr.ItemArray.Clone() as object[];

                ip = newDr["IP"].ToString();
                port = newDr["Port"].ToString();
                name = newDr["Name"].ToString();
                status = newDr["Status"].ToString();
                vlan = newDr["Vlan"].ToString();

                // 目前只針對vlan 151 , 符合名稱長度4碼, 及狀態非disabled的才執行檢查
                //if (!vlan.Equals("151") || name.Length != 4 || status.Equals("disabled"))
                //{
                //    newDr["MacAddress"] = "N/A";
                //    newDr["MaxPort"] = "N/A";

                //    finalDt.Rows.Add(newDr);
                //    continue;
                //}
                macAddress = string.Empty;
                tmpArrary = null;
                tmpString = string.Empty;

                macAddress = await bo.getMacAddressOnPortByTelnet(ip, port);
                Console.WriteLine(macAddress);

                // 先取出maximum number
                tmpIndex = macAddress.IndexOf("maximum");
                if (tmpIndex > 0)
                {
                    tmpString = macAddress.Substring(tmpIndex + 7);
                    tmpArrary = tmpString.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    tmpString = tmpArrary[0].Trim();
                    newDr["MaxPort"] = tmpString;
                }
                else
                {
                    newDr["MaxPort"] = "0";
                }

                // 取出mac address
                // 如果不存在就直接加入
                tmpIndex = macAddress.IndexOf("mac-address");
                if (tmpIndex < 0)
                {
                    newDr["MacAddress"] = string.Empty;
                    finalDt.Rows.Add(newDr);
                    continue;
                }

                // 如果存在mac-address 需取出各別的mac-address資訊
                if (tmpArrary == null)
                {
                    // 代表沒有被設定過maximun mac-address數
                    // 不需要再多長datarow
                    tmpString = macAddress.Substring(tmpIndex + 11);
                    tmpArrary = tmpString.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    tmpString = tmpArrary[0].Trim();
                    newDr["MacAddress"] = tmpString;

                    finalDt.Rows.Add(newDr);
                    continue;
                }

                // 如果tmpArray 不為null, 代表maxiumn已被設定 > 1
                // 需要將所有mac 找出來並呈現
                foreach (string tmpLine in tmpArrary)
                {
                    DataRow newDr1 = finalDt.NewRow();
                    newDr1.ItemArray = newDr.ItemArray.Clone() as object[];

                    tmpIndex = tmpLine.IndexOf("mac-address");
                    if (tmpIndex >= 0)
                    {
                        tmpString = tmpLine.Substring(tmpIndex + 11);
                        tmpString = tmpString.Replace("\r\n", "");
                        newDr1["MacAddress"] = tmpString.Trim();
                        finalDt.Rows.Add(newDr1);
                    }
                }
            }

            return finalDt;
        }


        public void Dispose()
        {
            exceptionSE = null;
            GC.SuppressFinalize(this);
        }
    }
}
