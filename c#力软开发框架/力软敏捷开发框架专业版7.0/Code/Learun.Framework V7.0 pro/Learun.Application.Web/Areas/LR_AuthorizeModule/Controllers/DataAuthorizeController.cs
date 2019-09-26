using Learun.Application.Base.AuthorizeModule;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// �� �� Learun-ADMS V7.0.0 �������ݿ������
    /// Copyright (c) 2013-2018 �Ϻ�������Ϣ�������޹�˾
    /// �� ���������ܿ�����
    /// �� �ڣ�2017-06-21 16:30
    /// �� ��������Ȩ��
    /// </summary>
    public class DataAuthorizeController : MvcControllerBase
    {
        private DataAuthorizeIBLL dataAuthorizeIBLL = new DataAuthorizeBLL();

        #region ��ͼ����

        /// <summary>
        /// ��ҳ��
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// ��ҳ
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryForm()
        {
             return View();
        }
        

        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <param name="relationId">��ϵ����</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataAuthorizeConditionList(string relationId)
        {
            var data = dataAuthorizeIBLL.GetDataAuthorizeConditionList(relationId);
            return Success(data);
        }
        /// <summary>
        /// ��ȡ����Ȩ�޶�Ӧ��ϵ�����б�
        /// <param name="pagination">��ҳ����</param>
        /// <param name="interfaceId">�ӿ�����</param>
        /// <param name="keyword">��ѯ�ؼ���</param>
        /// <param name="objectId">��������</param>
        /// <summary>
        /// <returns></returns>
        public ActionResult GetRelationPageList(string pagination, string interfaceId, string keyword, string objectId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = dataAuthorizeIBLL.GetRelationPageList(paginationobj, interfaceId, keyword, objectId);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <param name="relationId">��ϵ����</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataAuthorizeEntity(string keyValue)
        {
            var relationEntity = dataAuthorizeIBLL.GetRelationEntity(keyValue);
            var conditionEntity = dataAuthorizeIBLL.GetDataAuthorizeConditionList(keyValue);
            var jsonData = new
            {
                relationEntity = relationEntity,
                conditionEntity = conditionEntity
            };

            return Success(jsonData);
        } 

        #endregion

        #region �ύ����
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="keyValue">����</param>
        /// <param name="relation">��Ӧ��ϵ����</param>
        /// <param name="conditions">��������</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string relation, string conditions)
        {
            DataAuthorizeRelationEntity relationEntity = relation.ToObject<DataAuthorizeRelationEntity>();
            List<DataAuthorizeConditionEntity> conditionEntityList = conditions.ToObject<List<DataAuthorizeConditionEntity>>();

            dataAuthorizeIBLL.SaveEntity(keyValue, relationEntity, conditionEntityList);
            return Success("����ɹ���");
        }
        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            dataAuthorizeIBLL.DeleteEntity(keyValue);
            return Success("ɾ���ɹ���");
        }
        #endregion
    }
}
