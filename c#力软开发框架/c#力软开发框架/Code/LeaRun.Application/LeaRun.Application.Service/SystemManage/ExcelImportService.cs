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
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-03-31 15:17
    /// �� ����Excel����ģ���
    /// </summary>
    public class ExcelImportService : RepositoryFactory, ExcelImportIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<ExcelImportEntity>(pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<ExcelImportEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFiledEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<ExcelImportFiledEntity>("select * from Base_ExcelImportFiled where F_ImportTemplateId='" + keyValue + "'");        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ExcelImportEntity entity,List<ExcelImportFiledEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
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
                //����
                entity.Create();
                db.Insert(entity);
                //��ϸ
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
