using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model
{
    /// <summary>
    /// 解析DBC/XCP/CCP通用格式(保存时格式一致性)，DBC-Signal信号，XCP/CCP-begin measure
    /// </summary>
    public class AnalysisSignal
    {
        //name + describle+unit+dataType+dataLen+IsMotorola+startIndex+dataBitLen+dataAddress+factor+offset

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 变量名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 变量描述
        /// </summary>
        public string Describle { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 保存的数据类型
        /// </summary>
        public SaveDataTypeEnum SaveDataType { get; set; }

        /// <summary>
        /// 转换后的数据长度，以8bit为一个单位计算长度 len = 位长度/8
        /// </summary>
        public int SaveDataLen { get; set; }

        /// <summary>
        /// 是Motorola 格式还是Integer格式
        /// </summary>
        public int IsMotorola { get; set; }

        /// <summary>
        /// 数据起始位置索引，DBC有用，a2l无用
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 方法类型,a2l有效
        /// </summary>
        public string ReferenceMethod { get; set; }

        /// <summary>
        /// 数据长度，DBC有用，a2l无用
        /// </summary>
        public int DataBitLen { get; set; }

        /// <summary>
        /// A2l-ECU数据地址  Mornitor-的CANID
        /// </summary>
        public Int32 DataAddress { get; set; }

        /// <summary>
        /// 系数，a2l-表达式值计算转换后两位结果中的a
        /// </summary>
        public double Factor { get; set; }

        /// <summary>
        /// 系数，a2l-表达式值计算转换后两位结果中的b
        /// </summary>
        public double OffSet { get; set; }


        /// <summary>
        /// 键值
        /// </summary>
        public Dictionary<UInt32, string> tableDictionary;

        /// <summary>
        /// 存储参数
        /// </summary>
        public List<float> parameter;
    }
}
