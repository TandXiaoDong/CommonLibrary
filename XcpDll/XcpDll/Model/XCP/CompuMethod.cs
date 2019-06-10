using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public class CompuMethod
    {
        /// <summary>
        /// 函数名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述或说明
        /// </summary>
        public string describle { get; set; }
        /// <summary>
        /// 函数类型
        /// </summary>
        public string funType { get; set; }

        /// <summary>
        /// 格式
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 表达式原始值：coeffs
        /// </summary>
        public string coeffsValue { get; set; }

        /// <summary>
        /// 系数，a2l-表达式值计算转换后两位结果中的a
        /// </summary>
        public double Factor { get; set; }

        /// <summary>
        /// 系数，a2l-表达式值计算转换后两位结果中的b
        /// </summary>
        public double OffSet { get; set; }
    }
}
