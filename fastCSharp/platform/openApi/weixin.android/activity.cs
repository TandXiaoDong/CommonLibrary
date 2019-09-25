using System;
using Android.App;
using Android.OS;
using Android.Content;
using Com.Tencent.MM.Sdk.Openapi;
using Com.Tencent.MM.Sdk.Modelbase;

namespace fastCSharp.weixin.android
{
    /// <summary>
    /// ֧��/���� �ص� UI ����
    /// </summary>
    //[Android.App.IntentFilter(new string[] { "android.intent.action.VIEW" }, Categories = new string[] { "android.intent.category.DEFAULT" }, DataSchemes = new string[] { AppId })]
    //[Android.App.Activity(Name = PackageName + fastCSharp.weixin.android.activity.WXEntryActivityName, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Exported = true)]
    //[Android.App.Activity(Name = PackageName + fastCSharp.weixin.android.activity.WXPayEntryActivityName, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Exported = true)]
    public abstract class activity : Activity, IWXAPIEventHandler
    {
        /// <summary>
        /// ���� �ص� UI ������
        /// </summary>
        public const string WXEntryActivityName = ".wxapi.WXEntryActivity";
        /// <summary>
        /// ֧�� �ص� UI ������
        /// </summary>
        public const string WXPayEntryActivityName = ".wxapi.WXPayEntryActivity";
        /// <summary>
        /// API ��װ
        /// </summary>
        private api api;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            api = createApi();
            if (api != null) api.Api.HandleIntent(Intent, this);
        }
        /// <summary>
        /// ���� API ��װ
        /// </summary>
        /// <returns></returns>
        protected abstract api createApi();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
            if (api != null) api.Api.HandleIntent(intent, this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        public virtual void OnReq(BaseReq req) { }
        /// <summary>
        /// �ص���Ӧ����
        /// </summary>
        /// <param name="resp"></param>
        public virtual void OnResp(BaseResp resp)
        {
            Finish();
        }
    }
}