using System;
using System.Data;
using System.IO;
using System.Text;
using CommonUtils.Logger;
using System.Collections.Generic;

namespace CommonUtils.FileHelper
{
    public class WriteData
    {
        private static object objWrite = new object();
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fname">文件名</param>
        public static DataTable ReadFileDT(string path)
        {
            try
            {
                DataTable dt_analysFile = new DataTable();
                dt_analysFile.Columns.Add("t");
                dt_analysFile.Columns.Add("x");
                dt_analysFile.Columns.Add("y");
                dt_analysFile.Columns.Add("z");

                if (File.Exists(path))
                {
                    string str = string.Empty;
                    FileStream fs = new FileStream(path, FileMode.Open);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);

                    while (true)
                    {
                        str = sr.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            string[] strRes = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            DataRow dr = dt_analysFile.NewRow();
                            dr["t"] = strRes[0];
                            dr["x"] = strRes[1];
                            dr["y"] = strRes[2];
                            dr["z"] = strRes[3];
                            dt_analysFile.Rows.Add(dr);
                        }
                        else
                        {
                            sr.Close();
                            fs.Close();
                            break;
                        }
                    }

                    return dt_analysFile;
                }
                else
                {
                    LogHelper.Log.Error(DateTime.Now.ToString("yyyy-MM-dd") + " " + path + " 文件不存在");
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(DateTime.Now.ToString("yyyy-MM-dd") + " 读取文件失败 文件名：" + path);
                return null;
            }
        }

        /// <summary>
        /// 保存字符串
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="path">绝对路径</param>
        public static void WriteString(StringBuilder content, string path)
        {
            lock(objWrite)
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }

        public static void WriteString(List<StringBuilder> content, string path)
        {
            lock (objWrite)
            {
                using (FileStream fs = new FileStream(path,FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        for (int i = 0; i < content.Count; i++)
                        {
                            sw.WriteLine(content[i]);
                        }
                        sw.Flush();
                    }
                }
            }
        }

        public static void SaveBinary(byte[] bytarrDatToSav, string strFileCompleteName)
        {

            FileStream flstrmToSave = null;
            BinaryWriter bnrywrtrToSave = null;
            try
            {
                flstrmToSave = new FileStream(strFileCompleteName, File.Exists(strFileCompleteName) ? FileMode.Append : FileMode.Create);
                bnrywrtrToSave = new BinaryWriter(flstrmToSave);

                bnrywrtrToSave.Write(bytarrDatToSav);
                bnrywrtrToSave.Flush();
                bnrywrtrToSave.Close();
                flstrmToSave.Close();
            }
            catch (Exception ex)
            {
                Log.Write("Failed to write data. " + ex.Message);
                if (bnrywrtrToSave != null)
                {
                    bnrywrtrToSave.Flush();
                    bnrywrtrToSave.Close();
                }
                if (flstrmToSave != null)
                {
                    bnrywrtrToSave.Close();
                }
            }
        }

        /// <summary>
        /// 插入指定行
        /// </summary>
        public static void WriteRowData()
        {
            //输入要读取的文件名（全路径）
            string strFile = Console.ReadLine().Trim();

            System.IO.StreamReader sr = new System.IO.StreamReader(strFile);
            Console.WriteLine("在多少行插入文本：");
            int iLine = Int32.Parse(Console.ReadLine());

            Console.WriteLine("插入什么文本：");
            string strInsert = Console.ReadLine();

            string strText = ""; //用来存储更改后的文本内容
            int lines = 0; //记录文件行数
            while (!sr.EndOfStream)
            {
                lines++;
                if (lines == iLine)
                {
                    strText += strInsert + "\n";
                }
                string str = sr.ReadLine();
                strText += str + "\n";
            }
            if (lines < iLine) //输入的行号比文件总行数大
            {
                Console.WriteLine("\n输入的行号非法！");
            }
            else
            {
                Console.WriteLine("\n插入后文件的内容：\n===========================");
                Console.WriteLine(strText);
            }

            sr.Close();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(strFile, false);
            sw.Write(strText);
            sw.Flush();
            Console.WriteLine("\n文件内容更新完毕！！\n");
            sw.Close();
        }

        /// <summary>
        /// 文件过大时，将文件读入内存中，修改内存中数据，在保存数据
        /// </summary>
        private void InsertData()
        {
            try
            {
                FileStream mFileRead = new FileStream("mfilename", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                List<long> seekPosition = new List<long>();    //存放所有0xC0的首位置  索引值以0位首位置
                List<byte> fileBuf = new List<byte>();            //存放文件


                /*****遍历查找0xC0位置****/
                mFileRead.Seek(0, SeekOrigin.Begin);            //设置索引首位置
                int lastByte = 0;
                while (mFileRead.Position < mFileRead.Length)   //读到最后一个
                {
                    fileBuf.Add((byte)mFileRead.ReadByte());
                    //if (fileBuf.ElementAt((fileBuf.Count - 1)) == '0' && lastByte == 'C')   // 找到 0xC0   'C' 的位置
                    //{
                    //    seekPosition.Add((long)(mFileRead.Position - 2));
                    //}
                    //lastByte = fileBuf.ElementAt((fileBuf.Count - 1));
                }

                /*******遍历增加回车行*******/
                int i = seekPosition.Count - 1;              //TODO:要从后往前添加 否则索引错误
                byte[] NEXTLINE = new byte[2] { 0x0d, 0x0a };
                while (i > 0)
                {
                    //if ((seekPosition.ElementAt(i) - seekPosition.ElementAt(i - 1)) == 5)
                    //{
                    //    lbLOG.Items.Add("0xC0 Nend Add Position =" + seekPosition.ElementAt(i) + "\n");
                    //    fileBuf.Insert((int)(seekPosition.ElementAt(i - 1) + 3), NEXTLINE[0]);
                    //    fileBuf.Insert((int)(seekPosition.ElementAt(i - 1) + 4), NEXTLINE[1]);
                    //}
                    i--;
                }
                fileBuf.Insert(0, NEXTLINE[0]);    //前置回车行
                fileBuf.Insert(0, NEXTLINE[1]);
                mFileRead.Position = 0;
                mFileRead.Write(fileBuf.ToArray(), 0, fileBuf.Count);
                mFileRead.Flush();                //写入文件 清除缓冲区
                mFileRead.Close();                //关闭文件
            }
            catch (IOException e)
            {
                Console.WriteLine("Warnning " + e.ToString() + "\n");
            }
        }
    }
}
