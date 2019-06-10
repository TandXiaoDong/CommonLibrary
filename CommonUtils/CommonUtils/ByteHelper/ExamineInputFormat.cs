using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.ByteHelper
{
    public class ExamineInputFormat
    {
        /// <summary>
        /// 判断是否十六进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHexadecimal(string str)
        {
            const string PATTERN = @"[A-Fa-f0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// 判断是否八进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsOctal(string str)
        {
            const string PATTERN = @"[0-7]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// 判断是否二进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBinary(string str)
        {
            const string PATTERN = @"[0-1]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        /// <summary>
        /// 判断是否十进制格式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(string str)
        {
            const string PATTERN = @"[0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), PATTERN);
        }
    }
}
