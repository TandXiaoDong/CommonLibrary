using System;
using System.Collections.Generic;
using Android.OS;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.OS.Bundle ��չ
    /// </summary>
    public static class bundle
    {
        /// <summary>
        /// ��Ӽ�ֵ������ PutByteArray
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void putByteArray(this Bundle bundle, string key, byte[] value)
        {
            bundle.PutByteArray(key, value);
        }
        /// <summary>
        /// ���ݹؼ��ֻ�ȡ���� GetByteArray
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static byte[] getByteArray(this Bundle bundle, string key)
        {
            return bundle.GetByteArray(key);
        }
        /// <summary>
        /// PutAll
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="other"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void putAll(this Bundle bundle, Bundle other)
        {
            bundle.PutAll(other);
        }
    }
}