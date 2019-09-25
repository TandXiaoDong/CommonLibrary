using System;
using Android.App;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.App.AlertDialog ��չ
    /// </summary>
    public static class alertDialog
    {
        /// <summary>
        /// SetMessage
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="message"></param>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void setMessage(this AlertDialog dialog, string message)
        {
            dialog.SetMessage(message);
        }
    }
}