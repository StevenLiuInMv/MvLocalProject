using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace MvLocalProject.Controller
{
    internal sealed class MvAdConnector
    {

        public static string ConnectionString_LDAP
        {
            get { return ConfigurationManager.AppSettings["LDAP"]; }
        }
        public static string DomainMv
        {
            get { return ConfigurationManager.AppSettings["DomainMv"]; }
        }

        public static string DomainOffice
        {
            get { return ConfigurationManager.AppSettings["DomainOffice"]; }
        }

        public static string MxMailUrlMachvision
        {
            get { return ConfigurationManager.AppSettings["MxMailUrlMachvision"]; }
        }

        public static string MxMailUrlSiGold
        {
            get { return ConfigurationManager.AppSettings["MxMailUrlSiGold"]; }
        }


        public static DirectoryEntry Connection_LDAP
        {
            get { return new DirectoryEntry(ConnectionString_LDAP, "", "", AuthenticationTypes.ServerBind);  }
        }

        public static bool validateUser(string account, string password, string domain)
        {
            //string test1 = "LDAP://mv-dc.machvision.com.tw/DC=office,DC=machvision,DC=com,DC=tw";
            using (DirectoryEntry de = new DirectoryEntry(MvAdConnector.ConnectionString_LDAP, domain + "\\" + account, password, AuthenticationTypes.ServerBind))
            {
                return true;
                /**try
                {
                    object o = de.NativeObject;
                    return true;
                }
                catch (DirectoryServicesCOMException)
                {
                    return false;
                }
                catch (COMException)
                {
                    return false;
                }*/
            }
        }
    }
}
