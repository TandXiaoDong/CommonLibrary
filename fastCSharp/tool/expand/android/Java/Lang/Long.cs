using System;

namespace fastCSharp.android
{
    /// <summary>
    /// Java.Lang.Long ��չ
    /// </summary>
    public static class Long
    {
        /// <summary>
        /// LongValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static long longValue(this Java.Lang.Long value)
        {
            return value.LongValue();
        }
        /// <summary>
        /// ValueOf
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Java.Lang.Long valueOf(long value)
        {
            return Java.Lang.Long.ValueOf(value);
        }
        /// <summary>
        /// ValueOf
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Java.Lang.Long valueOf(string value)
        {
            return Java.Lang.Long.ValueOf(value);
        }
        /// <summary>
        /// �ַ���ת���� int.Parse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static long parseLong(string value)
        {
            return long.Parse(value);
        }
    }
}