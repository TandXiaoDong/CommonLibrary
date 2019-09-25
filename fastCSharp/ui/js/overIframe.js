/// <reference path = "./base.page.ts" />
'use strict';
var fastCSharp;
(function (fastCSharp) {
    var OverIframe = (function () {
        function OverIframe(Parameter) {
            if (Parameter === void 0) { Parameter = null; }
            fastCSharp.Pub.GetParameter(this, OverIframe.DefaultParameter, Parameter);
            var Identity = ++fastCSharp.Pub.Identity;
            this.Id = '_' + Identity + '_OVER_';
            this.TableId = '_' + Identity + '_OVERTABLE_';
            this.IframeId = '_' + Identity + '_OVERIFRAME_';
            this.InputId0 = '_' + Identity + '_OVERINPUT0_';
            this.InputId1 = '_' + Identity + '_OVERINPUT1_';
            fastCSharp.HtmlElement.$Create('div').Style('position', 'absolute').Styles('width,height', '100%').Display(0).Set('id', this.Id)
                .Html("<table id='" + this.TableId + "' cellspacing='0' cellpadding='0' border='0' width='100%' height='100%' style='background-color:#444444;'><tr height='100%'><td width='100%' height='100%' align='center'><iframe id='" + this.IframeId + "' name='" + this.IframeId + "' frameborder='0' marginwidth='0' marginheight='0' vspace='0' hspace='0' allowtransparency='true' scrolling='no'></iframe></td></tr></table>")
                .To();
            fastCSharp.HtmlElement.$Id(this.TableId).Opacity(90);
            this.MoveFunction = fastCSharp.Pub.ThisFunction(this, this.Move);
            this.FocusFunction = fastCSharp.Pub.ThisFunction(this, this.Focus);
            this.OnLoad = new fastCSharp.Events();
            frames[this.IframeId].onload = fastCSharp.Pub.ThisFunction(this, this.LoadIframe);
        }
        OverIframe.prototype.Move = function () {
            var Iframe = frames[this.IframeId], IframeElement = fastCSharp.HtmlElement.$Id(this.IframeId);
            try {
                if (this.Width == null)
                    IframeElement.Set('width', Iframe.document.body.scrollWidth || Iframe.document.documentElement.scrollWidth);
                if (this.Height == null)
                    IframeElement.Set('height', Iframe.document.body.scrollHeight || Iframe.document.documentElement.scrollHeight);
            }
            catch (e) { }
            fastCSharp.HtmlElement.$Id(this.Id).Style('zIndex', 0).Style('top', fastCSharp.HtmlElement.$GetScrollTop() + 'px').Style('left', fastCSharp.HtmlElement.$GetScrollLeft() + 'px').TopIndex();
        };
        OverIframe.prototype.Hide = function (IsOnHide) {
            if (IsOnHide === void 0) { IsOnHide = true; }
            if (this.Interval)
                clearTimeout(this.Interval);
            frames[this.IframeId].document.onblur = null;
            fastCSharp.HtmlElement.$Id(this.Id).Styles('top,left', '0px').Display(0);
            if (IsOnHide && this.OnHide)
                this.OnHide.Function();
        };
        OverIframe.prototype.SetHtml = function (Html) {
            if (Html === void 0) { Html = ''; }
            try {
                frames[this.IframeId].document.body.innerHTML = Html;
            }
            catch (e) { }
        };
        OverIframe.prototype.Show = function (ReLoad) {
            if (ReLoad === void 0) { ReLoad = true; }
            this.Hide(false);
            var Iframe = frames[this.IframeId], IframeElement = fastCSharp.HtmlElement.$Id(this.IframeId);
            fastCSharp.HtmlElement.$Id(this.Id).Display(1);
            this.Move();
            this.Interval = setInterval(this.MoveFunction, 500);
            if (ReLoad) {
                if (this.IframeCode) {
                    try {
                        Iframe.document.body.innerHTML = "<div align='center'>" + (this.WaitingImage ? "<img src='" + this.WaitingImage + "'><br />" : '') + "" + this.IframeCode + '</div>';
                    }
                    catch (e) {
                        if (this.IframeCode.substring(0, 6).toLowerCase() != '<html>')
                            this.IframeCode = '<html><body>' + this.IframeCode + '</body></html>';
                        Iframe.document.open();
                        Iframe.document.write(this.IframeCode);
                        Iframe.document.close();
                    }
                    this.LoadIframe();
                }
                else if (this.IframePath)
                    IframeElement.Set('src', this.IframePath);
            }
        };
        OverIframe.prototype.ShowPath = function (Path) {
            this.IframePath = Path;
            this.Show();
        };
        OverIframe.prototype.ShowHtml = function (Html) {
            this.IframeCode = Html;
            this.Show();
        };
        OverIframe.prototype.LoadIframe = function () {
            if (fastCSharp.HtmlElement.$Id(this.Id).Style0('display') == '') {
                var Iframe = frames[this.IframeId];
                fastCSharp.HtmlElement.$Id(this.IframeId).Set('width', this.Width ? this.Width : (Iframe.document.body.scrollWidth || Iframe.document.documentElement.scrollWidth))
                    .Set('height', this.Height ? this.Height : (Iframe.document.body.scrollHeight || Iframe.document.documentElement.scrollHeight));
                this.OnLoad.Function();
                this.AppendInput(this.InputId0);
                (this.AppendInput(this.InputId1)).onfocus = (Iframe.document).onblur = this.FocusFunction;
            }
        };
        OverIframe.prototype.AppendInput = function (InputId) {
            var Iframe = frames[this.IframeId], Element = Iframe.document.getElementById(InputId);
            if (Element == null) {
                Element = Iframe.document.createElement('input');
                Element.id = InputId;
                Element.style.position = 'absolute';
                Element.style.width = Element.style.height = '1px';
                Element.readOnly = true;
                Iframe.document.body.appendChild(Element);
            }
            return Element;
        };
        OverIframe.prototype.Focus = function () {
            frames[this.IframeId].document.getElementById(this.InputId0).focus();
        };
        OverIframe.CreateDefalut = function () {
            OverIframe.Defalut = new OverIframe();
        };
        OverIframe.DefaultParameter = { WaitingImage: '//__STATICDOMAIN__/upFile/waiting.gif', IframePath: null, IframeCode: null, Width: null, Height: null };
        return OverIframe;
    }());
    fastCSharp.OverIframe = OverIframe;
    fastCSharp.Pub.OnLoad(OverIframe.CreateDefalut, OverIframe, true);
})(fastCSharp || (fastCSharp = {}));
