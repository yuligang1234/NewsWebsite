define(function (require, exports, module) {

    var easyui = require('../PublicFunc/Easyui.js');

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
            success: function (data) {
                var result = data.split('-');
                switch (result[0]) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', result[1], 'info', function () {
                            $.ajax({
                                url: '/Login/LoginOut',
                                complete: function () {
                                    top.window.location.href = '/Login/Index';
                                }
                            });
                        });
                        break;
                    case "failue":
                        $.messager.alert('提示', result[1], 'info');
                        break;
                    default:
                        $.messager.alert('提示', "修改出错!", 'info');
                        break;
                }
            }
        });
    };

    //加载菜单树节点
    exports.LoadMenuTree = function (selector) {
        $(selector).tree({
            url: '/Ajax/GetTree?randId=' + Math.random(),
            method: 'get',
            animate: true,
            onClick: function (node) {
                //不是父节点
                if (node.children == undefined) {
                    var id, title, url, icon;
                    id = node.id;
                    title = node.text;
                    url = node.url;
                    icon = node.iconCls;
                    easyui.AddTabs(title, url, icon, true, id);
                }
            }
        });
    };

});