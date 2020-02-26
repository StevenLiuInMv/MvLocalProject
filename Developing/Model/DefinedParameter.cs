using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvLocalProject.Model
{
    public sealed class GlobalConstant
    {
        public static readonly string[] MvAdminPcHostName = new string[] { "PC-00268" };
        public static readonly string[] MvMisPcHostName = new string[] { "--PC-00268", "PC-00031", "PC-00233", "PC-00214", "PC-00254", "PC-00152", "PC-00153", "PC-00176", "PC-00247", "PC-00057" };
        public static readonly string[] MvMcPcHostName = new string[] { "PC-00255", "PC-00268-" ,"MV-TFG-C01"};
        public static readonly string[] MvRdPcHostName = new string[] { "PC-00329", "PC-00287" ,"MV-TEST-V02"};
        public static readonly string[] MvCsrPcHostName = new string[] { "PC-00226", "PC-00250", "PC-00268" };
        public static readonly string[] MvMisCiscoIpList = new string[] { "192.168.151.11", "192.168.151.12", "192.168.151.13", "192.168.151.14", "192.168.151.15"};
        public static readonly string[] MvMisCiscoIpListByLimitMac = new string[] { "192.168.151.11", "192.168.151.12", "192.168.151.13", "192.168.151.15", "192.168.151.16" };
        public static readonly string MvDnsServerName = "office.machvision.com.tw";
        public static readonly string MvDnsBackupServerName = "mv-dc.machvision.com.tw";
    }

    public static class GlobalMvVariable
    {
        public static string MvAdUserName = string.Empty;
        public static string MvAdPassword = string.Empty;
        public static MvCompanySite MvAdCompany = MvCompanySite.MACHVISION;
        public static string MvEmpNo = string.Empty;
        public static string MvDeptNo = string.Empty;
        public static UserData UserData = new UserData();
    }

    public static class GlobalMvVariableForMc
    {
        public static readonly string[] OwnWarehouses = { "301", "310", "472", "701" };
    }
}
