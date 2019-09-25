using System;

namespace fastCSharp.android
{
    /// <summary>
    /// Java.IO.DataInputStream ��չ
    /// </summary>
    public static class dataInputStream
    {
        /// <summary>
        /// ReadFully
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="b"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void readFully(this Java.IO.DataInputStream stream, byte[] b)
        {
            stream.ReadFully(b);
        }
    }
}