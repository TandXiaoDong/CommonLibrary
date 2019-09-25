using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.Util.Offices;
using LeaRun.Application.Code;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Entity.PublicInfoManage;
using System.Threading;
using System.IO;
namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2016.2.03 10:58
    /// 描 述：公共控制器
    /// </summary>
    public class UtilityController : Controller
    {
        FileInfoBLL fileInfoBLL = new FileInfoBLL();
        public ActionResult getCurrentInfo()
        {
            var jsonData = new
            {
                userId = OperatorProvider.Provider.Current().UserId,
                userName = OperatorProvider.Provider.Current().UserName,
                companyId = OperatorProvider.Provider.Current().CompanyId,
                departmentId = OperatorProvider.Provider.Current().DepartmentId,
                time=System.DateTime.Now,
            };
            return Content(jsonData.ToJson());
        }
        public ActionResult GetFiles(string fileIdlist)
        {
            if (!string.IsNullOrEmpty(fileIdlist)&&fileIdlist!="undefined")
            {
                FileInfoEntity entity = fileInfoBLL.GetEntity(fileIdlist);
                //return Content(entity.ToJson());
                string json = "[{\"FileId\":\"" + entity.FileId + "\",\"FileType\":\"" + entity.FileType + "\",\"FileName\":\"" + entity.FileName + "\"}]";
                return Content(json);
            }
            return Content("[]");
        }
        public ActionResult RemoveFile(string fileId)
        {
            if (!string.IsNullOrEmpty(fileId) && fileId != "undefined")
            {

                FileInfoEntity entity = fileInfoBLL.GetEntity(fileId);
                if (System.IO.File.Exists(Server.MapPath(entity.FilePath)))
                {
                    System.IO.File.Delete(Server.MapPath(entity.FilePath));
                }
                
            }
            return Content("[{\"code\":\"1\"}]");
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public void DownFile(string fileId)
        {
            var data = fileInfoBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.FileName);//返回客户端文件名称
            string filepath = this.Server.MapPath(data.FilePath);
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="Filedata">文件对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadifyFile(string DataItemName, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string userId = OperatorProvider.Provider.Current().UserId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("~/Resource/" + DataItemName + "/{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                FileInfoEntity fileInfoEntity = new FileInfoEntity();
                if (!System.IO.File.Exists(fullFileName))
                {
                    //保存文件
                    Filedata.SaveAs(fullFileName);
                    //文件信息写入数据库
                   
                    fileInfoEntity.Create();
                    fileInfoEntity.FileId = fileGuid;
                    //if (!string.IsNullOrEmpty(folderId))
                    //{
                    //    fileInfoEntity.FolderId = folderId;
                    //}
                    //else
                    //{
                        fileInfoEntity.FolderId = "0";
                    //}
                    fileInfoEntity.FileName = Filedata.FileName;
                    fileInfoEntity.FilePath = virtualPath;
                    fileInfoEntity.FileSize = filesize.ToString();
                    fileInfoEntity.FileExtensions = FileEextension;
                    fileInfoEntity.FileType = FileEextension.Replace(".", "");
                    fileInfoBLL.SaveForm("", fileInfoEntity);
                }
                return Content("{\"fileId\":\"" + fileInfoEntity.FileId + "\"}");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        #region 导出Excel
        /// <summary>
        /// 请选择要导出的字段页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelExportForm()
        {
            return View();
        }
        /// <summary>
        /// 执行导出Excel
        /// </summary>
        /// <param name="JsonColumn">表头</param>
        /// <param name="JsonData">数据</param>
        /// <param name="exportField">导出字段</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public void ExecuteExportExcel(string columnJson, string rowJson, string exportField, string filename)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = Server.UrlDecode(filename);
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 15;
            excelconfig.FileName = Server.UrlDecode(filename) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnEntity>();
            //表头
            List<GridColumnModel> columnData = columnJson.ToList<GridColumnModel>();
            //行数据
            DataTable rowData = rowJson.ToTable();
            //写入Excel表头
            string[] fieldInfo = exportField.Split(',');
            foreach (string item in fieldInfo)
            {
                var list = columnData.FindAll(t => t.name == item);
                foreach (GridColumnModel gridcolumnmodel in list)
                {
                    if (gridcolumnmodel.hidden.ToLower() == "false" && gridcolumnmodel.label != null)
                    {
                        string align = gridcolumnmodel.align;
                        excelconfig.ColumnEntity.Add(new ColumnEntity()
                        {
                            Column = gridcolumnmodel.name,
                            ExcelColumn = gridcolumnmodel.label,
                            //Width = gridcolumnmodel.width,
                            Alignment = gridcolumnmodel.align,
                        });
                    }
                }
            }
            ExcelHelper.ExcelDownload(rowData, excelconfig);
        }
        #endregion
    }
}
