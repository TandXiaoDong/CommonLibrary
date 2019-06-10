using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CommonUtils.CalculateAndString
{
    public static class Calculate
    {
        #region 计算长度
        /// <summary>
        /// 计算文字长度
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="fontSize">文字大小</param>
        /// <param name="typeFace">字体</param>
        /// <returns>关于字体占用的大小</returns>
        public static double MeasureString2(string text, float fontSize = 11, string typeFace = "微软雅黑")
        {
            Control ctr = new Control();
            Graphics vGraphics = ctr.CreateGraphics();
            vGraphics.PageUnit = GraphicsUnit.Point;
            int n = 0;
            if (text.Contains("[") && text.Contains("]"))
            {
                Regex regex = new Regex(@"\[(\w|\-)*\]");
                MatchCollection matchCollection = regex.Matches(text);
                foreach (Match match in matchCollection)
                {
                    text = text.Replace(match.ToString(), "");
                    n += 20;
                }
            }

            SizeF vSizeF = vGraphics.MeasureString(text, new Font(typeFace, fontSize));
            int dStrLength = Convert.ToInt32(Math.Ceiling(vSizeF.Width));
            ctr.Dispose();

            return Convert.ToDouble(vSizeF.Width) + n;
        }
        #endregion

        #region 最大公约数
        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="a">数字1</param>
        /// <param name="b">数字2</param>
        /// <returns>算出的最大公约数</returns>
        public static int GCD(int a, int b)
        {
            int gcd = 1;
            int min = a > b ? b : a;
            for (int i = min; i >= 1; i--)
            {
                if (a % i == 0 && b % i == 0)
                {
                    gcd = i;
                    break;
                }
            }
            return gcd;
        }
        #endregion

        #region 从字符串里随机得到，规定个数的字符串

        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        public static string GetRandomCode(string allChar, int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        #endregion
    }
}
