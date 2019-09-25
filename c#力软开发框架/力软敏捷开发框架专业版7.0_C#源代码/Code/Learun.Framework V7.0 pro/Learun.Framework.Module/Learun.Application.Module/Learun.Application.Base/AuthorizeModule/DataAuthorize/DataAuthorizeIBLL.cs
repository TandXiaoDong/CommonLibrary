using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// �� �� Learun-ADMS V7.0.0 �������ݿ������
    /// Copyright (c) 2013-2018 �Ϻ�������Ϣ�������޹�˾
    /// �� ���������ܿ�����
    /// �� �ڣ�2017-06-21 16:30
    /// �� ��������Ȩ��
    /// </summary>
    public interface DataAuthorizeIBLL
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <param name="relationId">��ϵ����</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeConditionEntity> GetDataAuthorizeConditionList(string relationId);
        /// <summary>
        /// ��ȡ����Ȩ�޶�Ӧ��ϵ�����б�
        /// <param name="moduleId">ģ������</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<DataAuthorizeRelationEntity> GetRelationList(string moduleId);
        /// <summary>
        /// ��ȡ����Ȩ�޶�Ӧ��ϵ�����б�
        /// <param name="moduleId">ģ������</param>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="keyword">��ѯ�ؼ���</param>
        /// <param name="objectId">��������</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<DataAuthorizeRelationEntity> GetRelationPageList(Pagination pagination, string moduleId, string keyword, string objectId);
        /// <summary>
        /// ��ȡ����Ȩ��������
        /// </summary>
        /// <param name="moduleId">����ģ������</param>
        /// <param name="objectId">��Ӧ��ɫ���û�����</param>
        /// <returns></returns>
        IEnumerable<DataAuthorizeConditionEntity> GetConditionList(string moduleId, string objectId);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        DataAuthorizeRelationEntity GetRelationEntity(string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ��ʵ������
        /// <param name="keyValue">����</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// ����ʵ�����ݣ��������޸ģ�
        /// <param name="keyValue">����</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, DataAuthorizeRelationEntity relationEntity, List<DataAuthorizeConditionEntity> conditionEntityList);
        #endregion

        #region ��չ����
        /// <summary>
        /// ���ò�ѯ���
        /// </summary>
        /// <param name="url">�ӿڵ�ַ</param>
        /// <returns></returns>
        bool SetWhereSql(string url);
        #endregion
    }
}
