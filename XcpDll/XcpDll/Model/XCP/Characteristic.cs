using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public class Characteristic
    {
        #region 公共属性
        /// <summary>
        /// 名称
        /// </summary>
        public string name;

        /// <summary>
        /// 维度
        /// </summary>
        public byte dimension;

        /// <summary>
        /// 维度类型
        /// </summary>
        public string dimensiontype;

        /// <summary>
        /// 地址
        /// </summary>
        public UInt32 address;

        /// <summary>
        /// 记录布局
        /// </summary>
        public string recordLayout;

        /// <summary>
        /// 写入页码数
        /// </summary>
        public byte writePageNumber;

        /// <summary>
        /// 读取页码数
        /// </summary>
        public byte readPageNumber;

        /// <summary>
        /// 页码数量
        /// </summary>
        public byte pageCount;

        /// <summary>
        /// 章节数
        /// </summary>
        public byte segmentNumber;
        #endregion

        #region X 属性
        /// <summary>
        /// X名称
        /// </summary>
        public string X_name;

        /// <summary>
        /// X数
        /// </summary>
        public UInt32 X_count;

        /// <summary>
        /// X数长度
        /// </summary>
        public UInt32 X_countLength;

        /// <summary>
        /// X元素长路
        /// </summary>
        public UInt32 X_elementLength;

        /// <summary>
        /// X元素类型
        /// </summary>
        public string X_elementType;

        /// <summary>
        /// X表达
        /// </summary>
        public string X_Expression;

        /// <summary>
        /// X表达类型
        /// </summary>
        public string X_ExpressType;

        /// <summary>
        /// X参数
        /// </summary>
        public List<float> X_parameter;

        /// <summary>
        /// X键值
        /// </summary>
        public Dictionary<UInt32, string> X_tableDictionary;

        /// <summary>
        /// X最大值
        /// </summary>
        public float X_maxValue;

        /// <summary>
        /// X最小值
        /// </summary>
        public float X_minValue;
        #endregion

        #region Y属性
        /// <summary>
        /// Y名称
        /// </summary>
        public string Y_name;

        /// <summary>
        /// Y数据
        /// </summary>
        public UInt32 Y_count;

        /// <summary>
        /// Y数据长度
        /// </summary>
        public UInt32 Y_countLength;

        /// <summary>
        /// Y元素长度
        /// </summary>
        public UInt32 Y_elementLength;

        /// <summary>
        /// Y元素类型
        /// </summary>
        public string Y_elementType;

        /// <summary>
        /// Y表达式
        /// </summary>
        public string Y_Expression;

        /// <summary>
        /// Y表达式类型
        /// </summary>
        public string Y_ExpressType;

        /// <summary>
        /// Y参数
        /// </summary>
        public List<float> Y_parameter;

        /// <summary>
        /// Y键值
        /// </summary>
        public Dictionary<UInt32, string> Y_tableDictionary;

        /// <summary>
        /// Y最大值
        /// </summary>
        public float Y_maxValue;

        /// <summary>
        /// Y最小值
        /// </summary>
        public float Y_minValue;
        #endregion

        #region V属性
        /// <summary>
        /// V数据
        /// </summary>
        public UInt32 V_count;

        /// <summary>
        /// V元素长度
        /// </summary>
        public UInt32 V_elementLength;

        /// <summary>
        /// V元素类型
        /// </summary>
        public string V_elementType;

        /// <summary>
        /// V表达式
        /// </summary>
        public string V_Expression;

        /// <summary>
        /// V表达式类型
        /// </summary>
        public string V_ExpressType;

        /// <summary>
        /// V参数
        /// </summary>
        public List<float> V_parameter;

        /// <summary>
        /// V键值
        /// </summary>
        public Dictionary<UInt32, string> V_tableDictionary;

        /// <summary>
        /// V最大值
        /// </summary>
        public float V_maxValue;

        /// <summary>
        /// V最小值
        /// </summary>
        public float V_minValue;

        /// <summary>
        /// V量程
        /// </summary>
        public float V_range;
        #endregion
    }
}
