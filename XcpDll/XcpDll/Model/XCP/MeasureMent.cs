using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public class MeasureMent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述或说明
        /// </summary>
        public string Describle { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 引用方法
        /// </summary>
        public string ReferenceMethod { get; set; }

        /// <summary>
        /// 比特率
        /// </summary>
        public string ResolutionBit { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string Accuracy { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public double LowerLimit { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public double UpperLimit { get; set; }

        /// <summary>
        /// bit mask
        /// </summary>
        public UInt32 BitMask { get; set; }

        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Ecu地址
        /// </summary>
        public Int32 EcuAddress { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public UInt32 length { get; set; }

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
