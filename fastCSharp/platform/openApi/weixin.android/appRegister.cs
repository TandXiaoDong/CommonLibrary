using Android.Content;

namespace fastCSharp.weixin.android
{
    /// <summary>
    /// �㲥����(���Ǳ����)
    /// </summary>
    public abstract class appRegister : BroadcastReceiver
    {
        /// <summary>
        /// ΢��ͨ�ù㲥
        /// </summary>
        public const string ActionRefreshWxApp = "com.tencent.mm.plugin.openapi.Intent.ACTION_REFRESH_WXAPP";
        ///// <summary>
        ///// ΢��֧���ɹ��㲥
        ///// </summary>
        //public const string ActionMessageWxPaySuccess = "com.tencent.mm.plugin.openapi.Intent.ACTION_MESSAGE_WXPAY_SUCCESS";

        /// <summary>
        /// API ��װ
        /// </summary>
        private api api;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {
            
            switch (intent.Action)
            {
                case ActionRefreshWxApp:
                    api = createApi(context, intent);
                    actionRefreshWxApp();
                    return;
                //case ActionMessageWxPaySuccess:
                //    api = createApi(context, intent);
                //    actionMessageWxPaySuccess();
                //    return;
            }
        }
        /// <summary>
        /// ���� API ��װ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        /// <returns></returns>
        protected abstract api createApi(Context context, Intent intent);
        /// <summary>
        /// ΢��ͨ�ù㲥
        /// </summary>
        protected virtual void actionRefreshWxApp() { }
        ///// <summary>
        ///// ΢��֧���ɹ��㲥
        ///// </summary>
        //protected virtual void actionMessageWxPaySuccess() { }

        /// <summary>
        /// Ĭ�Ϲ㲥����ע��
        /// </summary>
        public static readonly IntentFilter DefaultIntentFilter;
        /// <summary>
        /// Ĭ��ע��㲥
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appRegister">�㲥����</param>
        public static void Register(Context context, appRegister appRegister)
        {
            context.RegisterReceiver(appRegister, DefaultIntentFilter);
        }
        static appRegister()
        {
            DefaultIntentFilter = new IntentFilter();
            DefaultIntentFilter.AddAction(ActionRefreshWxApp);
            //DefaultIntentFilter.AddAction(ActionMessageWxPaySuccess);
        }
    }
}