﻿using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using LeaRun.Application.Entity.AuthorizeManage;
namespace LeaRun.Application.Busines.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:46
    /// 描 述：表单关联表
    /// </summary>
    public class FormModuleRelationBLL
    {
        private FormModuleRelationIService service = new FormModuleRelationService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FormModuleRelationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FormModuleRelationEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleRelationEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FormModuleRelationEntity entity,ModuleEntity module)
        {
            try
            {
                service.SaveForm(keyValue, entity, module);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateForm(string keyValue)
        {
            try
            {
                service.UpdateForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
