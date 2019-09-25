using System;
using Android.Content;
using Android.Database;

namespace fastCSharp.android
{
    /// <summary>
    /// Android.Content.ContentResolver ��չ
    /// </summary>
    public static class contentResolver
    {
        /// <summary>
        /// Query
        /// </summary>
        /// <param name="contentResolver"></param>
        /// <param name="uri"></param>
        /// <param name="projection"></param>
        /// <param name="selection"></param>
        /// <param name="selectionArgs"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ICursor query(this ContentResolver contentResolver, Android.Net.Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            return contentResolver.Query(uri, projection, selection, selectionArgs, sortOrder);
        }
    }
}