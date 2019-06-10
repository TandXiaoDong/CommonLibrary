using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace CommonUtils.FileHelper
{
    public class FileContent
    {
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 选择的所有文件的绝对路径
        /// </summary>
        public string[] FileNames { get; set; }

        /// <summary>
        /// 文件名+扩展名，不包含路径
        /// </summary>
        public string FileSafeName { get; set; }

        /// <summary>
        /// 选择的所有文件名+扩展名，不包含路径
        /// </summary>
        public string[] FileSafeNames { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string FileFilter { get; set; }

        /// <summary>
        /// 过滤条件索引
        /// </summary>
        public int FileFilterIndex { get; set; }

        public OpenFileDialog OpenFileResult { get; set; }
    }
    public class FileSelect
    {
        string filterImage = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";//图像过滤.

        

        /// <summary>
        /// 打开选择文件对话框，返回选中文件的绝对路径
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static FileContent GetSelectFileContent(string fileType, string tip)
        {
            FileContent fileProperty = new FileContent();
            ///获取完整路径名
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = fileType;//"(*.exe)|*.exe"
            //openFileDialog1.FileName = "Mysoft.CRE.WindowService.exe";
            openFileDialog1.Title = tip;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileProperty.FileName = openFileDialog1.FileName;
                fileProperty.FileSafeName = openFileDialog1.SafeFileName;
                fileProperty.FileFilter = openFileDialog1.Filter;
                fileProperty.FileFilterIndex = openFileDialog1.FilterIndex;
                fileProperty.FileNames = openFileDialog1.FileNames;
                fileProperty.FileSafeNames = openFileDialog1.SafeFileNames;
                fileProperty.FileFilterIndex = 1;
                fileProperty.OpenFileResult = openFileDialog1;
                return fileProperty;
            }
            else
                return null;
        }

        /// <summary>
        /// 打开选择文件对话框，返回选中文件名+扩展名
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static string GetSelectFileSafeName(string fileType, string tip)
        {
            ///获取完整路径名
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = fileType;//"(*.exe)|*.exe"
            //openFileDialog1.FileName = "Mysoft.CRE.WindowService.exe";
            openFileDialog1.Title = tip;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.SafeFileName;
            }
            else
                return "";
        }

        /// <summary>
        /// 打开文件夹对话框，返回选中文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetDirectorPath()
        {
            string path = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            return path;
        }

        public static string SaveAs(string filter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "";
            sfd.InitialDirectory = @"C:\";
            sfd.Filter = filter;
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                return sfd.FileName;
            }
            //using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    byte[] buffer = Encoding.Default.GetBytes("content");
            //    fsWrite.Write(buffer, 0, buffer.Length);
            //}
            return "";
        }
    }
}
