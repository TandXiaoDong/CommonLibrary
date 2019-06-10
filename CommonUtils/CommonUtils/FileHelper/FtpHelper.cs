using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentClient.Ftp
{
    public class FtpHelper
    {
        #region 属性与构造函数

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddr { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelatePath { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

       

        public FtpHelper() {

        }

        public FtpHelper(string ipAddr, string port, string userName, string password) {
            this.IpAddr = ipAddr;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
        }

        #endregion

        #region 方法


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isOk"></param>
        public void DownLoad(string filePath, out bool isOk) {
            string method = WebRequestMethods.Ftp.DownloadFile;
            var statusCode = FtpStatusCode.DataAlreadyOpen;
            FtpWebResponse response = callFtp(method);
            ReadByBytes(filePath, response, statusCode, out isOk);
        }

        public void UpLoad(string file,out bool isOk)
        {
            isOk = false;
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            long length = fs.Length;
            string uri = string.Format("ftp://{0}:{1}{2}", this.IpAddr, this.Port, this.RelatePath);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Credentials = new NetworkCredential(UserName, Password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.ContentLength = length;
            request.Timeout = 10 * 1000;
            try
            {
                Stream stream = request.GetRequestStream();

                int BufferLength = 2048; //2K   
                byte[] b = new byte[BufferLength];
                int i;
                while ((i = fs.Read(b, 0, BufferLength)) > 0)
                {
                    stream.Write(b, 0, i);
                }
                stream.Close();
                stream.Dispose();
                isOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally {
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="isOk"></param>
        /// <returns></returns>
        public string[] DeleteFile(out bool isOk) {
            string method = WebRequestMethods.Ftp.DeleteFile;
            var statusCode = FtpStatusCode.FileActionOK;
            FtpWebResponse response = callFtp(method);
            return ReadByLine(response, statusCode, out isOk);
        }

        /// <summary>
        /// 展示目录
        /// </summary>
        public string[] ListDirectory(out bool isOk)
        {
            string method = WebRequestMethods.Ftp.ListDirectoryDetails;
            var statusCode = FtpStatusCode.DataAlreadyOpen;
            FtpWebResponse response= callFtp(method);
            return ReadByLine(response, statusCode, out isOk);
        }

        /// <summary>
        /// 设置上级目录
        /// </summary>
        public void SetPrePath()
        {
            string relatePath = this.RelatePath;
            if (string.IsNullOrEmpty(relatePath) || relatePath.LastIndexOf("/") == 0 )
            {
                relatePath = "";
            }
            else {
                relatePath = relatePath.Substring(0, relatePath.LastIndexOf("/"));
            }
            this.RelatePath = relatePath;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 调用Ftp,将命令发往Ftp并返回信息
        /// </summary>
        /// <param name="method">要发往Ftp的命令</param>
        /// <returns></returns>
        private FtpWebResponse callFtp(string method)
        {
            string uri = string.Format("ftp://{0}:{1}{2}", this.IpAddr, this.Port, this.RelatePath);
            FtpWebRequest request; request = (FtpWebRequest)FtpWebRequest.Create(uri);
            request.UseBinary = true;
            request.UsePassive = true;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.KeepAlive = false;
            request.Method = method;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response;
        }

        /// <summary>
        /// 按行读取
        /// </summary>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        /// <param name="isOk"></param>
        /// <returns></returns>
        private string[] ReadByLine(FtpWebResponse response, FtpStatusCode statusCode,out bool isOk) {
            List<string> lstAccpet = new List<string>();
            int i = 0;
            while (true)
            {
                if (response.StatusCode == statusCode)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        string line = sr.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            lstAccpet.Add(line);
                            line = sr.ReadLine();
                        }
                    }
                    isOk = true;
                    break;
                }
                i++;
                if (i > 10)
                {
                    isOk = false;
                    break;
                }
                Thread.Sleep(200);
            }
            response.Close();
            return lstAccpet.ToArray();
        }

        private void ReadByBytes(string filePath,FtpWebResponse response, FtpStatusCode statusCode, out bool isOk)
        {
            isOk = false;
            int i = 0;
            while (true)

            {
                if (response.StatusCode == statusCode)
                {
                    long length = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];
                    using (FileStream outputStream = new FileStream(filePath, FileMode.Create))
                    {

                        using (Stream ftpStream = response.GetResponseStream())
                        {
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                            {
                                outputStream.Write(buffer, 0, readCount);
                                readCount = ftpStream.Read(buffer, 0, bufferSize);
                            }
                        }
                    }
                    break;
                }
                i++;
                if (i > 10)
                {
                    isOk = false;
                    break;
                }
                Thread.Sleep(200);
            }
            response.Close();
        }
        #endregion
    }

    /// <summary>
    /// Ftp内容类型枚举
    /// </summary>
    public enum FtpContentType
    {
        undefined = 0,
        file = 1,
        folder = 2
    }
}
