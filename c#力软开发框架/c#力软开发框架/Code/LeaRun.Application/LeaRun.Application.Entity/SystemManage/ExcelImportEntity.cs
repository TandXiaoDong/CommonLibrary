using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-03-31 15:17
    /// �� ����Excel����ģ���
    /// </summary>
    public class ExcelImportEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// ����ģ��Id
        /// </summary>
        /// <returns></returns>
        [Column("F_MODULEID")]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// ������ťId
        /// </summary>
        /// <returns></returns>
        [Column("F_MODULEBTNID")]
        public string F_ModuleBtnId { get; set; }
        /// <summary>
        /// �������ݿ�ID
        /// </summary>
        /// <returns></returns>
        [Column("F_DBID")]
        public string F_DbId { get; set; }
        /// <summary>
        /// �������ݿ��
        /// </summary>
        /// <returns></returns>
        [Column("F_DBTABLE")]
        public string F_DbTable { get; set; }
        /// <summary>
        /// ���������0��ֹ,1����
        /// </summary>
        /// <returns></returns>
        [Column("F_ERRORTYPE")]
        public int? F_ErrorType { get; set; }
        /// <summary>
        /// �Ƿ���Ч0��ͣ,1����
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>		
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>		
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>		
        public string F_ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_EnabledMark = 1;
            this.F_CreateDate = new DateTime?(DateTime.Now);
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
                                            }
        #endregion
    }
}