
define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");

    //加载用户信息列表
    exports.LoadGrid = function () {
        var url, gridColumns, title;
        url = '/NavMenu/LoadNavMenuGrid';
        title = '导航菜单';
        gridColumns = [
            { field: 'MenuName', title: '导航菜单名称', halign: 'center', align: 'left', width: 120 },
            { field: 'MenuType', title: '菜单类型', halign: 'center', align: 'center', width: 100 },
            { field: 'MenuId', title: '菜单名称', halign: 'center', align: 'left', width: 120 },
            { field: 'MenuUrl', title: '菜单链接', halign: 'center', align: 'center', width: 120 },
            { field: 'IsUse', title: '是否启用', halign: 'center', align: 'center', width: 100 },
            { field: 'OperationTime', title: '创建时间', halign: 'center', align: 'center', width: 120 },
            { field: 'OrderBy', title: '排序', halign: 'center', align: 'center', width: 100 }
        ];
        easyui.LoadDataGrid("#gridTool", url, gridColumns, title, true, undefined);
    };

    //新增
    exports.FixedAdd = function () {
        var url = "/NavMenu/Add";
        easyui.ShowParentWindow('#myWindow', '导航菜单-新增', url, 700, 300);
    };

    //保存
    exports.SaveAdd = function () {
        var result;
        $('#addNaVMenuForm').form('submit', {
            url: '/NavMenu/SaveAdd',
            onSubmit: function () {
                if (!$('#menuId').combobox('options').readonly && $('#menuId').combobox('getValues')[0] == "") {
                    parent.window.$.messager.alert('提示', "请选择新闻菜单", 'info');
                    return false;
                }
                return true;
            },
            success: function (data) {
                result = data.split('-');
                switch (result[0]) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', result[1], 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').datagrid('reload');//标签页里获取iframe
                        break;
                    default:
                        $.messager.alert('提示', result[1], 'info');
                        break;
                }
            }
        });
    };

    //编辑
    exports.FixedEdit = function () {
        var row = $('#gridTool').treegrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要编辑的数据行！', 'info');
        } else {
            var url = '/NavMenu/Edit?id=' + row.Id + '&randId=' + Math.random();
            easyui.ShowParentWindow('#myWindow', '导航菜单-编辑', url, 700, 300);
        }
    };

    //更新
    exports.UpdateEdit = function () {
        var result;
        $('#editNavMenuForm').form('submit', {
            url: '/NavMenu/UpdateEdit',
            onSubmit: function () {
                if (!$('#menuId').combobox('options').readonly && $('#menuId').combobox('getValues')[0] == "") {
                    parent.window.$.messager.alert('提示', "请选择新闻菜单", 'info');
                    return false;
                }
                return true;
            },
            success: function (data) {
                result = data.split('-');
                switch (result[0]) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', result[1], 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').datagrid('reload');//标签页里获取iframe
                        break;
                    default:
                        $.messager.alert('提示', result[1], 'info');
                        break;
                }
            }
        });
    };

    //删除
    exports.FixedDelete = function () {
        var row = $('#gridTool').treegrid('getSelected'), result;
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要删除的数据行！', 'info');
        } else {
            parent.window.$.messager.confirm('提示', '确定是否删除！', function (res) {
                if (res) {
                    $.ajax({
                        url: '/NavMenu/Delete',
                        type: 'post',
                        data: { id: row.Id },
                        complete: function (msg) {
                            result = msg.responseText.split('-');
                            switch (result[0]) {
                                case "success":
                                    parent.window.$('#myWindow').window('close');
                                    parent.window.$.messager.alert('提示', result[1], 'info');
                                    $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').datagrid('reload'); //标签页里获取iframe
                                    break;
                                default:
                                    parent.window.$.messager.alert('提示', result[1], 'info');
                            }
                        }
                    });
                }
            });
        }
    };

    //新闻类型判断事件
    exports.SelectNewsType = function (data) {
        switch (data.id) {
            case "2015011":
                $('#menuId').combobox('readonly', false);
                $('#menuUrl').textbox('readonly', true).textbox({ required: false }).textbox('clear');
                break;
            case "2015012":
                $('#menuId').combobox('readonly', true).combobox('clear');
                $('#menuUrl').textbox('readonly', false).textbox({ required: true });
                break;
            default:
                $('#menuId').combobox('readonly', false);
                $('#menuUrl').textbox('readonly', true).textbox({ required: false }).textbox('clear');
                break;
        }
    };

});