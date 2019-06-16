using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.ByteHelper
{
    class CharConvert
    {
        /// <summary>
        /// 字符数组转字符串
        /// </summary>
        /// <param name="ary"></param>
        /// <returns></returns>
        private static string CharToString(char[] ary)
        {
            string strTemp = "";
            foreach (var v in ary)
            {
                strTemp += v;
            }
            return strTemp;
        }
    }
}
