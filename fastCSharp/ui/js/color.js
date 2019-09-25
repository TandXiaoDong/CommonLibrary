/// <reference path = "./base.page.ts" />
'use strict';
var fastCSharp;
(function (fastCSharp) {
    var Color512_64 = (function () {
        function Color512_64(Parameter) {
            fastCSharp.Pub.GetParameter(this, Color512_64.DefaultParameter, Parameter);
            fastCSharp.Pub.GetEvents(this, Color512_64.DefaultEvents, Parameter);
            this.Identity = ++fastCSharp.Pub.Identity;
            this.Image = fastCSharp.Loader.JsDomain + 'js/color512_64.bmp';
        }
        Color512_64.prototype.Start = function () {
            var Iframe = '_' + this.Identity + '_COLOR_';
            fastCSharp.HtmlElement.$Id(this.Id).Html("<iframe Id='" + Iframe + "' name='" + Iframe + "' width='512px' height='64px' frameborder='0' marginwidth='0' marginheight='0' vspace='0' hspace='0' allowtransparency='true' scrolling='no'></iframe>");
            this.Iframe = frames[Iframe];
            this.Iframe.document.open();
            this.Iframe.document.write("<html><body><img id='Color512_64' src='" + this.Image + "' Width='512' Height='64' border='0' alt='点击改变最近选择的色彩参数' /></body></html>");
            this.Iframe.document.close();
            fastCSharp.HtmlElement.$(this.Iframe.document.getElementById('Color512_64')).AddEvent('mousemove', fastCSharp.Pub.ThisEvent(this, this.MoveColor, null, this.Iframe))
                .AddEvent('click', fastCSharp.Pub.ThisEvent(this, this.ClickColor, null, this.Iframe))
                .AddEvent('mouseover', fastCSharp.Pub.ThisFunction(this, this.Over, [true]))
                .AddEvent('mouseout', fastCSharp.Pub.ThisFunction(this, this.Over, [false]));
            fastCSharp.HtmlElement.$Id(this.CurrentColor).Set('maxLength', 6).Value('000000').Set('readOnly', true).AddEvent('focus', Color512_64.Select);
            if (this.CurrentColorSpan) {
                fastCSharp.HtmlElement.$Id(this.CurrentColorSpan).Style('backgroundColor', '#000000').Set('title', '点击改变最近选择的色彩参数').Cursor('pointer')
                    .AddEvent('mouseover', fastCSharp.Pub.ThisFunction(this, this.Over, [true]))
                    .AddEvent('mouseout', fastCSharp.Pub.ThisFunction(this, this.Over, [false]))
                    .AddEvent('click', fastCSharp.Pub.ThisFunction(this, this.Click));
            }
        };
        Color512_64.prototype.GetColor = function (Event) {
            var Width = Event.clientX, Height = Event.clientY, Color = (((Width >> 5) << 4) + ((Height >> 5) << 3) + 4) << 16;
            Color += (((Height < 32 ? Height : 63 - Height) << 3) + 4) << 8;
            Width &= 63;
            return Color + ((Width < 32 ? Width : 63 - Width) << 3) + 4;
        };
        Color512_64.Select = function () { this['select'](); };
        Color512_64.prototype.MoveColor = function (Event) {
            this.Move(this.GetColor(Event));
        };
        Color512_64.prototype.ClickColor = function (Event) {
            this.OnClick.Function(this.Move(this.GetColor(Event)));
        };
        Color512_64.prototype.Over = function (IsOver) {
            this.OnOver.Function(IsOver);
        };
        Color512_64.prototype.Move = function (Color) {
            var Hex = Color.toString(16).PadLeft(6, '0');
            fastCSharp.HtmlElement.$SetValueById(this.CurrentColor, Hex);
            fastCSharp.HtmlElement.$SetStyle(fastCSharp.HtmlElement.$IdElement(this.CurrentColorSpan), 'backgroundColor', '#' + Hex);
            this.OnMove.Function(Hex);
            return Hex;
        };
        Color512_64.prototype.Click = function () {
            this.OnClick.Function(this.Move(parseInt(fastCSharp.HtmlElement.$GetValueById(this.CurrentColor), 16)));
        };
        Color512_64.prototype.Show = function (IsShow) {
            fastCSharp.HtmlElement.$([fastCSharp.HtmlElement.$IdElement(this.Id), fastCSharp.HtmlElement.$IdElement(this.CurrentColor), fastCSharp.HtmlElement.$IdElement(this.CurrentColorSpan)]).Display(IsShow);
        };
        Color512_64.DefaultParameter = { Id: null, CurrentColor: null, CurrentColorSpan: null };
        Color512_64.DefaultEvents = { OnClick: null, OnOver: null, OnMove: null };
        return Color512_64;
    }());
    fastCSharp.Color512_64 = Color512_64;
})(fastCSharp || (fastCSharp = {}));
