﻿using LeaRun.Application.Entity.FlowManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:46
    /// 描 述：表单关联表
    /// </summary>
    public class FormModuleRelationMap : EntityTypeConfiguration<FormModuleRelationEntity>
    {
        public FormModuleRelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("Form_ModuleRelation");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
