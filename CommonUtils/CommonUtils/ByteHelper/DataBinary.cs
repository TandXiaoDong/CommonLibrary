using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CommonUtils.Logger;

namespace ComLibrary.ByteHelper
{
    public static class DataBinary
    {
        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            try
            {
                for (int i = 0; i < returnBytes.Length; i++)
                {
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }

                return returnBytes;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将二进制转成字符串
        /// </summary>
        public static string Decode(string s)
        {
            CaptureCollection cs = Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }

        public static string Encode(string s)
        {
            byte[] data = Encoding.Unicode.GetBytes(s);
            StringBuilder result = new StringBuilder(data.Length);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            LogHelper.Log.Debug(result.ToString());

            return result.ToString();
        }

        public static void ReadBinary(string name)
        {
            string sFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + '\\' + "blog";
            FileStream fs = new FileStream(sFileName + "\\" + name, FileMode.Open);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);

            byte[] byteForFloat = new byte[4];
            for (int i = 0; i < byteForFloat.Length; i++)
            {
                byteForFloat = br.ReadBytes(byteForFloat.Length);

                ByteToString(byteForFloat);
            }

            br.Close();
            fs.Close();
        }

        public static string StringToBinary(string ss)
        {
            string result = string.Empty;
            string binaryNum = string.Empty;
            byte[] u = Encoding.ASCII.GetBytes(ss);

            foreach (byte a in u)
            {
                binaryNum = Convert.ToString(a, 2);
                if (binaryNum.Length < 7)
                {
                    for (int i = 0; i < 7 - binaryNum.Length; i++)
                    {
                        binaryNum = '0' + binaryNum;
                    }
                }
                result += binaryNum;
            }

            return result;
        }

        public static StringBuilder BinaryFloat(float f)
        {
            int temp = 0x80;
            StringBuilder sb = new StringBuilder();
            byte[] aa = System.BitConverter.GetBytes(f);

            //逆序二进制数，是内存中存储顺序
            for (int i = 0; i < 4; i++)
            {
                temp = 0x80;
                int tt = aa[i];
                for (int j = 0; j < 8; j++)
                {
                    //txt1.Text += (tt & temp) != 0 ? 1 : 0;
                    sb.Append((tt & temp) != 0 ? 1 : 0);
                    temp = (byte)temp >> 1;
                }
            }

            return sb;
        }

        public static void Read(string path, string fname)
        {
            if (Directory.Exists(path))
            {
                FileStream fs = new FileStream(path + "\\" + fname, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string str = string.Empty;

                while (true)
                {
                    str = sr.ReadLine();
                    if (!string.IsNullOrEmpty(str))
                    {    
                        //1                                34                               67                               100
                        //00000000000000000000000000000000 00000000000000000000000000000000 00000000000000000000000000000000 00000000000000000000000000000000
                        //Log.Write(str.Substring(1, 32) +","+ str.Substring(34,32)+","+ str.Substring(67,32)+"," + str.Substring(100,32));
                    }
                    else
                    {
                        sr.Close();
                        fs.Close();
                        break;
                    }
                }
            }
            else
            {

            }
        }

        public static string ByteToString(byte[] inputBytes)
        {
            StringBuilder sb = new StringBuilder(2048);
            foreach (byte tempByte in inputBytes)
            {
                //tempByte > 15 ?Convert.ToString(tempByte, 2) : '0' + Convert.ToString(tempByte, 2)
                sb.Append(Convert.ToString(tempByte, 2));
            }

            LogHelper.Log.Debug(sb.ToString());

            return sb.ToString();
        }

        public static int BinaryFile(string name, string content)
        {
            try
            {
                string sFileName = AppDomain.CurrentDomain.BaseDirectory + "\\blog";
                if (!Directory.Exists(sFileName))
                {
                    DirectoryInfo director = Directory.CreateDirectory(sFileName);
                }

                FileStream fs = new FileStream(sFileName + "\\" + name, FileMode.OpenOrCreate);
                BinaryWriter binWriter = new BinaryWriter(fs);
                byte[] buffer = HexToByte(content);
                if (buffer == null)
                {
                    return 0;
                }
                binWriter.Write(buffer);
                binWriter.Flush();
                binWriter.Close();
                fs.Close();
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex.Message);
                return 0;
            }
        }

        /// <summary>  
        /// 将文件保存到本地  
        /// </summary>  
        /// <param name="psContent">文件的二进制数据字符串</param>  
        /// <param name="psFileName">文件名称，必须带后缀</param>
        public static void SaveFile(float t, float x, float y, float z, string psFileName)
        {

            string sFileName = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + '\\' + "blog";
            if (!Directory.Exists(sFileName))
            {
                DirectoryInfo director = Directory.CreateDirectory(sFileName);
            }

            FileStream fs = new FileStream(sFileName + "\\" + psFileName, FileMode.Append);
            BinaryWriter binWriter = new BinaryWriter(fs);

            //binWriter.Write(bianma(psContent)+"\r\n");
            binWriter.Write(BinaryFloat(t).ToString());
            binWriter.Write(BinaryFloat(x).ToString());
            binWriter.Write(BinaryFloat(y).ToString());
            binWriter.Write(BinaryFloat(z).ToString());
            binWriter.Write("\r\n");

            binWriter.Flush();
            if (File.GetAttributes(sFileName + "\\" + psFileName).ToString().IndexOf("ReadOnly") != -1)
            {
                File.SetAttributes(sFileName + "\\" + psFileName, FileAttributes.Normal);
            }
            binWriter.Close();
            fs.Close();
        }
    }
}