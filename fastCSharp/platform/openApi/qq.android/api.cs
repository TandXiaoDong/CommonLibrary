using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Com.Tencent.Tauth;

namespace fastCSharp.qq.android
{
    /// <summary>
    /// API ��װ
    /// </summary>
    public class api
    {
        //private const int QQ_SHARE_TITLE_MAX_LENGTH = 45;
        //private const int QQ_SHARE_SUMMARY_MAX_LENGTH = 60;
        private const String SHARE_TO_QQ_IMAGE_URL = "imageUrl";
        private const String SHARE_TO_QQ_IMAGE_LOCAL_URL = "imageLocalUrl";
        private const String SHARE_TO_QQ_TITLE = "title";
        private const String SHARE_TO_QQ_SUMMARY = "summary";
        //private const String SHARE_TO_QQ_SITE = "site";
        private const String SHARE_TO_QQ_TARGET_URL = "targetUrl";
        private const String SHARE_TO_QQ_APP_NAME = "appName";
        private const String SHARE_TO_QQ_AUDIO_URL = "audio_url";
        private const String SHARE_TO_QQ_KEY_TYPE = "req_type";
        //private const String SHARE_TO_QQ_EXT_STR = "share_qq_ext_str";
        private const String SHARE_TO_QQ_EXT_INT = "cflag";
        //private const int SHARE_TO_QQ_FLAG_QZONE_AUTO_OPEN = 1;
        private const int SHARE_TO_QQ_FLAG_QZONE_ITEM_HIDE = 2;
        private const int SHARE_TO_QQ_TYPE_DEFAULT = 1;
        private const int SHARE_TO_QQ_TYPE_AUDIO = 2;
        private const int SHARE_TO_QQ_TYPE_IMAGE = 5;
        private const int SHARE_TO_QQ_TYPE_APP = 6;

        private const String SHARE_TO_QZONE_KEY_TYPE = "req_type";
        //private const int SHARE_TO_QZONE_TYPE_NO_TYPE = 0;
        private const int SHARE_TO_QZONE_TYPE_IMAGE_TEXT = 1;
        //private const int SHARE_TO_QZONE_TYPE_IMAGE = 5;
        //private const int SHARE_TO_QZONE_TYPE_APP = 6;

        /// <summary>
        /// �ջص�����
        /// </summary>
        private sealed class nullUiListener : Java.Lang.Object, IUiListener
        {
            /// <summary>
            /// 
            /// </summary>
            public void OnCancel() { }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value"></param>
            public void OnComplete(Java.Lang.Object value) { }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="error"></param>
            public void OnError(UiError error) { }
            /// <summary>
            /// �ջص�����
            /// </summary>
            public static readonly nullUiListener Null = new nullUiListener();
        }
        /// <summary>
        /// UI �ص�����
        /// </summary>
        public sealed class UiListener : Java.Lang.Object, IUiListener
        {
            /// <summary>
            /// ��ɴ���
            /// </summary>
            private Action<Java.Lang.Object> onComplete;
            /// <summary>
            /// ȡ������
            /// </summary>
            private Action onCancel;
            /// <summary>
            /// ������
            /// </summary>
            private Action<UiError> onError;
            /// <summary>
            /// UI �ص�����
            /// </summary>
            /// <param name="onComplete">��ɴ���</param>
            public UiListener(Action<Java.Lang.Object> onComplete)
            {
                this.onComplete = onComplete;
            }
            /// <summary>
            /// UI �ص�����
            /// </summary>
            /// <param name="onComplete">��ɴ���</param>
            /// <param name="onError">������</param>
            /// <param name="onCancel">ȡ������</param>
            public UiListener(Action<Java.Lang.Object> onComplete, Action<UiError> onError, Action onCancel) : this(onComplete)
            {
                this.onError = onError;
                this.onCancel = onCancel;
            }
            /// <summary>
            /// UI �ص�����
            /// </summary>
            /// <param name="onComplete">��ɴ���</param>
            public static implicit operator UiListener(Action<Java.Lang.Object> onComplete) { return new UiListener(onComplete); }
            /// <summary>
            /// ȡ������
            /// </summary>
            public void OnCancel()
            {
                if (onCancel != null) onCancel();
            }
            /// <summary>
            /// ��ɴ���
            /// </summary>
            /// <param name="value"></param>
            public void OnComplete(Java.Lang.Object value)
            {
                if (onComplete != null) onComplete(value);
            }
            /// <summary>
            /// ������
            /// </summary>
            /// <param name="error"></param>
            public void OnError(UiError error)
            {
                if (onError != null) onError(error);
            }
        }

        /// <summary>
        /// API
        /// </summary>
        private Tencent tencent;
        /// <summary>
        /// UI ������
        /// </summary>
        private Activity activity;
        /// <summary>
        /// ��Q�ͻ��˶������滻�����ء���ť���֣����Ϊ�գ��÷��ش���
        /// </summary>
        private string appName;
        /// <summary>
        /// API ��װ
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="activity"></param>
        /// <param name="appName">��Q�ͻ��˶������滻�����ء���ť���֣����Ϊ�գ��÷��ش���</param>
        public api(string appId, Activity activity, string appName)
        {
            tencent = Tencent.CreateInstance(appId, this.activity = activity);
            this.appName = appName;
        }

        /// <summary>
        /// �������� http://wiki.open.qq.com/index.php?title=Android_API%E8%B0%83%E7%94%A8%E8%AF%B4%E6%98%8E&=45038#1.13_.E5.88.86.E4.BA.AB.E6.B6.88.E6.81.AF.E5.88.B0QQ.EF.BC.88.E6.97.A0.E9.9C.80QQ.E7.99.BB.E5.BD.95.EF.BC.89
        /// </summary>
        /// <param name="url">���� URI</param>
        /// <param name="title">�30���ַ�</param>
        /// <param name="summary">�40����</param>
        /// <param name="imageUrl">ͼƬ URI</param>
        /// <param name="isQZone">�Զ��򿪷���QZone�ĶԻ���</param>
        /// <param name="callback">�ص�����</param>
        public void Share(string url, string title, string summary = null, string imageUrl = null, bool isQZone = false, IUiListener callback = null)
        {
            Bundle parameters = new Bundle();
            parameters.PutInt(SHARE_TO_QQ_KEY_TYPE, SHARE_TO_QQ_TYPE_DEFAULT);
            parameters.PutString(SHARE_TO_QQ_TARGET_URL, url);
            parameters.PutString(SHARE_TO_QQ_TITLE, title);
            if (summary != null) parameters.PutString(SHARE_TO_QQ_SUMMARY, summary);
            if (imageUrl != null) parameters.PutString(SHARE_TO_QQ_IMAGE_URL, imageUrl);
            if (appName != null) parameters.PutString(SHARE_TO_QQ_APP_NAME, appName);
            if (!isQZone) parameters.PutInt(SHARE_TO_QQ_EXT_INT, SHARE_TO_QQ_FLAG_QZONE_ITEM_HIDE);
            tencent.ShareToQQ(activity, parameters, callback ?? nullUiListener.Null);
        }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="imageUrl">ͼƬ URI</param>
        /// <param name="isQZone">�Զ��򿪷���QZone�ĶԻ���</param>
        /// <param name="callback">�ص�����</param>
        public void ShareImage(string imageUrl, bool isQZone = false, IUiListener callback = null)
        {
            Bundle parameters = new Bundle();
            parameters.PutInt(SHARE_TO_QQ_KEY_TYPE, SHARE_TO_QQ_TYPE_IMAGE);
            parameters.PutString(SHARE_TO_QQ_IMAGE_LOCAL_URL, imageUrl);
            if (appName != null) parameters.PutString(SHARE_TO_QQ_APP_NAME, appName);
            if (!isQZone) parameters.PutInt(SHARE_TO_QQ_EXT_INT, SHARE_TO_QQ_FLAG_QZONE_ITEM_HIDE);
            tencent.ShareToQQ(activity, parameters, callback ?? nullUiListener.Null);
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="url">��ҳ URI</param>
        /// <param name="title">����</param>
        /// <param name="audioUrl">��Ƶ URI</param>
        /// <param name="summary">�40����</param>
        /// <param name="imageUrl">ͼƬ URI</param>
        /// <param name="isQZone">�Զ��򿪷���QZone�ĶԻ���</param>
        /// <param name="callback">�ص�����</param>
        public void ShareAudio(string url, string title, string audioUrl, string summary = null, string imageUrl = null, bool isQZone = false, IUiListener callback = null)
        {
            Bundle parameters = new Bundle();
            parameters.PutInt(SHARE_TO_QQ_KEY_TYPE, SHARE_TO_QQ_TYPE_AUDIO);
            parameters.PutString(SHARE_TO_QQ_TARGET_URL, url);
            parameters.PutString(SHARE_TO_QQ_AUDIO_URL, audioUrl);
            parameters.PutString(SHARE_TO_QQ_TITLE, title);
            if (summary != null) parameters.PutString(SHARE_TO_QQ_SUMMARY, summary);
            if (imageUrl != null) parameters.PutString(SHARE_TO_QQ_IMAGE_URL, imageUrl);
            if (appName != null) parameters.PutString(SHARE_TO_QQ_APP_NAME, appName);
            if (!isQZone) parameters.PutInt(SHARE_TO_QQ_EXT_INT, SHARE_TO_QQ_FLAG_QZONE_ITEM_HIDE);
            tencent.ShareToQQ(activity, parameters, callback ?? nullUiListener.Null);
        }
        /// <summary>
        /// ���� APP
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="summary">�40����</param>
        /// <param name="imageUrl">ͼƬ URI</param>
        /// <param name="isQZone">�Զ��򿪷���QZone�ĶԻ���</param>
        /// <param name="callback">�ص�����</param>
        public void ShareApp(string title, string summary = null, string imageUrl = null, bool isQZone = false, IUiListener callback = null)
        {
            Bundle parameters = new Bundle();
            parameters.PutInt(SHARE_TO_QQ_KEY_TYPE, SHARE_TO_QQ_TYPE_APP);
            parameters.PutString(SHARE_TO_QQ_TITLE, title);
            if (summary != null) parameters.PutString(SHARE_TO_QQ_SUMMARY, summary);
            if (imageUrl != null) parameters.PutString(SHARE_TO_QQ_IMAGE_URL, imageUrl);
            if (appName != null) parameters.PutString(SHARE_TO_QQ_APP_NAME, appName);
            if (!isQZone) parameters.PutInt(SHARE_TO_QQ_EXT_INT, SHARE_TO_QQ_FLAG_QZONE_ITEM_HIDE);
            tencent.ShareToQQ(activity, parameters, callback ?? nullUiListener.Null);
        }
        /// <summary>
        /// ������ҳ�� Qzone
        /// </summary>
        /// <param name="url">��ҳ URI</param>
        /// <param name="title">���200���ַ�</param>
        /// <param name="summary">���600�ַ�</param>
        /// <param name="imageUrls">���֧��9��ͼƬ��QZone�ӿ��ݲ�֧�ַ��Ͷ���ͼƬ�����������������ͼƬ������Զ�ѡ���һ��ͼƬ��ΪԤ��ͼ����ͼ�������������Ժ�֧�֣�</param>
        /// <param name="callback">�ص�����</param>
        public void ShareToQzone(string url, string title, string summary = null, IList<string> imageUrls = null, IUiListener callback = null)
        {
            Bundle parameters = new Bundle();
            parameters.PutInt(SHARE_TO_QZONE_KEY_TYPE, SHARE_TO_QZONE_TYPE_IMAGE_TEXT);
            parameters.PutString(SHARE_TO_QQ_TARGET_URL, url);
            parameters.PutString(SHARE_TO_QQ_TITLE, title);
            if (summary != null) parameters.PutString(SHARE_TO_QQ_SUMMARY, summary);
            if (imageUrls != null) parameters.PutStringArrayList(SHARE_TO_QQ_IMAGE_URL, imageUrls);
            if (appName != null) parameters.PutString(SHARE_TO_QQ_APP_NAME, appName);
            tencent.ShareToQzone(activity, parameters, callback ?? nullUiListener.Null);
        }
    }
}