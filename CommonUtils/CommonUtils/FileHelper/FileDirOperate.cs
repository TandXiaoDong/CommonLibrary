using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace CommonUtils.FileHelper
{
    public static class FileDirOperate
    {

        #region 获取文件扩展名
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        public static string GetExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }
        #endregion

        #region 获取视频文件类型
        static string[] strArrFfmpeg = new string[] { "asf", "avi", "mpg", "3gp", "mov" };
        static string[] strArrMencoder = new string[] { "wmv", "rmvb", "rm" };
        /// <summary>
        /// 获取视频文件类型
        /// </summary>
        public static string CheckExtension(string extension)
        {
            string m_strReturn = "";
            foreach (string var in strArrFfmpeg)
            {
                if (var == extension)
                {
                    m_strReturn = "ffmpeg"; break;
                }
            }
            if (m_strReturn == "")
            {
                foreach (string var in strArrMencoder)
                {
                    if (var == extension)
                    {
                        m_strReturn = "mencoder"; break;
                    }
                }
            }
            return m_strReturn;
        }
        #endregion

        #region 根据全路径删除文件
        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="FileFullPath">要删除的文件全路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                File.SetAttributes(FileFullPath, FileAttributes.Normal);
                File.Delete(FileFullPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Gets the name of the file.包括文件的扩展名
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns></returns>
        public static string GetFileName(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <param name="IncludeExtension">是否包含文件的扩展名</param>
        /// <returns></returns>
        public static string GetFileName(string FileFullPath, bool IncludeExtension)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                if (IncludeExtension == true)
                {
                    return F.Name;
                }
                else
                {
                    return F.Name.Replace(F.Extension, "");
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到文件的大小
        /// </summary>
        /// <param name="info">FileInfo</param>
        /// <returns></returns>
        public static String getFileSize(FileInfo info)
        {

            if (info.Exists == true)
            {

                long FL = info.Length;
                if (FL > 1024 * 1024 * 1024)
                {
                    //   KB      MB    GB   TB
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                else if (FL > 1024 * 1024)
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                else
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / 1024, 2)) + " KB";
                }
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 得到文件的后缀名
        /// </summary>
        /// <param name="info">FileInfo</param>
        /// <returns></returns>
        public static String getFileExtension(FileInfo info)
        {
            if (info.Exists == true)
            {
                String extension = info.Extension;
                return extension;//.Substring(1);
                                 // return extension.Substring(1, extension.Length - 1);

            }
            else
            {

                return null;
            }

        }
        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static string GetFileExtension(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Extension;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static bool OpenFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                System.Diagnostics.Process.Start(FileFullPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static string GetFileSize(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                long FL = F.Length;
                if (FL > 1024 * 1024 * 1024)
                {
                    //   KB      MB    GB   TB
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                else if (FL > 1024 * 1024)
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                else
                {
                    return System.Convert.ToString(Math.Round((FL + 0.00) / 1024, 2)) + " KB";
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Files to stream byte.
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static byte[] FileToStreamByte(string FileFullPath)
        {
            byte[] fileData = null;
            if (File.Exists(FileFullPath) == true)
            {
                FileStream FS = new FileStream(FileFullPath, System.IO.FileMode.Open);
                fileData = new byte[FS.Length];
                FS.Read(fileData, 0, fileData.Length);
                FS.Close();
                return fileData;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Bytes the stream to file.
        /// </summary>
        /// <param name="CreateFileFullPath">The create file full path.</param>
        /// <param name="StreamByte">The stream byte.</param>
        /// <returns></returns>
        public static bool ByteStreamToFile(string CreateFileFullPath, byte[] StreamByte)
        {
            try
            {
                if (File.Exists(CreateFileFullPath) == true)
                {
                    DeleteFile(CreateFileFullPath);
                }
                FileStream FS;
                FS = File.Create(CreateFileFullPath);
                FS.Write(StreamByte, 0, StreamByte.Length);
                FS.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static bool SerializeXmlFile(string FileFullPath)
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();
                DS.ReadXml(FileFullPath);
                FileStream FS = new FileStream(FileFullPath + ".tmp", FileMode.OpenOrCreate);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                FT.Serialize(FS, DS);
                FS.Close();
                DeleteFile(FileFullPath);
                File.Move(FileFullPath + ".tmp", FileFullPath);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">The file full path.</param>
        /// <returns></returns>
        public static bool DeserializeXmlFile(string FileFullPath)
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();
                FileStream FS = new FileStream(FileFullPath, FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                ((System.Data.DataSet)FT.Deserialize(FS)).WriteXml(FileFullPath + ".tmp");
                FS.Close();
                DeleteFile(FileFullPath);
                File.Move(FileFullPath + ".tmp", FileFullPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 得到文件的创建时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static String getFileCreateTime(FileInfo info)
        {

            return info.CreationTime.ToString();
        }
        /// <summary>
        /// 得到文件最后一次修改时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static String getFileLastModifyTime(FileInfo info)
        {
            return info.LastWriteTime.ToString();

        }
    }

    /// <summary>
    /// 与文件夹有关的操作类
    /// </summary>
    public class DirOperate
    {
        public enum OperateOption
        {
            /// <summary>
            /// 存在删除再创建
            /// </summary>
            ExistDelete,
            /// <summary>
            /// 存在直接返回
            /// </summary>
            ExistReturn
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <param name="DirOperateOption">The dir operate option.</param>
        /// <returns></returns>
        public bool CreateDir(string DirFullPath, OperateOption DirOperateOption)
        {
            try
            {
                if (Directory.Exists(DirFullPath) == false)
                {
                    Directory.CreateDirectory(DirFullPath);
                }
                else if (DirOperateOption == OperateOption.ExistDelete)
                {
                    Directory.Delete(DirFullPath, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <returns>成功则为True 否则为False</returns>
        public bool DeleteDir(string DirFullPath)
        {
            if (Directory.Exists(DirFullPath) == true)
            {
                Directory.Delete(DirFullPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the dir files.
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <returns></returns>
        public string[] GetDirFiles(string DirFullPath)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath) == true)
            {
                FileList = Directory.GetFiles(DirFullPath, "*.*", SearchOption.TopDirectoryOnly);

            }
            return FileList;
        }

        /// <summary>
        /// Gets the dir files.
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <param name="SO">The SO.</param>
        /// <returns></returns>
        public string[] GetDirFiles(string DirFullPath, SearchOption SO)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath) == true)
            {
                FileList = Directory.GetFiles(DirFullPath, "*.*", SO);
            }
            return FileList;
        }
        ArrayList filelist = new ArrayList();
        public ArrayList getDirFiles(String DirFullpath, String pattern)
        {

            if (Directory.Exists(DirFullpath))
            {
                DirectoryInfo inf = new DirectoryInfo(DirFullpath);

                FileSystemInfo[] infos = inf.GetFileSystemInfos();
                foreach (FileSystemInfo info in infos)
                {
                    if (info is FileInfo)

                    {
                        if (info.Name.Contains(pattern))
                            filelist.Add(info.FullName);
                    }
                    else
                    {
                        if (info.Name.Contains(pattern))
                            filelist.Add(info.FullName);
                        getDirFiles(info.FullName, pattern);
                    }
                }
            }
            return filelist;

        }
        /// <summary>
        /// Gets the dir files.
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <param name="SearchPattern">The search pattern.</param>
        /// <returns>所有文件</returns>
        public string[] GetDirFiles(string DirFullPath, string SearchPattern)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath) == true)
            {
                FileList = Directory.GetFiles(DirFullPath, SearchPattern);
            }
            return FileList;
        }

        /// <summary>
        /// Gets the dir files.
        /// </summary>
        /// <param name="DirFullPath">The dir full path.</param>
        /// <param name="SearchPattern">The search pattern.</param>
        /// <param name="SO">The SO.</param>
        /// <returns>与当前条件匹配的所有文件和文件夹</returns>
        public string[] GetDirFiles(string DirFullPath, string SearchPattern, SearchOption SO)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath) == true)
            {
                FileList = Directory.GetFiles(DirFullPath, SearchPattern, SO);
            }
            return FileList;
        }
        /// <summary>
        /// 得到文件的创建时间
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns>文件的创建时间</returns>
        public String getFileCreateTime(String FileFullPath)
        {
            FileInfo info = new FileInfo(FileFullPath);
            if (info.Exists)
            {
                return info.CreationTime.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到文件最后一次修改的时间
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns>文件的最后修改时间</returns>
        public String getFileLastModifyTime(String FileFullPath)
        {

            if (File.Exists(FileFullPath))
            {
                return new FileInfo(FileFullPath).LastWriteTime.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到当前目录下的子目录或文件
        /// </summary>
        /// <param name="FileFullPath">目录或文件的完整路径</param>
        /// <returns>当前目录的所有子目录和子文件</returns>
        public FileSystemInfo[] getFileSystemInfo(String FileFullPath)
        {
            if (Directory.Exists(FileFullPath))
            {
                DirectoryInfo info = new DirectoryInfo(FileFullPath);
                return info.GetFileSystemInfos();
            }
            else
            {

                return null;
            }

        }
        /// <summary>
        /// 得到文件的创建时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public String getDirCreationTime(DirectoryInfo info)
        {

            return info.CreationTime.ToString();
        }
        /// <summary>
        /// 得到文件最后一次修改的时间
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public String getDirLastModifyTime(DirectoryInfo info)
        {

            return info.LastWriteTime.ToString();
        }
        /// <summary>
        /// 保存文件夹的大小
        /// </summary>
        private long length = 0;
        /// <summary>
        /// 获得文件夹的大小
        /// </summary>
        /// <param name="info">文件夹实例</param>
        /// <returns>文件夹大小</returns>
        public long getDirSize(DirectoryInfo info)
        {

            if (info.Exists)
            {
                FileSystemInfo[] infos = info.GetFileSystemInfos();
                foreach (FileSystemInfo inf in infos)//循环每一个目录里的每一个文件得到总的文件夹的大小
                {

                    if (inf is DirectoryInfo)
                    {

                        length = +getDirSize((DirectoryInfo)inf);
                    }

                    else
                    {
                        length += ((FileInfo)inf).Length;
                        //return length;
                    }

                }
                return length;
            }
            else
            {
                return 0;
            }


        }
        /// <summary>
        /// 循环得到文件夹的大小
        /// </summary>
        /// <param name="info">文件夹实例</param>
        /// <returns>文件夹的大小</returns>
        public String getDirSizes(DirectoryInfo info)
        {
            long FL = 0;
            FL += getDirSize(info);
            length = 0;
            if (FL > 1024 * 1024 * 1024)//将得到的位化为 KB/MB /GB 
            {
                //   KB      MB    GB   TB
                return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2)) + " GB" + "  (" + FL + " 字节)";
            }
            else if (FL > 1024 * 1024)
            {
                return System.Convert.ToString(Math.Round((FL + 0.00) / (1024 * 1024), 2)) + " MB" + "  (" + FL + " 字节)";
            }
            else
            {
                return System.Convert.ToString(Math.Round((FL + 0.00) / 1024, 2)) + " KB" + "  (" + FL + " 字节)";
            }
        }
    }
}
