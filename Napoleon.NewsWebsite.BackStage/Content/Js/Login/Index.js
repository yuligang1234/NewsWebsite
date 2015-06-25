﻿
define(function (require, exports, module) {

    //设置输入框的宽度
    exports.SetBoxWidth = function () {
        $('.LoginBox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
    };

    //设置输入框提示信息
    exports.SetLoginText = function () {
        $(".Txt").css("color", "#D3D8DE");
        $('.Txt').on({
            focus: function () {
                if ($(this).val() === this.defaultValue) {
                    $(this).val("");
                    $(this).css("color", "#000000");
                    if (this.id === "ShowPassWord") {
                        $('#ShowPassWord').hide();
                        $('#PassWord').show().focus();
                    }
                }
            },
            blur: function () {
                if ($(this).val() === "") {
                    $(this).val(this.defaultValue);
                    $(this).css("color", "#D3D8DE");
                    if (this.id === "PassWord") {
                        $('#ShowPassWord').show();
                        $('#PassWord').hide();
                    }
                }
            }
        });
    };

    //重置
    exports.BtnClear = function () {
        $('.Txt').each(function () {
            if ($(this).val() != this.defaultValue) {
                $(this).val(this.defaultValue);
                $(this).css("color", "#D3D8DE");
                if (this.id === "PassWord") {
                    $('#ShowPassWord').show();
                    $('#PassWord').hide();
                }
            }
        });
        return false;
    };

    //登录
    exports.BtnLogin = function () {
        var userName,
            passWord;
        userName = $('#UserName').val();
        passWord = $('#PassWord').val();
        $.ajax({
            url: '/Login/CheckUser',
            type: 'post',
            data: { userName: userName, passWord: encodeURI(passWord) },
            beforeSend: function () {
                $.messager.progress({
                    msg: '登录中......'
                });
            },
            complete: function (msg) {
                $.messager.progress('close');
                var result = msg.responseText.split('-');
                switch (result[0]) {
                    case "success":
                        window.location.href = "/Home/Index";
                        break;
                    case "failue":
                        $.messager.alert('提示', result[1], 'info');
                        break;
                    default:
                        $.messager.alert('提示', "登录出错!", 'info');
                        break;
                }
            }
        });
    };

});