using System;
using Android.OS;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.OS.SystemClock ��չ
    /// </summary>
    public static class systemClock
    {
        /// <summary>
        /// ElapsedRealtime
        /// </summary>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static long elapsedRealtime()
        {
            return SystemClock.ElapsedRealtime();
        }
    }
}