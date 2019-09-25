using System;
using System.Collections.Generic;
using Android.App;
using Com.Alipay.Sdk.App;

namespace fastCSharp.alipay.android
{
    /// <summary>
    /// API
    /// </summary>
    public static class api
    {
        /// <summary>
        /// ͬ������֧��
        /// </summary>
        /// <param name="activity">UI ������</param>
        /// <param name="getOrderString">��ȡ�����ַ���</param>
        /// <param name="checkOrderResult">��֤�������</param>
        /// <param name="isShowPayLoading">�û����̻�app�ڲ��������Ƿ���Ҫһ��loading��Ϊ��Ǯ������֮ǰ�Ĺ��ɣ����ֵ����Ϊtrue�������ڵ���pay�ӿڵ�ʱ��ֱ�ӻ���һ��loading��ֱ������H5֧��ҳ����߻����ⲿ��Ǯ������ҳ��loading����ʧ�������齫��ֵ����Ϊtrue���Ż�������֧������֧��ҳ��Ĺ��ɹ��̡���</param>
        /// <returns>���������null ��ʾʧ��</returns>
        public static string Pay(Activity activity, Func<fastCSharp.net.returnValue<string>> getOrderString, Func<string, fastCSharp.net.returnValue<bool>> checkOrderResult, bool isShowPayLoading = true)
        {
            if (activity != null && getOrderString != null && checkOrderResult != null)
            {
                try
                {
                    string orderInfo = getOrderString();
                    if (orderInfo != null)
                    {
                        using (PayTask alipay = new PayTask(activity))
                        {
                            IDictionary<string, string> payResult = alipay.PayV2(orderInfo, isShowPayLoading);
                            if (payResult["resultStatus"] == "9000")
                            {
                                string result = payResult["result"];
                                if (checkOrderResult(result)) return result;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    fastCSharp.log.Default.Add(error, null, false);
                }
            }
            return null;
        }
        /// <summary>
        /// �첽֧��
        /// </summary>
        public sealed class pay
        {
            /// <summary>
            /// UI ������
            /// </summary>
            private Activity activity;
            /// <summary>
            /// �ص�
            /// </summary>
            private Action<string> callback;
            /// <summary>
            /// ��֤�������
            /// </summary>
            private Func<string, fastCSharp.net.returnValue<bool>> checkOrderResult;
            /// <summary>
            /// �û����̻�app�ڲ��������Ƿ���Ҫһ��loading��Ϊ��Ǯ������֮ǰ�Ĺ��ɣ����ֵ����Ϊtrue�������ڵ���pay�ӿڵ�ʱ��ֱ�ӻ���һ��loading��ֱ������H5֧��ҳ����߻����ⲿ��Ǯ������ҳ��loading����ʧ�������齫��ֵ����Ϊtrue���Ż�������֧������֧��ҳ��Ĺ��ɹ��̡���
            /// </summary>
            private bool isShowPayLoading;
            /// <summary>
            /// �첽֧��
            /// </summary>
            /// <param name="activity">UI ������</param>
            /// <param name="checkOrderResult">��֤�������</param>
            /// <param name="callback">�ص�</param>
            /// <param name="isShowPayLoading">�û����̻�app�ڲ��������Ƿ���Ҫһ��loading��Ϊ��Ǯ������֮ǰ�Ĺ��ɣ����ֵ����Ϊtrue�������ڵ���pay�ӿڵ�ʱ��ֱ�ӻ���һ��loading��ֱ������H5֧��ҳ����߻����ⲿ��Ǯ������ҳ��loading����ʧ�������齫��ֵ����Ϊtrue���Ż�������֧������֧��ҳ��Ĺ��ɹ��̡���</param>
            public pay(Activity activity, Func<string, fastCSharp.net.returnValue<bool>> checkOrderResult, Action<string> callback, bool isShowPayLoading)
            {
                this.activity = activity;
                this.checkOrderResult = checkOrderResult;
                this.callback = callback;
                this.isShowPayLoading = isShowPayLoading;
            }
            /// <summary>
            /// ��ȡ�����ַ�������֧������
            /// </summary>
            /// <param name="order"></param>
            public void Send(fastCSharp.net.returnValue<string> order)
            {
                bool isCheck = false;
                if (order.IsReturn && order.Value != null)
                {
                    try
                    {
                        using (PayTask alipay = new PayTask(activity))
                        {
                            IDictionary<string, string> payResult = alipay.PayV2(order.Value, isShowPayLoading);
                            if (payResult["resultStatus"] == "9000")
                            {
                                string result = payResult["result"];
                                if (checkOrderResult(result))
                                {
                                    isCheck = true;
                                    if (callback != null) callback(result);
                                    return;
                                }
                            }
                        }
                    }
                    catch (System.Exception error)
                    {
                        fastCSharp.log.Default.Add(error, null, false);
                    }
                }
                if (!isCheck && callback != null) callback(null);
            }
        }
        /// <summary>
        /// �첽����֧��
        /// </summary>
        /// <param name="activity">UI ������</param>
        /// <param name="getOrderString">��ȡ�����ַ���</param>
        /// <param name="checkOrderResult">��֤�������</param>
        /// <param name="callback">�ص�</param>
        /// <param name="isShowPayLoading">�û����̻�app�ڲ��������Ƿ���Ҫһ��loading��Ϊ��Ǯ������֮ǰ�Ĺ��ɣ����ֵ����Ϊtrue�������ڵ���pay�ӿڵ�ʱ��ֱ�ӻ���һ��loading��ֱ������H5֧��ҳ����߻����ⲿ��Ǯ������ҳ��loading����ʧ�������齫��ֵ����Ϊtrue���Ż�������֧������֧��ҳ��Ĺ��ɹ��̡���</param>
        public static void Pay(Activity activity, Action<Action<fastCSharp.net.returnValue<string>>> getOrderString, Func<string, fastCSharp.net.returnValue<bool>> checkOrderResult, Action<string> callback, bool isShowPayLoading = true)
        {
            if (activity != null && getOrderString != null && checkOrderResult != null)
            {
                try
                {
                    getOrderString(new pay(activity, checkOrderResult, callback, isShowPayLoading).Send);
                    return;
                }
                catch (System.Exception error)
                {
                    fastCSharp.log.Default.Add(error, null, false);
                }
            }
            if (callback != null) callback(null);
        }
    }
}