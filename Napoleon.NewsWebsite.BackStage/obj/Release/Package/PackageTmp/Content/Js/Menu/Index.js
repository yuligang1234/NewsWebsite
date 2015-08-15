
define(function (require, exports, module) {

    var easyui = require("../PublicJs/Frame/Easyui.js");
    var serialize = require("../PublicJs/Format/SerializeFunc.js");

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

    //设置页面
    exports.LoadBody = function (h) {
        var height = document.documentElement.clientHeight;
        var width = document.documentElement.clientWidth;
        $('#searchTool').css('width', width - 4 + "px");
        easyui.ResizeDataGrid("#gridTool", height - h, width - 4);
    };

    //新增
    exports.FixedAdd = function () {
        var url = "/Menu/Add";
        easyui.ShowParentWindow('#myWindow', '新闻菜单-新增', url, 700, 300);
    };

    //保存
    exports.SaveAdd = function () {
        $('#addMenuForm').form('submit', {
            url: '/Menu/SaveAdd',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload');//标签页里获取iframe
                        break;
                    default:
                        $.messager.alert('提示', json.Msg, 'info');
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
        $('#editMenuForm').form('submit', {
            url: '/Menu/UpdateEdit',
            success: function (data) {
                var json = serialize.DeserializeJson(data);
                switch (json.Status) {
                    case "success":
                        parent.window.$('#myWindow').window('close');
                        parent.window.$.messager.alert('提示', json.Msg, 'info');
                        $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload');//标签页里获取iframe
                        break;
                    default:
                        $.messager.alert('提示', json.Msg, 'info');
                        break;
                }
            }
        });
    };

    //删除
    exports.FixedDelete = function () {
        var row = $('#gridTool').treegrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要删除的数据行！', 'info');
        } else {
            parent.window.$.messager.confirm('提示', '确定是否删除！', function (res) {
                if (res) {
                    $.ajax({
                        url: '/Menu/Delete',
                        type: 'post',
                        data: { "id": row.Id },
                        complete: function (data) {
                            var json = serialize.DeserializeJson(data.responseText);
                            switch (json.Status) {
                                case "success":
                                    parent.window.$.messager.alert('提示', json.Msg, 'info');
                                    $(window.parent.$('#tabs').tabs('getSelected').find('iframe'))[0].contentWindow.$('#gridTool').treegrid('reload'); //标签页里获取iframe
                                    break;
                                default:
                                    parent.window.$.messager.alert('提示', json.Msg, 'info');
                            }
                        }
                    });
                }
            });
        }
    };
});