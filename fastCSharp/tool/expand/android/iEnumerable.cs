using System;
using System.Collections;
using System.Collections.Generic;

namespace fastCSharp.android
{
    /// <summary>
    /// IEnumerable ��չ
    /// </summary>
    public static class iEnumerable
    {
        /// <summary>
        /// ��ȡö���� GetEnumerator
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<valueType> iterator<valueType>(this IEnumerable<valueType> value)
        {
            return value.GetEnumerator();
        }
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEnumerator iterator(this IEnumerable value)
        {
            return value.GetEnumerator();
        }
        /// <summary>
        /// this
        /// </summary>
        /// <typeparam name="keyType"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<valueType> entrySet<valueType>(this IEnumerable<valueType> value)
        {
            return value;
        }
    }
}