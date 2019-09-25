using System;

namespace fastCSharp.android
{
    /// <summary>
    /// Java.IO.Writer ��չ
    /// </summary>
    public static class writer
    {
        /// <summary>
        /// �ر� Close
        /// </summary>
        /// <param name="stream"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void close(this Java.IO.Writer stream)
        {
            stream.Close();
        }
        /// <summary>
        /// д���ַ��� Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void write(this Java.IO.Writer writer, string str)
        {
            writer.Write(str);
        }
        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="cbuf"></param>
        /// <param name="off"></param>
        /// <param name="len"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void write(this Java.IO.Writer writer, char[] cbuf, int off, int len)
        {
            writer.Write(cbuf, off, len);
        }
        /// <summary>
        /// ˢ�»����� Flush
        /// </summary>
        /// <param name="writer"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void flush(this Java.IO.Writer writer)
        {
            writer.Flush();
        }
    }
}