using System;
using Android.Content;
using Android.Graphics;
using Com.Tencent.MM.Sdk.Openapi;
using Com.Tencent.MM.Sdk.Modelmsg;
using Com.Tencent.MM.Sdk.Modelpay;

namespace fastCSharp.weixin.android
{
    /// <summary>
    /// API ��װ
    /// </summary>
    public class api
    {
        /// <summary>
        /// API
        /// </summary>
        public IWXAPI Api { get; private set; }
        /// <summary>
        /// API �Ƿ����
        /// </summary>
        public bool IsApi { get; private set; }
        /// <summary>
        /// API ��װ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appId"></param>
        /// <param name="isRegisterApp">�Ƿ��Զ�ע��</param>
        public api(Context context, string appId, bool isRegisterApp = true)
        {
            if (isRegisterApp)
            {
                Api = WXAPIFactory.CreateWXAPI(context, null);
                IsApi = Api.IsWXAppInstalled && Api.IsWXAppSupportAPI;
                if (!IsApi || !Api.RegisterApp(appId))
                {
                    Api = null;
                    IsApi = false;
                    fastCSharp.log.Default.Add("΢�� API ע��ʧ��", new System.Diagnostics.StackFrame(), false);
                }
            }
            else
            {
                Api = WXAPIFactory.CreateWXAPI(context, appId);
                IsApi = Api.IsWXAppInstalled && Api.IsWXAppSupportAPI;
            } 
        }

        /// <summary>
        /// ������
        /// </summary>
        public enum shareScene
        {
            /// <summary>
            /// ����Ȧ
            /// </summary>
            WXSceneSession = 0,
            /// <summary>
            /// ʱ����
            /// </summary>
            WXSceneTimeline = 1,
            /// <summary>
            /// �ղ�
            /// </summary>
            WXSceneFavorite = 2
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="msg">������Ϣ</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        private bool share(string type, WXMediaMessage msg, shareScene scene)
        {
            SendMessageToWX.Req req = new SendMessageToWX.Req();
            req.Transaction = type;
            req.Message = msg;
            req.Scene = (int)scene;
            return Api.SendReq(req);
        }
        /// <summary>
        /// �����ı�
        /// </summary>
        /// <param name="text">�ı�</param>
        /// <param name="description">����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool Share(string text, string description = null, shareScene scene = shareScene.WXSceneSession)
        {
            if (IsApi)
            {
                WXMediaMessage msg = new WXMediaMessage(new WXTextObject(text));
                msg.Description = description ?? text;
                return share("text", msg, scene);
            }
            return false;
        }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="bmp">ͼƬ</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="thumbnailNeedRecycle">����ͼ�Ƿ���Ҫ����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareImage(Bitmap bmp, Bitmap thumbnail, bool thumbnailNeedRecycle = true, shareScene scene = shareScene.WXSceneSession)
        {
            if (IsApi)
            {
                WXMediaMessage msg = new WXMediaMessage(new WXImageObject(bmp));
                msg.ThumbData = bmpToByteArray(thumbnail, thumbnailNeedRecycle);
                share("img", msg, scene);
            }
            return false;
        }
        /// <summary>
        /// ����ͼת JPEG �ֽ�����
        /// </summary>
        /// <param name="bmp">ͼƬ</param>
        /// <param name="needRecycle">�Ƿ���Ҫ����</param>
        /// <returns>�ֽ�����</returns>
        private static byte[] bmpToByteArray(Bitmap bmp, bool needRecycle)
        {
            using (System.IO.MemoryStream output = new System.IO.MemoryStream())
            {
                bmp.Compress(Bitmap.CompressFormat.Jpeg, 100, output);
                if (needRecycle) bmp.Recycle();
                return output.ToArray();
            }
        }
        /// <summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="bmp">ͼƬ</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <param name="needRecycle">ԭͼ�Ƿ���Ҫ����</param>
        /// <returns>����ͼ</returns>
        public static Bitmap CreateThumbnail(Bitmap bmp, int width, int height, bool needRecycle = true)
        {
            Bitmap thumbnail = Bitmap.CreateScaledBitmap(bmp, width, height, true);
            if (needRecycle) bmp.Recycle();
            return thumbnail;
        }
        /// <summary>
        /// �����ý��
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="mediaObject">ý����Ϣ</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="thumbnailNeedRecycle">����ͼ�Ƿ���Ҫ����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        private bool share(string type, WXMediaMessage.IMediaObject mediaObject, string title, string description, Bitmap thumbnail, bool thumbnailNeedRecycle, shareScene scene)
        {
            WXMediaMessage msg = new WXMediaMessage(mediaObject);
            msg.Title = title;
            msg.Description = description ?? string.Empty;
            if (thumbnail != null) msg.ThumbData = bmpToByteArray(thumbnail, thumbnailNeedRecycle);
            return share(type, msg, scene);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="url">�������ӵ�ַ</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="thumbnailNeedRecycle">����ͼ�Ƿ���Ҫ����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareMusic(string url, string title, string description = null, Bitmap thumbnail = null, bool thumbnailNeedRecycle = true, shareScene scene = shareScene.WXSceneSession)
        {
            if (IsApi)
            {
                WXMusicObject music = new WXMusicObject();
                music.MusicUrl = url;
                return share("music", music, title, description, thumbnail, thumbnailNeedRecycle, scene);
            }
            return false;
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="url">��Ƶ���ӵ�ַ</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="thumbnailNeedRecycle">����ͼ�Ƿ���Ҫ����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareVideo(string url, string title, string description = null, Bitmap thumbnail = null, bool thumbnailNeedRecycle = true, shareScene scene = shareScene.WXSceneSession)
        {
            if (IsApi)
            {
                WXVideoObject video = new WXVideoObject();
                video.VideoUrl = url;
                return share("video", video, title, description, thumbnail, thumbnailNeedRecycle, scene);
            }
            return false;
        }
        /// <summary>
        /// ������ҳ
        /// </summary>
        /// <param name="url">��ҳ URI</param>
        /// <param name="title">����</param>
        /// <param name="description">����</param>
        /// <param name="thumbnail">����ͼ</param>
        /// <param name="thumbnailNeedRecycle">����ͼ�Ƿ���Ҫ����</param>
        /// <param name="scene">������</param>
        /// <returns>�Ƿ��ͳɹ��������ڷ���ɹ�</returns>
        public bool ShareWeb(string url, string title, string description = null, Bitmap thumbnail = null, bool thumbnailNeedRecycle = true, shareScene scene = shareScene.WXSceneSession)
        {
            if (IsApi)
            {
                WXWebpageObject web = new WXWebpageObject(url);
                return share("webpage", web, title, description, thumbnail, thumbnailNeedRecycle, scene);
            }
            return false;
        }
        /// <summary>
        /// ����֧������
        /// </summary>
        /// <param name="order">���׻Ự��Ϣ</param>
        /// <returns>�Ƿ��ͳɹ���������֧���ɹ�</returns>
        public bool SendPayReq(fastCSharp.openApi.weixin.api.appPrePayIdOrder order)
        {
            return order != null && SendPayReq(new PayReq
            {
                AppId = order.appid,
                PartnerId = order.partnerid,
                PrepayId = order.prepayid,
                NonceStr = order.noncestr,
                TimeStamp = order.timestamp,
                PackageValue = order.package,
                Sign = order.sign
            });
        }
        /// <summary>
        /// ����֧������
        /// </summary>
        /// <param name="req">֧��������Ϣ</param>
        /// <returns>�Ƿ��ͳɹ���������֧���ɹ�</returns>
        public bool SendPayReq(PayReq req)
        {
            req.SignType = "MD5";
            return IsApi && Api.SendReq(req);
        }
        /// <summary>
        /// ͬ������֧��
        /// </summary>
        /// <param name="getOrder">��ȡ���׻Ự��Ϣ</param>
        /// <returns>�Ƿ��ͳɹ���������֧���ɹ�</returns>
        public bool Pay(Func<fastCSharp.net.returnValue<fastCSharp.openApi.weixin.api.appPrePayIdOrder>> getOrder)
        {
            if (getOrder != null && IsApi)
            {
                try
                {
                    fastCSharp.net.returnValue<fastCSharp.openApi.weixin.api.appPrePayIdOrder> order = getOrder();
                    if (order.IsReturn && SendPayReq(order)) return true;
                }
                catch (System.Exception error)
                {
                    fastCSharp.log.Default.Add(error, null, false);
                }
            }
            return false;
        }
        /// <summary>
        /// �첽֧��
        /// </summary>
        protected sealed class pay
        {
            /// <summary>
            /// API ��װ
            /// </summary>
            public api Api;
            /// <summary>
            /// �ص�
            /// </summary>
            public Action<bool> Callback;
            /// <summary>
            /// ��ȡ���׻Ự��Ϣ����֧������
            /// </summary>
            /// <param name="order"></param>
            public void Send(fastCSharp.net.returnValue<fastCSharp.openApi.weixin.api.appPrePayIdOrder> order)
            {
                bool isSend = false;
                if (order.IsReturn && order.Value != null)
                {
                    try
                    {
                        if (Api.SendPayReq(order))
                        {
                            Callback(isSend = true);
                            return;
                        }
                    }
                    catch (System.Exception error)
                    {
                        fastCSharp.log.Default.Add(error, null, false);
                    }
                }
                if (!isSend && Callback != null) Callback(false);
            }
        }
        /// <summary>
        /// �첽����֧��
        /// </summary>
        /// <param name="getOrder">��ȡ���׻Ự��Ϣ</param>
        /// <param name="callback">�ص�</param>
        public void Pay(Action<Action<fastCSharp.net.returnValue<fastCSharp.openApi.weixin.api.appPrePayIdOrder>>> getOrder, Action<bool> callback)
        {
            if (getOrder != null && !IsApi)
            {
                try
                {
                    getOrder(new pay { Api = this, Callback = callback }.Send);
                    return;
                }
                catch (System.Exception error)
                {
                    fastCSharp.log.Default.Add(error, null, false);
                }
            }
            if (callback != null) callback(false);
        }
    }
}