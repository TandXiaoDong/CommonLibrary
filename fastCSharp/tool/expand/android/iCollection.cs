using System;
using System.Collections;
using System.Collections.Generic;

namespace fastCSharp.android
{
    /// <summary>
    /// ICollection ��չ
    /// </summary>
    public static class iCollection
    {
        /// <summary>
        /// �ж��Ƿ�ռ���
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool isEmpty<valueType>(this ICollection<valueType> value)
        {
            return value.Count == 0;
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int size<valueType>(this ICollection<valueType> value)
        {
            return value.Count;
        }
    }
}