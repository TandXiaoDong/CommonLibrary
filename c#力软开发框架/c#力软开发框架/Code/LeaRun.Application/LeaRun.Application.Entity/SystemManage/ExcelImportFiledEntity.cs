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
    public class ExcelImportFiledEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// ����ģ��Id
        /// </summary>
        /// <returns></returns>
        [Column("F_IMPORTTEMPLATEID")]
        public string F_ImportTemplateId { get; set; }
        /// <summary>
        /// �ֵ�����
        /// </summary>
        /// <returns></returns>
        [Column("F_FLIEDNAME")]
        public string F_FliedName { get; set; }
        /// <summary>
        /// �ֶ�����
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEDTYPE")]
        public string F_FiledType { get; set; }
        /// <summary>
        /// excel����
        /// </summary>
        /// <returns></returns>
        [Column("F_COLNAME")]
        public string F_ColName { get; set; }
        /// <summary>
        /// Ψһ����֤:0Ҫ,1��Ҫ
        /// </summary>
        /// <returns></returns>
        [Column("F_ONLYONE")]
        public int? F_OnlyOne { get; set; }
        /// <summary>
        /// ��������0:�޹���,1:GUID,2:�����ֵ�3:���ݱ�;4:�̶���ֵ;5:������ID;6:����������;7:����ʱ��;
        /// </summary>
        /// <returns></returns>
        [Column("F_RELATIONTYPE")]
        public int? F_RelationType { get; set; }
        /// <summary>
        /// �����ֵ���
        /// </summary>
        /// <returns></returns>
        [Column("F_DATAITEMENCODE")]
        public string F_DataItemEncode { get; set; }
        /// <summary>
        /// �̶�����
        /// </summary>
        /// <returns></returns>
        [Column("F_VALUE")]
        public string F_Value { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        /// <returns></returns>
        [Column("F_DBID")]
        public string F_DbId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("F_DBTABLE")]
        public string F_DbTable { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <returns></returns>
        [Column("F_DBSAVEFLIED")]
        public string F_DbSaveFlied { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <returns></returns>
        [Column("F_DBRELATIONFLIED")]
        public string F_DbRelationFlied { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
                                            }
        #endregion
    }
}