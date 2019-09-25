using System;
using Xamarin.Forms;
#if __IOS__
using Foundation;
using WebKit;
using Xamarin.Forms.Platform.iOS;
#endif
#if __ANDROID__
using Android.Webkit;
using Java.Interop;
using Xamarin.Forms.Platform.Android;
#endif

[assembly: ExportRenderer(typeof(fastCSharp.xamarinForms.ui.webView), typeof(fastCSharp.xamarinForms.webViewRenderer))]
namespace fastCSharp.xamarinForms
{
    /// <summary>
    /// WebView ��Ⱦ�м��
    /// </summary>
    /// <typeparam name="webViewType"></typeparam>
    /// <typeparam name="rendererType"></typeparam>
    public abstract class webViewRenderer<webViewType, rendererType>
#if __IOS__
        : ViewRenderer<webViewType, WKWebView>, IWKScriptMessageHandler
#endif
#if __ANDROID__
        : ViewRenderer<webViewType, Android.Webkit.WebView>
#endif
        where webViewType : fastCSharp.xamarinForms.ui.webView
        where rendererType : webViewRenderer<webViewType, rendererType>
    {
        /// <summary>
        /// JavaScript ���ô���
        /// </summary>
        private const string javascriptInvokeName = "fastCSharpInvoke";
#if __IOS__
        /// <summary>
        ///  JavaScript ���ô���
        /// </summary>
        private static readonly NSString javascriptInvokeString = new NSString("window.fastCSharpInvoke=window.webkit.messageHandlers." + javascriptInvokeName + ";");
        /// <summary>
        /// 
        /// </summary>
        private WKWebView webView;
        /// <summary>
        /// JavaScript ���ô���
        /// </summary>
        private WKUserContentController userController;
        /// <summary>
        /// JavaScript ���ô���
        /// </summary>
        /// <param name="userContentController"></param>
        /// <param name="message"></param>
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.CallJavascript(message.Body.ToString());
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<webViewType> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
#if __IOS__
                userController = new WKUserContentController();
                userController.AddUserScript(new WKUserScript(javascriptInvokeString, WKUserScriptInjectionTime.AtDocumentEnd, false));
                userController.AddScriptMessageHandler(this, javascriptInvokeName);

                webView = new WKWebView(Frame, new WKWebViewConfiguration { UserContentController = userController });
#endif
#if __ANDROID__
                Android.Webkit.WebView webView = new Android.Webkit.WebView(Forms.Context);
                webView.Settings.JavaScriptEnabled = true;
                webView.SetWebViewClient(new WebViewClient());
#endif
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
#if __IOS__
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler(javascriptInvokeName);
#endif
#if __ANDROID__
                Control.RemoveJavascriptInterface(javascriptInvokeName);
#endif
                e.OldElement.Cleanup();
            }
            if (e.NewElement != null)
            {
#if __IOS__
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", Element.Uri));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Uri, false)));
#endif

#if __ANDROID__
                Control.AddJavascriptInterface(new webViewRenderer.javascriptInvokeCSharp(this.Element), javascriptInvokeName);
                Control.LoadUrl(Element.Uri);
                //Control.LoadUrl(string.Format("file:///android_asset/Content/{0}", Element.Uri));
#endif
            }
        }
        /// <summary>
        /// ִ�нű�
        /// </summary>
        /// <param name="script"></param>
        /// <param name="callbak"></param>
        public void EvaluateJavascript(string script, Action<string, string> callback)
        {
            if (Control != null)
            {
#if __IOS__
                if (webView != null)
                {
                    webView.EvaluateJavaScript(script, new webViewRenderer.callback { Callback = callback }.CallbackResult);
                    return;
                }
#endif
#if __ANDROID__
                Control.EvaluateJavascript(script, callback == null ? null : new webViewRenderer.callback { Callbak = callback });
                return;
#endif
                }
            if (callback != null) callback(null, "�Ҳ��� WebView");
        }
    }
    /// <summary>
    /// Ĭ�� WebView ��Ⱦ
    /// </summary>
    public class webViewRenderer : webViewRenderer<fastCSharp.xamarinForms.ui.webView, webViewRenderer>
    {
#if __IOS__
        /// <summary>
        /// JavaScript �ص�
        /// </summary>
        internal sealed class callback
        {
            /// <summary>
            /// JavaScript �ص�
            /// </summary>
            public Action<string, string> Callback;
            /// <summary>
            /// JavaScript �ص�
            /// </summary>
            /// <param name="result"></param>
            /// <param name="error"></param>
            public void CallbackResult(NSObject result, NSError error)
            {
                Callback(result == null ? null : (NSString)result, error == null ? null : error.ToString());
            }
        }
#endif
#if __ANDROID__
        /// <summary>
        /// JavaScript ���ô���
        /// </summary>
        internal sealed class javascriptInvokeCSharp : Java.Lang.Object
        {
            /// <summary>
            /// WebView ����
            /// </summary>
            private readonly WeakReference<fastCSharp.xamarinForms.ui.webView> webViewReference;
            /// <summary>
            /// JavaScript ���ô���
            /// </summary>
            /// <param name="webView"></param>
            public javascriptInvokeCSharp(fastCSharp.xamarinForms.ui.webView webView)
            {
                webViewReference = new WeakReference<fastCSharp.xamarinForms.ui.webView>(webView);
            }
            /// <summary>
            /// JavaScript ���ô���
            /// </summary>
            /// <param name="data"></param>
            [JavascriptInterface]
            [Export]
            public void postMessage(string data)
            {
                fastCSharp.xamarinForms.ui.webView webView;
                if (webViewReference != null && webViewReference.TryGetTarget(out webView)) webView.CallJavascript(data);
            }
        }
        /// <summary>
        /// JavaScript �ص�
        /// </summary>
        internal sealed class callback : Java.Lang.Object, IValueCallback
        {
            /// <summary>
            /// JavaScript �ص�
            /// </summary>
            public Action<string, string> Callbak;
            /// <summary>
            /// JavaScript �ص�
            /// </summary>
            /// <param name="value"></param>
            public void OnReceiveValue(Java.Lang.Object value)
            {
                if (value == null) Callbak(null, "����ʧ��");
                else Callbak((string)value, null);
            }
        }
#endif
    }
}