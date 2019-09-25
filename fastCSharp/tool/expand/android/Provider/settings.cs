using System;
using Android.Content;
using Android.Provider;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.Provider.Settings ��չ
    /// </summary>
    public static class settings
    {
        /// <summary>
        /// Android.Provider.Settings.Secure ��չ
        /// </summary>
        public static class Secure
        {
            /// <summary>
            /// GetString
            /// </summary>
            /// <param name="resolver"></param>
            /// <param name="name"></param>
            /// <returns></returns>
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            public static string getString(ContentResolver resolver, string name)
            {
                return Settings.Secure.GetString(resolver, name);
            }
        }
    }
}