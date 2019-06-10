using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.DBC
{

    public enum DbcResultEnum
    {
        SUCCESS,
        FAILT
    }

    public class DbcSearchSign
    {
        public const string VERSION = "VERSION";
        public const string NS = "NS_";
        public const string BS = "BS_";
        public const string BU = "BU_";
        public const string MESSAGE_START_BO = "BO_";
        public const string SIGNAL_SG = "SG_";
    }
}
