using System;
using System.IO;
using System.Diagnostics;
using CommonUtils.Logger;

namespace CommonUtils.FileHelper
{
    public class CmdCommand
    {
        public static void StartCmd(String workingDirectory, String command)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //MessageBox.Show(p.StartInfo.WorkingDirectory);
            p.StartInfo.WorkingDirectory = workingDirectory;
            //MessageBox.Show(p.StartInfo.WorkingDirectory);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
            //MessageBox.Show(command);
            p.StandardInput.WriteLine("exit");
        }

        public static void StartBat(string path, string name)
        {
            Process proc = null;
            try
            {
                string targetDir = string.Format(path);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = name;
                proc.StartInfo.Arguments = string.Format("10");
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }


        public static string ExeCommand()
        {
            Process p = new Process();   //实例一个Process类，启动一个独立进程

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true;   //重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;   //重定向标准输出
            p.StartInfo.RedirectStandardError = true;   //重定向错误输出
            p.StartInfo.CreateNoWindow = true;    //设置不显示窗口
            string strOutput = null;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(@"ftp -is");
        
                p.StandardInput.WriteLine("exit");
                StreamReader reader = p.StandardOutput;//截取输出流
                string line = reader.ReadLine();//每次读取一行
                while (!reader.EndOfStream)
                {
                    //tbResult += line + "\n";
                    line = reader.ReadLine();
                }


                //                p.StandardInput.WriteLine("ping 134.224.48.78");

                //                p.StandardInput.WriteLine("exit");
                //                strOutput = p.StandardOutput.ReadToEnd();                
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }

        public static void ExeCmd()
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //关闭Shell的使用
                p.StartInfo.RedirectStandardInput = true;   //重定向标准输入
                p.StartInfo.RedirectStandardOutput = true;   //重定向标准输出
                p.StartInfo.RedirectStandardError = true;   //重定向错误输出
                p.StartInfo.CreateNoWindow = true;//创建窗口 false是开启窗口
                //p.Start("ftp://192.168.1.103/");
                System.Diagnostics.Process.Start("ftp", "-s:ftp://192.168.1.103/");
                //p.StandardInput.WriteLine();
                //p.StandardInput.WriteLine("bye");
                //p.StandardInput.WriteLine("exit");
            }
            catch (Exception e)
            {
                LogHelper.Log.Debug(e.Message);
            }
        }
    }
}
