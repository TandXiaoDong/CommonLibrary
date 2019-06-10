using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils.Logger;

namespace CommonUtils.ByteHelper
{
    public class ConvertByte
    {
        /// <summary>
        /// hex to byte
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByte(byte[] buffer,string[] hexString,int startIndex)
        {
            for (int i = 0; i < hexString.Length; i++)
            {
                try
                {
                    buffer[startIndex + i] = Convert.ToByte(hexString[i], 16);
                }
                catch
                {
                    LogHelper.Log.Info("Hexstringtobyte error! "+hexString[i]);
                }
            }
            return buffer;
        }

        /// <summary>
        ///两位十六进制字符串转byte
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hs)
        {
            string[] strArr = hs.Trim().Split(' ');
            byte[] b = new byte[strArr.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < strArr.Length; i++)
            {
                b[i] = Convert.ToByte(strArr[i], 16);
            }
            return b;
        }

        private static void FloatToBytes(byte[] floatData, float fVal, int startIndex)
        {
            int i = 0;
            byte[] tempV = BitConverter.GetBytes(fVal);

            for (i = 0; i < 4; i++)
            {
                floatData[startIndex + i] = tempV[i];
            }
        }

        private static void IntToBytes(byte[] abNeedInt, int iVal, int indexStart)
        {
            int i = 0;
            byte[] abITmp = BitConverter.GetBytes(iVal);

            for (i = 0; i < 4; i++)
            {
                abNeedInt[indexStart + i] = abITmp[i];
            }
        }

        private static void DoubleToBytes(byte[] doubleData, double tval, int indexStart)
        {
            int i = 0;
            byte[] temp = BitConverter.GetBytes(tval);

            for (i = 0; i < 8; i++)
            {
                doubleData[indexStart + i] = temp[i];
            }
        }

        private static void StrToBytes(byte[] abNeedInt, string strVal, int indexStart, int iLength)
        {
            int i = 0;
            byte[] abStrTmp = Encoding.Default.GetBytes(strVal);

            for (i = 0; i < iLength; i++)
            {
                if (abStrTmp.Length == i)
                {
                    break;
                }
                abNeedInt[indexStart + i] = abStrTmp[i];
            }
        }

        public static string ByteToHex(byte comByte)
        {
            return comByte.ToString("X2") + " ";
        }
        public static string ByteToHex(byte[] comByte, int len)
        {
            string returnStr = "";
            if (comByte != null)
            {
                for (int i = 0; i < len; i++)
                {
                    returnStr += comByte[i].ToString("X2") + " ";
                }
            }
            return returnStr;
        }
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");

            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }
        public static string HEXToASCII(string data)
        {
            data = data.Replace(" ", "");
            byte[] comBuffer = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
            {
                comBuffer[i / 2] = (byte)Convert.ToByte(data.Substring(i, 2), 16);
            }
            string result = Encoding.Default.GetString(comBuffer);
            return result;
        }
        public static string ASCIIToHEX(string data)
        {
            StringBuilder result = new StringBuilder(data.Length * 2);
            foreach (char t in data)
            {
                result.Append(((int)t).ToString("X2") + " ");
            }
            return Convert.ToString(result);
        }
    }
}
