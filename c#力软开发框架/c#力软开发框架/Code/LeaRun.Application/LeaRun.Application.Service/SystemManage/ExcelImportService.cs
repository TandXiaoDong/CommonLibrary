using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-03-31 15:17
    /// 描 述：Excel导入模板表
    /// </summary>
    public class ExcelImportService : RepositoryFactory, ExcelImportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<ExcelImportEntity>(pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<ExcelImportEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFiledEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<ExcelImportFiledEntity>("select * from Base_ExcelImportFiled where F_ImportTemplateId='" + keyValue + "'");        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<ExcelImportEntity>(keyValue);
                db.Delete<ExcelImportFiledEntity>(t => t.F_Id.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        public void UpdateState(string keyValue, int State)
        {
            ExcelImportEntity userEntity = new ExcelImportEntity();
            userEntity.Modify(keyValue);
            userEntity.F_EnabledMark = State;
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ExcelImportEntity entity,List<ExcelImportFiledEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                db.Update(entity);
                //明细
                db.Delete<ExcelImportFiledEntity>(t => t.F_ImportTemplateId.Equals(keyValue));
                foreach (ExcelImportFiledEntity item in entryList)
                {
                    item.Create();
                    item.F_ImportTemplateId = entity.F_Id;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (ExcelImportFiledEntity item in entryList)
                {
                    item.Create();
                    item.F_ImportTemplateId = entity.F_Id;
                    db.Insert(item);
                }
            }
            db.Commit();
        }
        catch (Exception)
        {
            db.Rollback();
            throw;
        }
        }
        #endregion
    }
}
