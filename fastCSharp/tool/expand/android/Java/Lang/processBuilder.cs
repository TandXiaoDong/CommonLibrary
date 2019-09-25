using Java.Lang;

namespace fastCSharp.android
{
    /// <summary>
    /// Java.Lang.ProcessBuilder ��չ
    /// </summary>
    public static class processBuilder
    {
        /// <summary>
        /// Start
        /// </summary>
        /// <param name="processBuilder"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Process start(this ProcessBuilder processBuilder)
        {
            return processBuilder.Start();
        }
    }
}