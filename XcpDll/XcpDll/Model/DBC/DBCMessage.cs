using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.DBC
{
    /*
     * 
        BO_
        BO_是对Message的定义。
	    格式：BO_ ID Name: DLC Transmiter
        例子：BO_ 100 ESP_01: 8 ESP
		释义：发送方=ESP，帧名称=ESP_01，帧ID=0x64，报文长度=8个字节
    */
    /// <summary>
    /// dbc message 信息，包括：帧ID、帧名称、发送方、报文长度
    /// </summary>
    public class DBCMessage
    {
        /// <summary>
        /// 帧ID
        /// </summary>
        public Int32 FrameID { get; set; }

        /// <summary>
        /// 帧名称
        /// </summary>
        public string FrameName { get; set; }

        /// <summary>
        /// 报文长度
        /// </summary>
        public int MessageLen { get; set; }

        /// <summary>
        /// 发送方
        /// </summary>
        public string Sender { get; set; }
    }
}
