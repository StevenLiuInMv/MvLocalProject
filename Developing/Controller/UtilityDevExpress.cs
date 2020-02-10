using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraTreeList;

namespace MvLocalProject.Controller
{
    public static class UtilityDevExpress
    {
        public static void clearTreeListData(ref TreeList treeList)
        {
            treeList.DataSource = null;
            treeList.DataBindings.Clear();
            treeList.Columns.Clear();
        }
    }
}
