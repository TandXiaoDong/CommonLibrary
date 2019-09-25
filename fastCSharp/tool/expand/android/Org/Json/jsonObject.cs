using System;
using Java.Util;
using Org.Json;

namespace fastCSharp.android
{
    /// <summary>
    /// Org.Json.JSONObject ��չ
    /// </summary>
    public static class jsonObject
    {
        /// <summary>
        /// �������ƻ�ȡ���� OptInt
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int optInt(this JSONObject json, string name)
        {
            return json.OptInt(name);
        }
        /// <summary>
        /// OptInt
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int optInt(this JSONObject json, string name, int fallback)
        {
            return json.OptInt(name, fallback);
        }
        /// <summary>
        /// �������ƻ�ȡ���� Opt
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Java.Lang.Object opt(this JSONObject json, string name)
        {
            return json.Opt(name);
        }
        /// <summary>
        /// �ж��Ƿ�������� IsNull
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool isNull(this JSONObject json, string name)
        {
            return json.IsNull(name);
        }
        /// <summary>
        /// �������ƻ�ȡ�ַ��� GetString
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string getString(this JSONObject json, string name)
        {
            return json.GetString(name);
        }
        /// <summary>
        /// �������ƻ�ȡ���� GetLong
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static long getLong(this JSONObject json, string name)
        {
            return json.GetLong(name);
        }
        /// <summary>
        /// �������ƻ�ȡ���� GetInt
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int getInt(this JSONObject json, string name)
        {
            return json.GetInt(name);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject put(this JSONObject json, string name, long value)
        {
            return json.Put(name, value);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject put(this JSONObject json, string name, int value)
        {
            return json.Put(name, value);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject put(this JSONObject json, string name, double value)
        {
            return json.Put(name, value);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject put(this JSONObject json, string name, bool value)
        {
            return json.Put(name, value);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject put(this JSONObject json, string name, Java.Lang.Object value)
        {
            return json.Put(name, value);
        }
        /// <summary>
        /// GetJSONObject
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static JSONObject getJSONObject(this JSONObject json, string name)
        {
            return json.GetJSONObject(name);
        }
        /// <summary>
        /// Keys
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IIterator keys(this JSONObject json)
        {
            return json.Keys();
        }
        /// <summary>
        /// Keys
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int length(this JSONObject json)
        {
            return json.Length();
        }
    }
}