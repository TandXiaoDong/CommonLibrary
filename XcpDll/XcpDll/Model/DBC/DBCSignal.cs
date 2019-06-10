using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.DBC
{
    public class DBCSignal
    {
        /*
         * 格式：SG_ Name : StartBit | Length @ ByteOrder SignedFlag (Factor,Offset) [Minimum | Maximum] "Unit" Receiver1,Receiver2
        例子：SG_ VehSpd : 7|16@0+ (0.01,0) [0|655.35] "km/h" ECM.TCM
		释义：信号名称=VehSpd，起始地址=7，长度=16，字节顺序=MSB（大端），符号位=无符号，系数=0.01，偏移=0，最小值=0，最大值=655.35，单位=km/h，接收方=ECM和TCM
		显示列头：sg_name   message_name    startbit len(bit)  byte order  value type factor offset  minimum maximum unit
        */

        /// <summary>
        /// 信号名称
        /// </summary>
        public string SignalName { get; set; }

        /// <summary>
        /// 对应帧ID
        /// </summary>
        public Int32 FrameID { get; set; }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int StartBitIndex { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int BitLength { get; set; }

        /// <summary>
        /// 字节顺序/数据类型：0-int,1-motonor
        /// 意义：motonor-大端（MSB/单片机），int-小端（PC）
        /// 区别：motorola高字节在前，inter低字节在前
        /// </summary>
        public ByteOrder ByteOrder { get; set; }

        /// <summary>
        /// 符号类型：+（无符号） ，-（有符号）
        /// </summary>
        public string SymbolType { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public float Factor { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        public float Offset { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public float Minimum { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public float Maximun { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 接收方
        /// </summary>
        public string Receiver { get; set; }
    }
}
