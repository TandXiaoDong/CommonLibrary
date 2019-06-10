using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommonUtils.FileHelper
{
    public static class AbsAndRelPath
    {
        #region 绝对路径转相对路径
        /// <summary>
        /// 绝对路径转相对路径
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="relativeTo">相对路径</param>
        /// <returns>转换好的相对路径</returns>
        public static string RelativePath(string absolutePath, string relativeTo)
        {
            //from - www.cnphp6.com
            string[] absoluteDirectories = absolutePath.Split('\\');
            string[] relativeDirectories = relativeTo.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
            {
                if (absoluteDirectories[index] == relativeDirectories[index])
                {
                    lastCommonRoot = index;
                }
                else
                {
                    break;
                }
            }

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
            {
                if (absoluteDirectories[index].Length > 0)
                {
                    relativePath.Append("..\\");
                }
            }

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
            {
                relativePath.Append(relativeDirectories[index] + "\\");
            }

            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }
        #endregion

        #region 相对路径转绝对路径
        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="relativeTo"></param>
        /// <returns></returns>
        public static string AbsolutePath(string relativeTo)
        {
            //Directory.SetCurrentDirectory(relativeTo);
            return Path.GetFullPath(relativeTo);
        }
        #endregion
    }
}
