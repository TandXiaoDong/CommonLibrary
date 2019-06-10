using System;
using System.IO;
using System.Text;
//using System.Windows.Media.Imaging;
using System.Linq;
using CommonUtils.Logger;

namespace CommonUtils.FileHelper
{

    /// <summary>
    /// 关于文件工具类
    /// </summary>
    public static class FileUtil
    {

        #region 检查文件是否存在
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path">需要检查的路径</param>
        /// <returns>存在则true,不存在则false</returns>
        internal static bool CheckFileExists(string path)
        {
            return File.Exists(path);
        }
        #endregion

        #region 流转字节数组
        /// <summary>
        /// 流转字节数组
        /// </summary>
        /// <param name="instream">需要转换的流</param>
        /// <param name="streamLength">流的长度</param>
        /// <returns>转换好的字节数组</returns>
        public static byte[] StreamToBytes(Stream instream, int streamLength = 0)
        {
            #region 首先将网络流转换为内存流
            //首先将网络流转换为内存流
            MemoryStream outstream = new MemoryStream();
            //
            int bufferLen = streamLength;
            byte[] buffer = new byte[bufferLen];
            int count = 0;
            while ((count = instream.Read(buffer, 0, bufferLen)) > 0)
            {
                outstream.Write(buffer, 0, count);
            }
            #endregion


            byte[] bytes = null;
            //实例化一个新的字节数组
            bytes = new byte[streamLength];
            outstream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            outstream.Seek(0, SeekOrigin.Begin);
            //返回
            return bytes;
        }
        #endregion

        #region 删除所有头像
        /// <summary>
        /// 删除所有头像
        /// </summary>
        public static void DeleteHeadImg(string UserId)
        {
            try
            {
                //Directory.Delete(Applicate.LocalConfigData.UserAvatorFolderPath, true);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("删除头像(删除所有头像)时出错" + ex.Message, ex);
            }
            //删除所有头像
        }
        #endregion

        #region 删除用户头像
        /// <summary>
        /// 删除用户头像
        /// </summary>
        public static void DeleteUserHeadImg(string UserId)
        {
            try
            {
                //Task.Run(() =>
                //{
                string fullPath = "";//Applicate.LocalConfigData.GetDownloadAvatorPath(UserId);//chatFileTmpPath + "Head/" + userId + ".jpg";//定义头像存放路径
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);//删除头像
                }
                //});
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("删除头像(删除用户头像)时出错" + ex.Message, ex);
            }
        }
        #endregion

        #region 读取文件字节
        /// <summary>
        /// 读取文件字节
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadFileToBytes(string path)
        {
            BinaryReader binReader = new BinaryReader(File.OpenRead(path));
            FileInfo fileInfo = new FileInfo(path);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();
            binReader.Dispose();
            return bytes;
        }
        #endregion

        #region 将图片读取为byte再转为BitmapImage
        /// <summary>
        /// 将图片读取为byte再转为BitmapImage
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns>内存中的图片</returns>
        //public static BitmapImage ReadFileByteToBitmap(string filePath)
        //{
        //    var bitmap = new BitmapImage();
        //    try
        //    {
        //        BinaryReader binReader = new BinaryReader(File.OpenRead(filePath));
        //        FileInfo fileInfo = new FileInfo(filePath);
        //        byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
        //        binReader.Close();
        //        binReader.Dispose();
        //        // Init bitmap
        //        bitmap.BeginInit();
        //        bitmap.StreamSource = new MemoryStream(bytes);
        //        bitmap.EndInit();
        //    }
        //    catch (Exception ex)
        //    {
        //        ConsoleLog.Output("将头像复制到内存时出错：" + ex.Message);
        //        return null;
        //    }
        //    return bitmap;
        //}
        #endregion

        #region 转Stream为byte
        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        #endregion

        #region 转byte为Stream
        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion

        #region 字节数组存文件
        /// <summary>
        /// 字节数组存文件
        /// </summary>
        /// <param name="bytes">字节</param>
        /// <param name="path">路径</param>
        public static void ByteToFile(this byte[] bytes, string path)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region 文件转为byte数组
        /// <summary>  
        /// 文件转为byte数组
        /// </summary>  
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>  
        private static byte[] FileTojByte(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex.Message);
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源  
                    fs.Close();
                }
            }
        }
        #endregion

        #region private void SearchFile(string startdir, string filter, string enddir) 文件模糊查询

        public static string GetDirFiles(string DirFullPath,string strFilter)
        {
            string[] FileList = null;
            if (Directory.Exists(DirFullPath) == true)
            {
                FileList = Directory.GetFiles(DirFullPath, "*.*", SearchOption.TopDirectoryOnly);
                return GetFullName(FileList, strFilter);
            }
            return "";
        }

        public static string GetFullName(string[] fileList, string strFilter)
        {
            foreach (string fstr in fileList)
            {
                if (fstr.Contains(strFilter))
                {
                    return fstr;
                }
            }
            return strFilter+".png";
        }
        #endregion
    }
}
