define(function (require, exports, module) {

    var easyui = require("../PublicJs/Frame/Easyui.js");
    var serialize = require("../PublicJs/Format/SerializeFunc.js");

    //退出
    exports.LoginOut = function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {
            if (r) {
                $.ajax({
                    url: '/Login/LoginOut',
                    complete: function () {
                        top.window.location.href = '/Login/Index';
                    }
                });
            }
        });
    };

    //修改密码
    exports.ChangePassWord = function () {
        easyui.ShowWindow('#myWindow', '修改密码', '/Home/ChangePw', '420', '320');
    };

    //保存密码
    exports.SavePw = function () {
        $('#pwForm').form('submit', {
            url: '/Home/SaveUser',
            onSubmit:function(param) {
                param.password = $('#password').val();
                param.newPw = $('#newPw').val();
            },
            success: function (data) {
                var json = serialize.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info', function () {
                            $.ajax({
                                url: '/Login/LoginOut',
                                complete: function () {
                                    top.window.location.href = '/Login/Index';
                                }
                            });
                        });
                        break;
                    default:
                        $.messager.alert('提示', json.Msg, 'info');
                        break;
                }
            }
        });
    };

});