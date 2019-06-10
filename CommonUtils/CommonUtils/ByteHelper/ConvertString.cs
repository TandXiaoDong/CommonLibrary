using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils.Logger;

namespace CommonUtils.ByteHelper
{
    public class ConvertString
    {
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
    }
}
