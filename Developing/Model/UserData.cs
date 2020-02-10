using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvLocalProject.Model
{
    public class UserData
    {
        private string adAccount = string.Empty;
        private string password = string.Empty;
        private string employeeID = string.Empty;
        private string employeeName = string.Empty;
        private string departmentID = string.Empty;
        private string departmentName = string.Empty;
        private MvCompanySite companySite = MvCompanySite.MACHVISION;

        /// <summary>
        /// AD account
        /// </summary>
        public string AdAccount
        { get { return adAccount; } set { adAccount = value; } }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        { get { return password; } set { password = value; } }

        /// <summary>
        /// Employee ID
        /// </summary>
        public string EmployeeID
        { get { return employeeID; } set { employeeID = value; } }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string EmployeeName
        { get { return employeeName; } set { employeeName = value; } }

        /// <summary>
        /// Department ID
        /// </summary>
        public string DepartmentID
        { get { return departmentID; } set { departmentID = value; } }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName
        { get { return departmentName; } set { departmentName = value; } }

        /// <summary>
        /// Mv Company
        /// </summary>
        public MvCompanySite CompanySite
        { get { return companySite; } set { companySite = value; } }

        public void clear()
        {
            adAccount = string.Empty;
            password = string.Empty;
            employeeID = string.Empty;
            departmentID = string.Empty;
            departmentName = string.Empty;
            companySite = MvCompanySite.MACHVISION;
        }
    }
}
