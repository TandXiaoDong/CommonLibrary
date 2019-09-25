﻿using System;
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
    /// 描 述：表格字段信息
    /// </summary>
    public class GridColumnModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 字段Id
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 显示位置
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool hidden { get; set; }
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool sortable { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string formatter { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string datatype { get; set; }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public string isnullable { get; set; }
        /// <summary>
        /// 是否列表页中查询
        /// </summary>
        public bool query { get; set; }
    }
}
