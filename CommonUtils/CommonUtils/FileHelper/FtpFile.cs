using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace EquipmentClient.Ftp
{
    class FtpFile
    {
        private string _username;
        private string _password;

        public static void DownloadByFtp(string uri, MemoryStream stream, bool isInternalAuth, string sftpUserName, string sftpPassword)
        {
            if (!uri.Contains("ftp://"))
                uri = "ftp://" + uri;

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            if (!isInternalAuth)
                request.Credentials = new NetworkCredential(sftpUserName, sftpPassword);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            int len = 0;

            do
            {
                byte[] buffer = new byte[2048];
                len = responseStream.Read(buffer, 0, buffer.Length);
                stream.Write(buffer, 0, len);
            } while (len > 0);

            responseStream.Close();
            response.Close();

            stream.Position = 0;
        }


        public static void UploadByFtp(string ftpAddress, MemoryStream fileStream, string folderName, string fileName, string sftpUsername, string sftpPassword)
        {
            string uri = string.Empty;
            if (ftpAddress.Contains("ftp://"))
            {
                uri = ftpAddress;
            }
            else
            {
                uri = "ftp://" + ftpAddress;
            }
            if (!string.IsNullOrEmpty(folderName))
            {
                uri = uri.TrimEnd('/') + "/" + folderName;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                uri = uri.TrimEnd('/') + "/" + fileName;
            }
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            request.Credentials = new NetworkCredential(sftpUsername, sftpPassword);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.ContentLength = fileStream.Length;

            byte[] buffer = fileStream.GetBuffer();
            Stream stream = request.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
        }
        ////another
        /// <summary>
        /// upload the file to ftp server.
        /// </summary>
        public void UploadByFtp(string ftpAddress, string filePath, string name)
        {
            FileStream fs = null;
            Stream stream = null;
            string uri = string.Empty;
            FileInfo fi = new FileInfo(filePath);

            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress + name;
            else
                uri = "ftp://" + ftpAddress + name;

            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(_username, _password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.ContentLength = fi.Length;

                byte[] buffer = new byte[2048];
                fs = fi.OpenRead();
                stream = request.GetRequestStream();
                int len = fs.Read(buffer, 0, buffer.Length);
                while (len != 0)
                {
                    stream.Write(buffer, 0, len);
                    len = fs.Read(buffer, 0, buffer.Length);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// upload the file to ftp server. only for cover image process.
        /// </summary>
        public void UploadByFtp(string ftpAddress, MemoryStream fileStream, string fileName)
        {
            string uri = string.Empty;
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress;
            else
                uri = "ftp://" + ftpAddress;
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + fileName));
            request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.ContentLength = fileStream.Length;

            byte[] buffer = fileStream.GetBuffer();
            Stream stream = request.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
        }

        /// <summary>
        /// delete the file from ftp server by ftp protocol.
        /// </summary>
        public void DeleteByFtp(string ftpAddress, string fileName)
        {
            string uri = string.Empty;
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress + fileName;
            else
                uri = "ftp://" + ftpAddress + fileName;
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            request.GetResponse().Close();
        }

        public void DownloadByFtp(string uri, MemoryStream stream, bool isInternalAuth)
        {
            if (!uri.Contains("ftp://"))
                uri = "ftp://" + uri;

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            if (!isInternalAuth)
                request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            int len = 0;

            do
            {
                byte[] buffer = new byte[2048];
                len = responseStream.Read(buffer, 0, buffer.Length);
                stream.Write(buffer, 0, len);
            } while (len > 0);

            responseStream.Close();
            response.Close();

            stream.Position = 0;
        }

        /// <summary>
        /// create the directory on ftp server.
        /// </summary>
        public void CreateDirectory(string ftpAddress, string directory)
        {
            string uri = string.Empty;
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress;
            else
                uri = "ftp://" + ftpAddress;
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + directory));
            request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;

            request.GetResponse().Close();
        }

        /// <returns>0: successful; 1: can not delete(e.g. have sub-dirs or sub-files); -1: failure</returns>
        public int RemoveDirectory(string ftpAddress, string directory)
        {
            int result;
            string uri = string.Empty;
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress;
            else
                uri = "ftp://" + ftpAddress;
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + directory));
            request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;

            try
            {
                request.GetResponse().Close();

                result = 0;
            }
            catch (WebException we)
            {
                if (((FtpWebResponse)we.Response).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                    throw we;
                }
            }
            catch (Exception e)
            {
                result = 1;
                throw e;
            }

            return result;
        }

        public string[] GetFileList(string ftpAddress, string directory, bool isInternalAuth)
        {
            string uri = string.Empty;
            StringBuilder result = new StringBuilder();
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress;
            else
                uri = "ftp://" + ftpAddress;

            FtpWebRequest request = null;
            if (!string.IsNullOrEmpty(directory))
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + directory));
            else
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            if (!isInternalAuth)
                request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string line = reader.ReadLine();
            while (line != null)
            {
                result.Append(line);
                result.Append("/");
                line = reader.ReadLine();
            }

            reader.Close();
            response.Close();

            if (result.Length > 0)
                return result.ToString().TrimEnd('/').Split('/');
            else
                return null;
        }

        /// <returns>0: exist; -1: not exist; 1: unknown (other cause)</returns>
        public int IsExist(string ftpAddress, string directory)
        {
            string uri = string.Empty;
            int result;
            if (ftpAddress.Contains("ftp://"))
                uri = ftpAddress;
            else
                uri = "ftp://" + ftpAddress;

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + directory));
            request.Credentials = new NetworkCredential(_username, _password);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            try
            {
                request.GetResponse().Close();

                result = 0;
            }
            catch (WebException we)
            {
                if (((FtpWebResponse)we.Response).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                    throw we;
                }
            }
            catch (Exception e)
            {
                result = 1;
                throw e;
            }

            return result;
        }
    }
}
