using System.Threading;
using Android.App;
using Android.Graphics;
using Com.Sina.Weibo.Sdk.Api;
using Com.Sina.Weibo.Sdk.Api.Share;

namespace fastCSharp.weibo.android
{
    /// <summary>
    /// API ��װ
    /// </summary>
    public class api
    {
        /// <summary>
        /// UI ������
        /// </summary>
        private Activity activity;
        /// <summary>
        /// ����ǰ׺
        /// </summary>
        private string transactionPrefix;
        /// <summary>
        /// ��ǰ�����ʶ
        /// </summary>
        private int identity;
        /// <summary>
        /// API
        /// </summary>
        public IWeiboShareAPI Api { get; private set; }
        /// <summary>
        /// API �Ƿ����
        /// </summary>
        public bool IsApi { get; private set; }
        /// <summary>
        /// API ��װ
        /// </summary>
        /// <param name="activity">UI ������</param>
        /// <param name="appKey"></param>
        /// <param name="isRegisterApp">�Ƿ��Զ�ע��</param>
        public api(Activity activity, string appKey, bool isRegisterApp = true)
        {
            this.activity = activity;
            transactionPrefix = ((ulong)date.Now.Ticks).toHex16();
            Api = WeiboShareSDK.CreateWeiboAPI(activity, appKey);
            IsApi = Api.IsWeiboAppInstalled && Api.IsWeiboAppSupportAPI;
            if (isRegisterApp)
            {
                if (!IsApi || !Api.RegisterApp())
                {
                    Api = null;
                    IsApi = false;
                    fastCSharp.log.Default.Add("΢�� API ע��ʧ��", new System.Diagnostics.StackFrame(), false);
                }
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="mediaObject">������Ϣ</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool Share(BaseMediaObject mediaObject)
        {
            WeiboMessage weiboMessage = new WeiboMessage();
            weiboMessage.MediaObject = mediaObject;
            SendMessageToWeiboRequest request = new SendMessageToWeiboRequest();
            request.Transaction = transactionPrefix + ((uint)Interlocked.Increment(ref identity)).toHex8();
            request.Message = weiboMessage;
            return Api.SendRequest(activity, request);
        }
        /// <summary>
        /// ���ø�����Ϣ
        /// </summary>
        /// <param name="mediaObject">������Ϣ</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        private static void set(BaseMediaObject mediaObject, string url, string title, string description)
        {
            if (url != null) mediaObject.ActionUrl = url;
            if (title != null) mediaObject.Title = title;
            if (description != null) mediaObject.Description = description;
        }
        /// <summary>
        /// ������ҳ
        /// </summary>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareWebpage(string url, string title = null, string description = null)
        {
            WebpageObject webpageObject = new WebpageObject();
            set(webpageObject, url, title, description);
            return Share(webpageObject);
        }
        /// <summary>
        /// �����ı�
        /// </summary>
        /// <param name="text">�ı�</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareText(string text, string url = null, string title = null, string description = null)
        {
            TextObject textObject = new TextObject();
            textObject.Text = text;
            set(textObject, url, title, description);
            return Share(textObject);
        }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="bmp">ͼƬ</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareImage(Bitmap bmp, string url = null, string title = null, string description = null)
        {
            ImageObject imageObject = new ImageObject();
            imageObject.SetImageObject(bmp);
            set(imageObject, url, title, description);
            return Share(imageObject);
        }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="imageUrl">ͼƬ URI</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareImage(string imageUrl, string url = null, string title = null, string description = null)
        {
            ImageObject imageObject = new ImageObject();
            imageObject.ImagePath = imageUrl;
            set(imageObject, url, title, description);
            return Share(imageObject);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="musicObject">������Ϣ</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareMusic(MusicObject musicObject, Bitmap thumbnail = null, string url = null, string title = null, string description = null)
        {
            if (thumbnail != null) musicObject.SetThumbImage(thumbnail);
            set(musicObject, url, title, description);
            return Share(musicObject);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="voiceObject">������Ϣ</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareVoice(VoiceObject voiceObject, Bitmap thumbnail = null, string url = null, string title = null, string description = null)
        {
            if (thumbnail != null) voiceObject.SetThumbImage(thumbnail);
            set(voiceObject, url, title, description);
            return Share(voiceObject);
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="videoObject">��Ƶ��Ϣ</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="url">������� URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareVideo(VideoObject videoObject, Bitmap thumbnail = null, string url = null, string title = null, string description = null)
        {
            if (thumbnail != null) videoObject.SetThumbImage(thumbnail);
            set(videoObject, url, title, description);
            return Share(videoObject);
        }
    }
}