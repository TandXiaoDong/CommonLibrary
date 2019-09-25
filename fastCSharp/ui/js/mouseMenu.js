/// <reference path = "./menu.ts" />
/*include:js\menu*/
'use strict';
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
//鼠标覆盖菜单	<div id="YYY"></div><div mousemenu="{MenuId:'YYY',IsDisplay:1,Type:'Bottom',Top:5,Left:-5}" id="XXX"></div>
var fastCSharp;
(function (fastCSharp) {
    var MouseMenu = (function (_super) {
        __extends(MouseMenu, _super);
        function MouseMenu(Parameter) {
            _super.call(this, Parameter);
            fastCSharp.Pub.GetParameter(this, MouseMenu.DefaultParameter, Parameter);
            fastCSharp.Pub.GetEvents(this, MouseMenu.DefaultEvents, Parameter);
            this.HideFunction = fastCSharp.Pub.ThisFunction(this, this.Hide);
            this.MouseOutFunction = fastCSharp.Pub.ThisFunction(this, this.MouseOut);
            this.Start(this.Event || fastCSharp.DeclareEvent.Default);
        }
        MouseMenu.prototype.Start = function (Event) {
            if (!Event.IsGetOnly) {
                this.OnStart.Function(this);
                var Element = fastCSharp.HtmlElement.$IdElement(this.Id);
                if (Element != this.Element) {
                    this.Element = Element;
                    fastCSharp.HtmlElement.$AddEvent(Element, ['mouseout'], this.MouseOutFunction);
                    if (this.IsMouseMove)
                        fastCSharp.HtmlElement.$AddEvent(Element, ['mousemove'], fastCSharp.Pub.ThisEvent(this, this.ReShow));
                    this.CheckMenuParameter(true);
                }
                this.ClearInterval();
                this.IsOver = true;
                this.Show(Event);
            }
        };
        MouseMenu.prototype.CheckMenuParameter = function (IsStart) {
            if (IsStart === void 0) { IsStart = false; }
            var Element = fastCSharp.HtmlElement.$Id(this.MenuId);
            if (Element.Element0()) {
                var Parameter = fastCSharp.HtmlElement.$Attribute(Element.Element0(), 'mousemenu');
                if (Parameter != null) {
                    var Id = eval('(' + Parameter + ')').Id;
                    if (Id != this.Id) {
                        Parameter = null;
                        var Menu = fastCSharp.Declare.Getters['MouseMenu'](Id, true);
                        if (Menu) {
                            Menu.Remove();
                            Element.Set('mousemenu', '{Id:"' + this.Id + '"}');
                            return;
                        }
                    }
                }
                if (IsStart) {
                    if (Parameter == null)
                        Element.Set('mousemenu', '{Id:"' + this.Id + '"}');
                    Element.AddEvent('mouseout', this.MouseOutFunction);
                }
            }
        };
        MouseMenu.prototype.Show = function (Event) {
            this.ShowMenu();
            if (this.IsMove)
                this.ReShow(Event, fastCSharp.HtmlElement.$Id(this.Id));
            this.OnShowed.Function();
        };
        MouseMenu.prototype.MouseOut = function () {
            this.IsOver = false;
            this.ClearInterval();
            this.HideInterval = setTimeout(this.HideFunction, this.Timeout);
        };
        MouseMenu.prototype.ClearInterval = function () {
            if (this.HideInterval) {
                clearTimeout(this.HideInterval);
                this.HideInterval = 0;
            }
        };
        MouseMenu.prototype.Hide = function () {
            this.ClearInterval();
            this.HideMenu();
        };
        MouseMenu.prototype.Remove = function () {
            this.ClearInterval();
            this.ShowView = null;
            var Element = fastCSharp.HtmlElement.$Id(this.Id);
            Element.DeleteEvent('mouseout', this.MouseOutFunction);
            fastCSharp.HtmlElement.$Id(this.MenuId).Set('mousemenu', '').DeleteEvent('mouseout', this.MouseOutFunction);
            this.Element = null;
        };
        MouseMenu.prototype.ReShow = function (Event, Element) {
            this.OnMove.Function(Event, this);
            this['To' + (this.Type || 'Mouse')](Event, Element);
        };
        MouseMenu.prototype.ToMouse = function (Event, Element) {
            this.CheckScroll(Event.clientX, Event.clientY);
        };
        MouseMenu.DefaultParameter = { Timeout: 100, IsMouseMove: 0 };
        MouseMenu.DefaultEvents = { OnMove: null };
        return MouseMenu;
    }(fastCSharp.Menu));
    fastCSharp.MouseMenu = MouseMenu;
    var MouseMenuEnum = (function () {
        function MouseMenuEnum(Value, Show) {
            this.Value = Value;
            this.Show = Show || Value;
        }
        MouseMenuEnum.prototype.ToJson = function (IsIgnore, IsNameQuery, Parents) {
            return fastCSharp.Pub.ToJson(this.Value, IsIgnore, IsNameQuery, Parents);
        };
        MouseMenuEnum.prototype.toString = function () {
            return this.Value;
        };
        return MouseMenuEnum;
    }());
    fastCSharp.MouseMenuEnum = MouseMenuEnum;
    new fastCSharp.Declare(MouseMenu, 'MouseMenu', 'mouseover', 'ParameterId');
})(fastCSharp || (fastCSharp = {}));
