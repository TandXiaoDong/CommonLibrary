using System;
using System.IO;

namespace CommonUtils.Logger
{
    public static class Log
    {
        private static object obj = new object();
        /// <summary>
        /// 日志文件,记录软件的运行状态等
        /// </summary>
        public static void Write(string str)
        {
            lock (obj)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "log";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo director = Directory.CreateDirectory(path);
                }

                string filePath = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (File.Exists(filePath))
                {
                    FileStream fs = new FileStream(filePath, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + str);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(str);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void Error(string str)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(path))
            {
                DirectoryInfo director = Directory.CreateDirectory(path);
            }

            string filePath = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".err";
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" "+str);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" "+str);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }

    }
}
