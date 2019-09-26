using Learun.Application.Base.SystemModule;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
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
    public class DataAuthorizeBLL : DataAuthorizeIBLL
    {
        private DataAuthorizeService dataAuthorizeService = new DataAuthorizeService();
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_dataauthorize_";

        private InterfaceIBLL interfaceIBLL = new InterfaceBLL(); 

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <param name="relationId">��ϵ����</param>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeConditionEntity> GetDataAuthorizeConditionList(string relationId)
        {
            try
            {
                IEnumerable<DataAuthorizeConditionEntity> list = cache.Read<IEnumerable<DataAuthorizeConditionEntity>>(cacheKey + relationId, CacheId.dataAuthorize);
                if (list == null)
                {
                    list = dataAuthorizeService.GetDataAuthorizeConditionList(relationId);
                    cache.Write<IEnumerable<DataAuthorizeConditionEntity>>(cacheKey + relationId, list, CacheId.dataAuthorize);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// ��ȡ����Ȩ�޶�Ӧ��ϵ�����б�
        /// <param name="interfaceId">�ӿ�����</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeRelationEntity> GetRelationList(string interfaceId)
        {
            try
            {
                IEnumerable<DataAuthorizeRelationEntity> list = cache.Read<IEnumerable<DataAuthorizeRelationEntity>>(cacheKey + interfaceId, CacheId.dataAuthorize);
                if (list == null)
                {
                    list = dataAuthorizeService.GetRelationList(interfaceId);
                    cache.Write<IEnumerable<DataAuthorizeRelationEntity>>(cacheKey + interfaceId, list, CacheId.dataAuthorize);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// ��ȡ����Ȩ�޶�Ӧ��ϵ�����б�
        /// <param name="interfaceId">�ӿ�����</param>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="keyword">��ѯ�ؼ���</param>
        /// <param name="objectId">��������</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeRelationEntity> GetRelationPageList(Pagination pagination, string interfaceId, string keyword, string objectId)
        {
            try
            {
                return dataAuthorizeService.GetRelationPageList(pagination, interfaceId, keyword, objectId);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// ��ȡ����Ȩ��������
        /// </summary>
        /// <param name="interfaceId">�ӿ�����</param>
        /// <param name="objectId">��Ӧ��ɫ���û�����</param>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeConditionEntity> GetConditionList(string interfaceId, string objectId)
        {
            try
            {
                List<DataAuthorizeRelationEntity> list = (List<DataAuthorizeRelationEntity>)GetRelationList(interfaceId);
                DataAuthorizeRelationEntity relationEntity = list.Find(t => t.F_ObjectId == objectId);
                if (relationEntity != null)
                {
                    return GetDataAuthorizeConditionList(relationEntity.F_Id);
                }
                else
                {
                    return new List<DataAuthorizeConditionEntity>();
                }

            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public DataAuthorizeRelationEntity GetRelationEntity(string keyValue)
        {
            try
            {
                return dataAuthorizeService.GetRelationEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ��ʵ������
        /// <param name="keyValue">����</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
          
            try
            {
                var entity = dataAuthorizeService.GetRelationEntity(keyValue);
                cache.Remove(cacheKey + entity.F_InterfaceId, CacheId.dataAuthorize);
                cache.Remove(cacheKey + entity.F_Id, CacheId.dataAuthorize);
                dataAuthorizeService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// ����ʵ�����ݣ��������޸ģ�
        /// <param name="keyValue">����</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, DataAuthorizeRelationEntity relationEntity, List<DataAuthorizeConditionEntity> conditionEntityList)
        {
            try
            {
                cache.Remove(cacheKey + relationEntity.F_InterfaceId, CacheId.dataAuthorize);
                cache.Remove(cacheKey + keyValue, CacheId.dataAuthorize);
                dataAuthorizeService.SaveEntity(keyValue, relationEntity, conditionEntityList);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region ��չ����
        /// <summary>
        /// ���ò�ѯ���
        /// </summary>
        /// <param name="url">�ӿڵ�ַ</param>
        /// <returns></returns>
        public bool SetWhereSql(string url)
        {
            try
            {
                UserInfo userInfo = LoginUserInfo.Get();
                if (userInfo.isSystem)
                {
                    return true;
                }
                // �жϸýӿ�ע����
                InterfaceEntity interfaceEntity = interfaceIBLL.GetEntityByUrl(url);
                if (interfaceEntity == null)
                {
                    // ����ӿ�û��ע����������
                    return true;
                }
                else
                {
                    List<DataAuthorizeRelationEntity> relationList = (List<DataAuthorizeRelationEntity>)GetRelationList(interfaceEntity.F_Id);
                    if (relationList.Count > 0)
                    {
                        
                        relationList = relationList.FindAll(t => t.F_ObjectId.Equals(userInfo.userId) || t.F_ObjectId.Like(userInfo.roleIds));
                        if (relationList.Count > 0)
                        {
                            string whereSql = "";
                            DbWhere dbWhere = new DbWhere();
                            dbWhere.dbParameters = new List<FieldValueParam>();

                            int relationnum = 0;
                            foreach (var item in relationList)
                            {
                                if (whereSql != "")
                                {
                                    whereSql += " OR ";
                                }
                                whereSql += " ( ";
                                string strSql = "";
                                List<DataAuthorizeConditionEntity> conditionList = (List<DataAuthorizeConditionEntity>)GetDataAuthorizeConditionList(item.F_Id);

                                if (!string.IsNullOrEmpty(item.F_Formula))
                                {
                                    strSql = item.F_Formula;
                                    for (int i = 1; i < conditionList.Count + 1; i++)
                                    {
                                        strSql = strSql.Replace("" + i, "{@learun" + i + "learun@}");
                                    }
                                }
                                else
                                {
                                    for (int i = 1; i < conditionList.Count + 1; i++)
                                    {
                                        if (strSql != "")
                                        {
                                            strSql += " AND ";
                                        }
                                        strSql += " {@learun" + i + "learun@} ";
                                    }
                                }

                                int num = 1;

                                foreach (var conditionItem in conditionList)
                                {
                                    string strone = " " + conditionItem.F_FieldId;
                                    string value = " @" + conditionItem.F_FieldId + relationnum;

                                    FieldValueParam dbParameter = new FieldValueParam();
                                    dbParameter.name = conditionItem.F_FieldId + relationnum;
                                    dbParameter.value = getValue(conditionItem.F_FiledValueType, conditionItem.F_FiledValue);
                                    dbParameter.type = conditionItem.F_FieldType;
                                    dbWhere.dbParameters.Add(dbParameter);
                                    //[{ value: 1, text: '����' }, { value: 2, text: '����' }, { value: 3, text: '���ڵ���' }, { value: 4, text: 'С��' }, { value: 5, text: 'С�ڵ���' }, { value: 6, text: '����' }, { value: 7, text: '������' }, { value: 8, text: '������' }, { value: 9, text: '������' }, { value: 10, text: '��������' }],
                                    switch (conditionItem.F_Symbol)
                                    {
                                        case 1:// ����
                                            strone += " = " + value;
                                            break;
                                        case 2:// ����
                                            strone += " > " + value;
                                            break;
                                        case 3:// ���ڵ���
                                            strone += " >= " + value;
                                            break;
                                        case 4:// С��
                                            strone += " < " + value;
                                            break;
                                        case 5:// С�ڵ���
                                            strone += " <= " + value;
                                            break;
                                        case 6:// ����
                                            strone += " like %" + value + "%";
                                            break;
                                        case 7:// ������
                                            strone += " in ( '" + value.Replace(",", "','") + "' )";
                                            break;
                                        case 8:// ������
                                            strone += " != " + value;
                                            break;
                                        case 9:// ������
                                            strone += " not like %" + value + "%";
                                            break;
                                        case 10:// ��������
                                            strone += " not in ( '" + value.Replace(",","','") + "' )";
                                            break;
                                        default:
                                            break;
                                    }
                                    strone += " ";
                                    strSql = strSql.Replace("{@learun" + num + "learun@}", strone);
                                    num++;
                                }

                                whereSql += strSql;
                                whereSql += " ) ";
                                relationnum++;
                            }
                            dbWhere.sql = whereSql;
                            WebHelper.AddHttpItems("DataAhthorCondition", dbWhere);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // �ýӿ�û��Ȩ�޹���
                        return true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// ���ò�ѯ���
        /// </summary>
        /// <returns></returns>
        public bool SetWhereSql()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            if (userInfo.departmentIds != null)
            {
                string whereSql = string.Format(" CHARINDEX(F_DepartmentId,'{0}') > 0", string.Join(",", userInfo.departmentIds));
                DbWhere dbWhere = new DbWhere();
                dbWhere.sql = whereSql;
                WebHelper.AddHttpItems("DataAhthorCondition", dbWhere);
            }
            return true;
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="value">����ֵ</param>
        /// <returns></returns>
        private string getValue(int? type, string value)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            //1.�ı�2.��¼��ID3.��¼���˺�4.��¼�߹�˾5.��¼�߲���6.��¼�߸�λ7.��¼�߽�ɫ
            string text = "";
            switch (type)
            {
                case 1:// �ı�
                    text = value;
                    break;
                case 2:// ��¼��ID
                    text = userInfo.userId;
                    break;
                case 3:// ��¼���˺�
                    text = userInfo.account;
                    break;
                case 4:// ��¼�߹�˾
                    text = userInfo.companyId;
                    break;
                case 41:// ��¼�߹�˾��������˾
                    foreach (var id in userInfo.companyIds) {
                        if (text != "") {
                            text += ",";
                        }
                        text += id;
                    }
                    break;
                case 5:// ��¼�߲���
                    text = userInfo.departmentId;
                    break;
                case 51:// ��¼�߲��ż���������
                    foreach (var id in userInfo.departmentIds)
                    {
                        if (text != "")
                        {
                            text += ",";
                        }
                        text += id;
                    }
                    break;
                case 6:// ��¼�߸�λ
                    text = userInfo.postIds;
                    break;
                case 7:// ��¼�߽�ɫ
                    text = userInfo.roleIds;
                    break;
                default:
                    text = value;
                    break;
            }
            return text;
        }
        #endregion
    }
}
