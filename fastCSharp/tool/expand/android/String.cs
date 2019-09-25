using System;
using System.Text;
using System.Text.RegularExpressions;

namespace fastCSharp.android
{
    /// <summary>
    /// �ַ�����չ
    /// </summary>
    public static partial class stringExpand
    {
        /// <summary>
        /// ��ȱȽ� ==
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool equals(this string left, string right)
        {
            return left == right;
        }
        /// <summary>
        /// �ж��Ƿ�����ַ��Ӵ� Contains
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool contains(this string left, string right)
        {
            return left.Contains(right);
        }
        /// <summary>
        /// ɾ��ǰ��հ��ַ� Trim
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string trim(this string left)
        {
            return left.Trim();
        }
        /// <summary>
        /// ��ȡ�ַ������� Length
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int length(this string left)
        {
            return left.Length;
        }
        /// <summary>
        /// ��ȡ�ַ� [index]
        /// </summary>
        /// <param name="left"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static char charAt(this string left, int index)
        {
            return left[index];
        }
        /// <summary>
        /// ��ȡ�ַ����� ToCharArray
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static char[] toCharArray(this string left)
        {
            return left.ToCharArray();
        }
        /// <summary>
        /// �����Ӵ�λ�� IndexOf
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int indexOf(this string left, string right)
        {
            return left.IndexOf(right);
        }
        /// <summary>
        /// ��ĸСдת�� ToLower
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string toLowerCase(this string left)
        {
            return left.ToLower();
        }
        /// <summary>
        /// �����ַ�����
        /// </summary>
        /// <param name="left"></param>
        /// <param name="srcBegin"></param>
        /// <param name="srcEnd"></param>
        /// <param name="dst"></param>
        /// <param name="dstBegin"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void getChars(this string left, int srcBegin, int srcEnd, char[] dst, int dstBegin)
        {
            while (srcBegin < srcEnd) dst[dstBegin++] = left[srcBegin++];
        }
        /// <summary>
        /// �����ַ����滻
        /// </summary>
        /// <param name="left"></param>
        /// <param name="regex"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string replaceFirst(this string left, string regex, string replaceValue)
        {
            return new Regex(regex).Replace(left, replaceValue, 1);
        }
        /// <summary>
        /// System.String.Compare
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool equalsIgnoreCase(this string left, string right)
        {
            return System.String.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
        }
        /// <summary>
        /// Split
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string[] split(this string left, char right)
        {
            return left.Split(right);
        }
        /// <summary>
        /// Split
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string[] split(this string left, string right)
        {
            return left.Split(new string[] { right }, StringSplitOptions.None);
        }
        /// <summary>
        /// Encoding.GetBytes
        /// </summary>
        /// <param name="left"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static byte[] getBytes(this string left, string encoding)
        {
            return Encoding.GetEncoding(encoding).GetBytes(left);
        }
        /// <summary>
        /// fastCSharp.String.getBytes
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static byte[] getBytes(this string left)
        {
            return fastCSharp.String.getBytes(left);
        }
        /// <summary>
        /// StartsWith
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool startsWith(this string left, string right)
        {
            return left.StartsWith(right);
        }
        /// <summary>
        /// Substring
        /// </summary>
        /// <param name="left"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string substring(this string left, int startIndex, int length)
        {
            return left.Substring(startIndex, length);
        }
        /// <summary>
        /// Substring
        /// </summary>
        /// <param name="left"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string substring(this string left, int startIndex)
        {
            return left.Substring(startIndex);
        }
        /// <summary>
        /// Replace
        /// </summary>
        /// <param name="left"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string replace(this string left, string oldValue, string newValue)
        {
            return left.Replace(oldValue, newValue);
        }
        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int compareTo(this string left, string right)
        {
            return left.CompareTo(right);
        }
        /// <summary>
        /// LastIndexOf
        /// </summary>
        /// <param name="left"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int lastIndexOf(this string left, char value)
        {
            return left.LastIndexOf(value);
        }
        /// <summary>
        /// ToUpper
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string toUpperCase(this string left)
        {
            return left.ToUpper();
        }

    }
}