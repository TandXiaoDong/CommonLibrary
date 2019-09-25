using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FigKey.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2013-2019 丰柯电子科技（上海）有限公司重庆分公司
    /// 创建人：唐小东
    /// 日 期：2019.09.24 9:54
    /// 描 述：表单字段信息
    /// </summary>
    public class FormFieldModel
    {
        /// <summary>
        /// 字段标识
        /// </summary>
        public string controlId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string controlName { get; set; }
        /// <summary>
        /// 字段验证
        /// </summary>
        public string controlValidator { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string controlType { get; set; }
        /// <summary>
        /// 合并列
        /// </summary>
        public int? controlColspan { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string controlDataSource { get; set; }
        public string controlDataItemEncode { get; set; }
        public string controlDataItemEncodeValue { get; set; }
        public string controlDataDb { get; set; }
        public string controlDataTable { get; set; }
        public string controlDataFliedId { get; set; }
        public string controlDataFliedText { get; set; }
        public string controlDefault { get; set; }
    }
}
