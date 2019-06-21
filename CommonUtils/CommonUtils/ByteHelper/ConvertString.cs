using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils.Logger;

namespace CommonUtils.CalculateAndString
{
    public class ConvertString
    {
        #region 字符串转定长字符串
        /// <summary>
        /// 字符串转为定长字符串，空内容补空格
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <param name="output"></param>
        private void WriteContent(string input,int len,out string output)
        {
            byte[] ognInput = Encoding.Default.GetBytes(input);

            //声明指定长度字符串
            byte[] arrayInput = new byte[len];

            //pid
            for (int i = 0; i < arrayInput.Length; i++)
            {
                if (i < ognInput.Length)
                {
                    arrayInput[i] = ognInput[i];
                }
                else
                {
                    arrayInput[i] = 0x20;
                }
            }
            output = Encoding.Default.GetString(arrayInput);
        }
        #endregion

        #region 十进制转十六进制
        /// <summary>
        /// 十进制字符串转十六进制
        /// </summary>
        /// <param name="inputstr">十六进制输入字符串</param>
        /// <param name="hexLen">十六进制位数</param>
        /// <returns></returns>
        public static string ConvertToHex(string inputstr,int hexLen)
        {
            try
            {
                if (ExamineInputFormat.IsDecimal(inputstr.Trim()))
                {
                    return "0X" + Convert.ToString(int.Parse(inputstr.Trim()), 16).PadLeft(hexLen, '0').ToUpper();
                }
                else
                {
                    if (ExamineInputFormat.IsHexadecimal(inputstr.Trim()))
                    {
                        LogHelper.Log.Info(inputstr + "已是十六进制无需转换");
                        return inputstr.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("十进制字符串转HEX失败！"+ex.Message);
            }
            return "";
        }
        #endregion

        #region 十六进制转十进制
        /// <summary>
        /// 十六进制字符串转十进制
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns></returns>
        public static string ConvertToDec(string inputstr)
        {
            try
            {
                inputstr = inputstr.Trim().ToLower().Replace("0x", "");
                if (ExamineInputFormat.IsHexadecimal(inputstr))
                {
                    return Convert.ToInt32(inputstr,16).ToString();
                }
                else
                {
                    if (ExamineInputFormat.IsDecimal(inputstr))
                    {
                        LogHelper.Log.Info(inputstr + "已是十进制无需转换");
                        return inputstr;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("十六进制字符串转十进制失败"+ex.Message);
            }
            return "";
        }
        #endregion
    }
}
