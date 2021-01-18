using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvSharedLib.Model
{
    public enum MvCompanySite
    {
        MACHVISION = 1,     // 不能用MV_TW命名, 因為後面要Insert 到ERP時, 台灣ERP的公司別為MACHVISION, 華東為MV_CE, 華南為MV_CS
        MV_CE = 2,          // 所以只能用MACHVISION
        MV_CS = 3,
        MV_TEST = 999
    }

    public enum MvSystem
    {
        TW_ERP = 001,
//        TW_EASYFLOW = 002,
        TW_WORKFLOW = 003,
        TW_HR = 004,
//        TW_SCM = 005,
        TW_MVPLAN = 006,
        CN_ERP = 901
    }
}
