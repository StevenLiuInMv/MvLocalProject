using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvSharedLib.Model
{
    //class GlobalEnum
    //{
    //}
    public enum MvCompanySite
    {
        MACHVISION = 1,
        MV_CE = 2,
        MV_CS = 3,
        MV_CC = 4,
        SIGOLD = 21,
        AUTOVISION = 22,
        MV_TEST = 999
    }

    public enum MvDBSource
    {
        ERPDB2_MACHVISION = 11,
        ERPDB2_MVTEST = 12,
        ERPBK_mvWorkFlow = 22,
    }
}
