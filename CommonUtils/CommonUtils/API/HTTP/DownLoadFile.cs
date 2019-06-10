using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtils.API.HTTP
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum DownLoadFileType
    {
        /// <summary>
        /// 图片类型
        /// </summary>
        Image,

    }


    /// <summary>
    /// 下载状态
    /// </summary>
    public enum DownloadState
    {
        /// <summary>
        /// 下载完成
        /// </summary>
        Successed,

        /// <summary>
        /// 下载错误
        /// </summary>
        Error
    }


    /// <summary>
    /// 
    /// </summary>
    public class DownLoadFile
    {

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于文件验证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户Jid
        /// </summary>
        public string Jid { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 需要下载文件的网络Uri
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 如果文件存在时是否先删除并重新下载
        /// </summary>
        public bool ShouldDeleteWhileFileExists { get; set; }

        /// <summary>
        /// 下载的文件类型
        /// </summary>
        public DownLoadFileType Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DownloadState State { get; set; }

        /// <summary>
        /// 下载到本地的路径
        /// </summary>
        public string LocalUrl { get; internal set; }
    }
}
