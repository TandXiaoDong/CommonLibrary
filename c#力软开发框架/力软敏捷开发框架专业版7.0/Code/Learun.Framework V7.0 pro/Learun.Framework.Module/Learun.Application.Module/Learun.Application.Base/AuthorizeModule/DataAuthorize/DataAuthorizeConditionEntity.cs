using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// �� �� Learun-ADMS V7.0.0 �������ݿ������
    /// Copyright (c) 2013-2018 �Ϻ�������Ϣ�������޹�˾
    /// �� ���������ܿ�����
    /// �� �ڣ�2017-06-21 16:30
    /// �� ��������Ȩ��
    /// </summary>
    public class DataAuthorizeConditionEntity 
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// ����Ȩ�޶�Ӧ��ϵ����
        /// </summary>
        /// <returns></returns>
        [Column("F_DATAAUTHORIZERELATIONID")]
        public string F_DataAuthorizeRelationId { get; set; }
        /// <summary>
        /// �ֶ�ID
        /// </summary>
        /// <returns></returns>
        [Column("F_FIELDID")]
        public string F_FieldId { get; set; }
        /// <summary>
        /// �ֶ�����
        /// </summary>
        /// <returns></returns>
        [Column("F_FIELDNAME")]
        public string F_FieldName { get; set; }
        /// <summary>
        /// �ֶ�����
        /// </summary>
        [Column("F_FIELDTYPE")]
        public int F_FieldType { get; set; }
        /// <summary>
        /// �ȽϷ�1.����2.����3.������4.������5.������6.��������
        /// </summary>
        /// <returns></returns>
        [Column("F_SYMBOL")]
        public int? F_Symbol { get; set; }
        /// <summary>
        /// �ȽϷ�����
        /// </summary>
        /// <returns></returns>
        [Column("F_SYMBOLNAME")]
        public string F_SymbolName { get; set; }
        /// <summary>
        /// �ֶ�ֵ����1.�ı�2.��¼��ID3.��¼���˺�4.��¼�߹�˾5.��¼�߲���6.��¼�߸�λ7.��¼�߽�ɫ
        /// </summary>
        [Column("F_FILEDVALUETYPE")]
        public int? F_FiledValueType { get; set; }
        /// <summary>
        /// �ֶ�ֵ
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEDVALUE")]
        public string F_FiledValue { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("F_SORT")]
        public int F_Sort { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}

