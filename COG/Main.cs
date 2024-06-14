using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COG
{
    public partial class Main
    {
        public partial struct DEFINE
        {

            public const string PROGRAM_TYPE = "OHC_InLine_BGM_INSPECTION_PC1";
            public const string SYS_DATADIR = "D:\\Systemdata_" + PROGRAM_TYPE + "\\";

            public const string MODEL_DATADIR = "VISION";
            public static string MODEL_FILE = DEFINE.SYS_DATADIR + "MODEL_" + DEFINE.MODEL_DATADIR + "\\";
            public const string LOG_DATADIR = "logdata\\";

            public const bool OPEN_F = true;
        }
        public struct MTickTimer
        {
            public DateTime timeStart;
            public DateTime timeEnd;

            public void StartTimer()
            {
                timeStart = DateTime.Now;
            }

            public long GetEllapseTime()
            {
                timeEnd = DateTime.Now;
                return (timeEnd.Ticks - timeStart.Ticks) / 10000; //리턴값 1ms 
            }
        }

    }
}
