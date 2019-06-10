using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public class XcpFormat
    {
        /// <summary>
        /// A2L文件measure变量格式及顺序,
        /// </summary>
        public enum XcpMeasureFormat
        {
            NULL_INDEX_0,//空
            NAME,//名称
            DESCRIBLE,//描述
            TYPE,//数据类型
            REFERENCE_METHOD,//引用方法
            RESOLUTION_IN_BITS,//比特率
            ACCURACY,//精度
            LOWER_LIMIT,//下限
            UPPER_LIMIT,//上限
            NULL_INDEX_9,//空
            BIT_MASK,//bit mask 0x
            FORMAT_11,//format
            NULL_12,//null
            ECU_ADDRESS,//ecu地址
            NULL_14
        }

        public enum XcpCompu_Method
        {
            NULL_0,
            REFERENCE_METHOD,//方法名
            DESCRIBLE,//描述
            FUN_TYLE,//函数类型
            FORMAT_STRING,//格式
            UNIT,//单位
            NULL_6,
            COEFFS,//表达式值
            NULL_8
        }

        public enum XcpMod_Common
        {
            BYTE_ORDER,
            ALIGNMENT_BYTE, //1
            ALIGNMENT_WORD, //2
            ALIGNMENT_LONG, //4
            ALIGNMENT_FLOAT32_IEEE //4
        }
    }
}
