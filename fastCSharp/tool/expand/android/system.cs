using System;

namespace fastCSharp.android
{
    /// <summary>
    /// System ��չ
    /// </summary>
    public static class system
    {
        /// <summary>
        /// ��ȡ��ǰʱ����Ժ�����
        /// </summary>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static long currentTimeMillis()
        {
            return (date.NowSecond.Ticks - fastCSharp.web.ajax.JavascriptLocalMinTimeTicks) / date.MillisecondTicks;
        }
        /// <summary>
        /// Buffer.BlockCopy
        /// </summary>
        /// <param name="src"></param>
        /// <param name="srcOffset"></param>
        /// <param name="dst"></param>
        /// <param name="dstOffset"></param>
        /// <param name="count"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void arraycopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }
    }
}