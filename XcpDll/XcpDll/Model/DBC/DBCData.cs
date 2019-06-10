using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisAgreeMent.Model.DBC;
using AnalysisAgreeMent.Model;

namespace AnalysisAgreeMent.Model.DBC
{
    public class DBCData
    {
        /// <summary>
        /// 要解析的文件类型
        /// </summary>
        public FileType AnalysisFileType { get; set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        public AgreementType AgreeMentType { get; set; }

        /// <summary>
        /// 存储报文message
        /// </summary>
        public List<DBCMessage> DBCMessageList { get; set; }

        /// <summary>
        /// 存储报文信号集合
        /// </summary>
        public List<DBCSignal> DBCSignalList { get; set; }
    }
}
