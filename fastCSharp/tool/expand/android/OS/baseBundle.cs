using System;
using System.Collections.Generic;
using Android.OS;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.OS.BaseBundle ��չ
    /// </summary>
    public static class baseBundle
    {
        /// <summary>
        /// �ж��Ƿ���ڹؼ��� ContainsKey
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool containsKey(this BaseBundle bundle, string key)
        {
            return bundle.ContainsKey(key);
        }
        /// <summary>
        /// ��ȡ��ֵ������ Size
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int size(this BaseBundle bundle)
        {
            return bundle.Size();
        }
        /// <summary>
        /// ����ַ�����ֵ�� PutString
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void putString(this BaseBundle bundle, string key, string value)
        {
            bundle.PutString(key, value);
        }
        /// <summary>
        /// ���ݹؼ��ֻ�ȡ�ַ��� GetString
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string getString(this BaseBundle bundle, string key)
        {
            return bundle.GetString(key);
        }
        /// <summary>
        /// ɾ���ؼ��� Remove
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void remove(this BaseBundle bundle, string key)
        {
            bundle.Remove(key);
        }
        /// <summary>
        /// ��ȡ�ؼ��ּ��� KeySet
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ICollection<string> keySet(this BaseBundle bundle)
        {
            return bundle.KeySet();
        }
        /// <summary>
        /// ���ݹؼ��ֻ�ȡ���� Get
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Java.Lang.Object get(this BaseBundle bundle, string key)
        {
            return bundle.Get(key);
        }
        /// <summary>
        /// ���ݹؼ��ֻ�ȡ�ַ������� GetStringArray
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string[] getStringArray(this BaseBundle bundle, string key)
        {
            return bundle.GetStringArray(key);
        }
        /// <summary>
        /// �ж��Ƿ���ڼ�ֵ�� IsEmpty
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool isEmpty(this BaseBundle bundle)
        {
            return bundle.IsEmpty;
        }
        /// <summary>
        /// GetInt
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int getInt(this BaseBundle bundle, string key, int defaultValue)
        {
            return bundle.GetInt(key, defaultValue);
        }
    }
}