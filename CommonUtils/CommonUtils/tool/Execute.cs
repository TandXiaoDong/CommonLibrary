using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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

                // 当我们需要给可执行文件传入参数时候可以设置这个参数
                // "para1 para2 para3" 参数为字符串形式，每一个参数用空格隔开
                //process.StartInfo.Arguments = "para1 para2 para3";
                //process.StartInfo.UseShellExecute = true;        // 使用操作系统shell启动进程
                                                                 // 启动可执行文件
                return process.Start();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
