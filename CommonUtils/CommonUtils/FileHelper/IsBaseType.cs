using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EquipmentClient.IO
{
    class IsBaseType
    {
        /// <summary>
        /// 数据类型为十进制
        /// </summary>
        private const string DECIMAL_PATTERN = "^[0 - 9] * $";

        /// <summary>
        /// 判断是否十六进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsHexadecimal(string str)
        {
            
            return Regex.IsMatch(str, DECIMAL_PATTERN);
        }

        public static bool IsHexadecimal(byte[] bty)
        {
            for (int i = 0; i < bty.Length; i++)
            {
                string[] str = bty[i].ToString().Split(' ');
                for (int j = 0; j < str.Length; j++)
                {
                    if (Regex.IsMatch(str[j], DECIMAL_PATTERN))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否八进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsOctal(string str)
        {
            const string PATTERN = @"[0-7]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// 判断是否二进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsBinary(string str)
        {
            const string PATTERN = @"[0-1]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// 判断是否十进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsDecimal(string str)
        {
            const string PATTERN = @"[0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }
    }
}
