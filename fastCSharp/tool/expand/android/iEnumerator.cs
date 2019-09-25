using System;
using System.Collections;
using System.Collections.Generic;

namespace fastCSharp.android
{
    /// <summary>
    /// IEnumerator ��չ
    /// </summary>
    public static class iEnumerator
    {
        /// <summary>
        /// ��ȡ��ǰ���� Current
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static valueType next<valueType>(this IEnumerator<valueType> value)
        {
            return value.Current;
        }
        /// <summary>
        /// �ж��Ƿ������һ������ MoveNext
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool hasNext(this IEnumerator value)
        {
            return value.MoveNext();
        }
        /// <summary>
        /// ��ȡ��ǰ���� Current
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static object next(this IEnumerator value)
        {
            return value.Current;
        }
    }
}