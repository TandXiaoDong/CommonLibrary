﻿using Learun.Application.OA.Email.EmailConfig;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_OA
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：邮件配置
    /// </summary>
    public class EmailConfigMap : EntityTypeConfiguration<EmailConfigEntity>
    {
        public EmailConfigMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_EMAILCONFIG");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}