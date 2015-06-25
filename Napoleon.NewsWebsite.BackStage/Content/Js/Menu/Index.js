
define(function (require, exports, module) {

    var easyui = require("../PublicFunc/Easyui.js");

    //加载用户信息列表
    exports.LoadGrid = function () {
        var url, gridColumns, title;
        url = '/Menu/LoadMenuGrid';
        title = '新闻菜单';
        gridColumns = [
            { field: 'MenuName', title: '菜单名称', halign: 'center', align: 'left', width: 120 },
            { field: 'IsParent', title: '是否父节点', halign: 'center', align: 'center', width: 100 },
            { field: 'IsUse', title: '是否启用', halign: 'center', align: 'center', width: 100 },
            { field: 'OperationTime', title: '创建时间', halign: 'center', align: 'center', width: 120 },
            { field: 'OrderBy', title: '排序', halign: 'center', align: 'center', width: 100 }
        ];
        easyui.LoadTreeGrid("#gridTool", url, gridColumns, "Id", "MenuName", title);
    };

    //新增
    exports.FixedAdd = function () {
        var url = "/Menu/Add";
        easyui.ShowParentWindow('#myWindow', '新闻菜单-新增', url, 700, 300);
    };

    //保存
    exports.SaveAdd = function () {
        var result;
        $('#addMenuForm').form('submit', {
            url: '/Menu/SaveAdd',
            success: function (data) {
                result = data.split('-');
                switch (result[0]) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', result[1], 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload');//标签页里获取iframe
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
            var url = '/Menu/Edit?id=' + row.Id + '&randId=' + Math.random();
            easyui.ShowParentWindow('#myWindow', '新闻菜单-编辑', url, 700, 300);
        }
    };

    //更新
    exports.UpdateEdit = function () {
        var result;
        $('#editMenuForm').form('submit', {
            url: '/Menu/UpdateEdit',
            success: function (data) {
                result = data.split('-');
                switch (result[0]) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', result[1], 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload');//标签页里获取iframe
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
                        url: '/Menu/Delete',
                        type: 'post',
                        data: { "id": row.Id },
                        complete: function (msg) {
                            result = msg.responseText.split('-');
                            switch (result[0]) {
                                case "success":
                                    parent.window.$('#myWindow').window('close');
                                    parent.window.$.messager.alert('提示', result[1], 'info');
                                    $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload'); //标签页里获取iframe
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
});