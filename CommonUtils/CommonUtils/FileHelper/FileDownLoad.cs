using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CommonUtils.Logger;

namespace CommonUtils.FileHelper
{
    class FileDownLoad
    {
        public static bool DownLoad(string url, string path, string contentType)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.ServicePoint.Expect100Continue = false;
                req.Method = "GET";
                req.KeepAlive = true;
                req.ContentType = contentType;// "image/png";
                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                {
                    using (Stream reader = rsp.GetResponseStream())
                    {


                        using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            byte[] buff = new byte[512];
                            int c = 0; //实际读取的字节数
                            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                            {
                                writer.Write(buff, 0, c);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Log.Error("HttpRequestHelper.DownLoad"+e.Message+e.StackTrace);
                return false;
            }
        }

        //第二种方式

        public static bool DownLoad(string url, string path)

        {
            try
            {
                WebClient mywebclient = new WebClient();
                string direcotry = path.Substring(0, path.LastIndexOf('/'));
                if (!System.IO.Directory.Exists(direcotry))
                    System.IO.Directory.CreateDirectory(direcotry);
                mywebclient.DownloadFile(url, path);
            }
            catch (Exception e)
            {
                LogHelper.Log.Error("HttpRequestHelper.DownLoad"+e.Message+e.StackTrace);
                return false;
            }
            return true;
        }
    }
}
