
define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");

    //#region json序列化和反序列化    

    //将数组序列化为json格式
    exports.SerializeJson = function serializeJson(strs) {
        return JSON.stringify(strs);
    };

    exports.DeserializeJson = function deserializeJson(json) {
        return eval('(' + json + ')');
    };

    //#endregion

    //#region 键盘按钮事件

    //禁用退格键返回上一页且输入框能正常使用
    exports.BackSapce = function (event) {
        var e = event || window.event || arguments.callee.caller.arguments[0];
        var d = e.srcElement || e.target;
        if (e && e.keyCode == 8) {
            return d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA' ? true : false;
        }
        return true;
    };

    //#endregion

    //#region 字符串操作

    //判断当前字符串是否以str开始
    exports.StartWidth = function (value, str) {
        //slice比indexof方法在处理长字符串时效率高
        return value.slice(0, str.length) == str;
    };

    //判断当前字符串是否以str结尾
    exports.EndWidth = function (value, str) {
        return value.slice(-str.length) == str;
    };

    //替换字符串
    exports.ReplaceJs = function (value, oldStr, newStr) {
        var reg = new RegExp(oldStr, "g");
        var newValue = value.replace(reg, newStr);
        return newValue;
    };

    //#endregion

    //#region 权限操作

    //加载权限
    exports.LoadOperate = function (selector) {
        var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");
        exports.OperatePurview(selector, iframeid);
    };

    //操作权限
    exports.OperatePurview = function (selector, id) {
        $.ajax({
            url: '/Ajax/GetOperate',
            async: false,
            data: { menuId: id },
            type: 'post',
            complete: function (result) {
                $(selector).append(result.responseText);
                $.parser.parse($(selector));
            }
        });
    };

    //#endregion

    //#region 公共设置

    //设置页面
    exports.LoadBody = function (bottom) {
        var height = document.documentElement.clientHeight;
        var width = document.documentElement.clientWidth;
        var bottomHeight = bottom === undefined ? 0 : bottom;
        $('#searchTool').css('width', width - "4" + "px");
        easyui.ResizeDataGrid("#gridTool", height - bottomHeight, width);
    };

    //#endregion

});