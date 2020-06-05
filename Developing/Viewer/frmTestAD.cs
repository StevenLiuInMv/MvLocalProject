using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using MvLocalProject.Controller;
using System.Security.Principal;
using System.Collections;
using System.Management;
using System.Net;
using System.Net.Sockets;
using MvLocalProject.Model;
using MvLocalProject.Bo;
using MvSharedLib.Controller;

namespace MvLocalProject.Viewer
{
    public partial class frmTestAD : Form
    {
        public frmTestAD()
        {
            InitializeComponent();
        }

        private void btnSearchUsers_Click(object sender, EventArgs e)
        {
            bool result = false;
            string ip = "192.168.161.10";
            result = MvNetworker.isPingAlive(ip);
            if (result == false)
            {
                Console.WriteLine(string.Format("{0} is not alive", ip));
            } else
            {
                Console.WriteLine(string.Format("{0} is alive", ip));
            }


            searchUserByWinPC();
            GetComputerUsers();
            
            //searchUsersByLDAP(MvAdConnector.ConnectionString_LDAP, MvAdConnector.DomainOffice);
            findAllUsers();
            string test1 = ValidateUser(MvAdConnector.ConnectionString_LDAP, MvAdConnector.DomainOffice);
            searchUserByWinPC();
            
        }

        private void searchUsersByLDAP(string ldapSetting, string domain)
        {
            List<string> userList = new List<string>();
            DirectoryEntry de = new DirectoryEntry(ldapSetting, domain + "\\" + "administrator", "ms7725", AuthenticationTypes.ServerBind);
            DirectorySearcher ds = new DirectorySearcher(de);

            string value = string.Empty;
            foreach(SearchResult sr in ds.FindAll())
            {
                foreach(string key in de.Properties.PropertyNames)
                {
                    foreach(object propVal in de.Properties[key])
                    {
                        value = key + "=" + propVal;
                        Console.WriteLine(value);
                    }
                }
            }

            string objectSid = "";

            try
            {
                objectSid = (new SecurityIdentifier((byte[])de.Properties["objectSid"].Value, 0).Value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                de.Dispose();
            }
        }

        private void searchUserByWinPC()
        {
            DirectoryEntry de;
            //string strPath = "WinNT://" + Environment.MachineName;
            string strPath = "WinNT://PC-00268";
            string result = string.Empty;
            de = new DirectoryEntry(strPath);
            de.Username = "stevenliu";
            de.Password = "m50882";
            string outputString = "";

            foreach (DirectoryEntry childDe in de.Children)
            {
                Console.WriteLine(string.Format("{0} ### {1}", childDe.SchemaClassName, childDe.Name));
                if (childDe.SchemaClassName == "User")
                {
                    outputString += childDe.Name + "<br>";
                }
                else if (childDe.SchemaClassName == "Service" && childDe.Name == "VSS")
                {
                    result = "";
                }
                else if(childDe.SchemaClassName == "Group" && childDe.Name == "Users")
                {
                    result = "";
                    DirectoryEntries childDe1 = childDe.Children;
                }
            }
        }


        public void findAllUsers()
        {
            string ldapServerName = "192.168.222.5";
            DirectoryEntry oRoot = new DirectoryEntry("LDAP://" + ldapServerName + "/ou=People,dc=office,dc=com", "administrator", "ms7725", AuthenticationTypes.ServerBind);
            DirectorySearcher oSearcher = new DirectorySearcher(oRoot);
            SearchResultCollection oResults;
            //SearchResult oResult;
            Hashtable retArry = new Hashtable();
            try
            {
                oSearcher.PropertiesToLoad.Add("uid");
                oSearcher.PropertiesToLoad.Add("givenname");
                oSearcher.PropertiesToLoad.Add("cn");
                oResults = oSearcher.FindAll();

                foreach (SearchResult oResult in oResults)
                {
                    if (oResult.GetDirectoryEntry().Properties["cn"].Value.ToString() == "")
                    {
                        Console.WriteLine(oResult.GetDirectoryEntry().Properties["cn"].Value.ToString() == "");
                    }

                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string ValidateUser(string ldapSetting, string domain)
        {

            string mail = "";
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://192.168.222.5/CN=Users,DC=office");

                DirectoryEntries entries = entry.Children;
                foreach (DirectoryEntry members in entries)
                {
                    if (members.Name.IndexOf("Some User Name") >= 0)
                    {
                        if (members.Properties.Contains("mail"))
                        {
                            foreach (object obj in members.Properties["mail"])
                            {
                                mail = obj.ToString();
                            }
                        }
                    }
                }
                return mail;
            }
            catch (Exception ex)
            {
                string tt = ex.Message;
                return tt;
            }


        }
        public static List<string> GetComputerUsers()
        {
            List<string> users = new List<string>();
            var path = string.Format("WinNT://{0},computer", Environment.MachineName);

            using (var computerEntry = new DirectoryEntry(path))
            {
                foreach (DirectoryEntry childEntry in computerEntry.Children)
                {
                    if (childEntry.SchemaClassName == "User")
                    {
                        Console.WriteLine(childEntry.Name);
                        users.Add(childEntry.Name);
                    }
                }
            }

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("users", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                Console.WriteLine(member.Name);
            }

            return users;
        }

        private void btnGetIPAliveByIpRange_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            
            IPEnumeration IPList = new IPEnumeration("192.168.161.0", "192.168.161.255");
            MvLogger.write("start process");
            MvItToolBo bo = new MvItToolBo();
            dt = bo.scanComputerByIpRange(new string[] { "192.168.161" }, true);
            MvLogger.write("end process");
            Console.WriteLine(dt.Rows.Count);


            richTextBox1.Clear();
            

            foreach (IPAddress ip in IPList)
            {
                bool result = MvNetworker.isPingAlive(ip);
                string hostName = string.Empty;
                string strShow = string.Empty;

                if (result == false)
                {
                    strShow = string.Format("{0} is not alive{1}", ip.ToString(), Environment.NewLine);
                    Console.WriteLine(strShow);
                    richTextBox1.Text += strShow;
                    richTextBox1.Refresh();

                    continue;
                }

                try
                {
                    // 取host name
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    hostName = host.HostName;
                    strShow = string.Format("{0} {1} is alive{2}", ip.ToString(), hostName, Environment.NewLine);
                    Console.WriteLine(strShow);
                    richTextBox1.Text += strShow;
                    richTextBox1.Refresh();
                }
                catch (SocketException)
                {
                    strShow = string.Format("{0} host name is not alive{1}", ip.ToString(), Environment.NewLine);
                    Console.WriteLine(strShow);
                    richTextBox1.Text += strShow;
                    richTextBox1.Refresh();
                }
            }
        }

        private void btnScanItPcBo_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            MvItToolBo bo = new MvItToolBo();
            //dt = bo.compareMvNetworkByPcName(MvItToolBo.mvLanDefault);
            dt = bo.compareMvNetworkByPcName(new string[] { "192.168.161" });
            dataGridView1.DataSource = dt;
        }

        private void frmTestAD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
