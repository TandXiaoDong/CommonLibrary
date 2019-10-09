using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CommonUtils.Logger;

namespace CommonUtils.tool
{
    public class Execute
    {
        public static bool ExecuteApply(string targetPath,string executeFileName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.WorkingDirectory = targetPath;
                process.StartInfo.FileName = executeFileName;
                //process.StartInfo.UseShellExecute = true;        // 使用操作系统shell启动进程
                //process.StartInfo.Arguments = "";
                //process.StartInfo.UseShellExecute = false;//是否重定向标准输入 
                //process.StartInfo.RedirectStandardInput = false;//是否重定向标准转出 
                //process.StartInfo.RedirectStandardOutput = false;//是否重定向错误 
                process.StartInfo.RedirectStandardError = false;//执行时是不是显示窗口 
                process.StartInfo.CreateNoWindow = true;//启动 
                bool b = process.Start();
                process.WaitForExit();
                process.Close();
                if (b)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"执行可执行文件失败！路径={targetPath}\r\n Err="+ex.Message+ex.StackTrace);
                return false;
            }
        }
    }
}
