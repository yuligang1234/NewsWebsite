
define(function (require, exports, module) {

    var easyui = require("../PublicJs/Frame/Easyui.js");

    //加载权限
    exports.LoadOperate = function (selector) {
        var iframeid = window.parent.$('#tabs').tabs('getSelected').find('iframe').attr("id");
        easyui.LoadOperate(selector, iframeid);
    };

    //加载日志列表
    exports.LoadGrid = function () {
        var url, gridColumns, title;
        url = '/Log/LoadDataGrid';
        title = '日志列表';
        gridColumns = [
            { field: 'IpAddress', title: 'IP地址', halign: 'center', align: 'center', width: 100 },
            { field: 'OperateTime', title: '日志时间', halign: 'center', align: 'center', width: 100 },
            { field: 'OperateContent', title: '日志内容', halign: 'center', align: 'left', width: 300 },
            { field: 'OperateUrl', title: '地址', halign: 'center', align: 'center', width: 100 },
            { field: 'OperateType', title: '日志类型', halign: 'center', align: 'center', width: 100 }
        ];
        easyui.LoadPageDataGrid("#gridTool", url, gridColumns, title, undefined, undefined, undefined, exports.FixedInfo);
    };

    //设置页面
    exports.LoadBody = function (h) {
        var height = document.documentElement.clientHeight;
        var width = document.documentElement.clientWidth;
        $('#searchTool').css('width', width - 4 + "px");
        easyui.ResizeDataGrid("#gridTool", height - h, width - 4);
    };

    //查询
    exports.FixedSearch = function () {
        var userName = $('#userName').val();
        var content = $('#content').val();
        var datetime1 = $('#datetime1').datebox('getValue');
        var datetime2 = $('#datetime2').datebox('getValue');
        var parameters = { userName: userName, content: content, datetime1: datetime1, datetime2: datetime2 };
        if (datetime2 < datetime1) {
            parent.window.$.messager.alert('警告', '结束时间不能小于开始时间！', 'warning');
            return;
        }
        easyui.ReloadDataGrid("#gridTool", parameters);
    };

    //导出Excel
    exports.FixedExcel = function () {
        var userName = $('#userName').val();
        var content = $('#content').val();
        var datetime1 = $('#datetime1').datebox('getValue');
        var datetime2 = $('#datetime2').datebox('getValue');
        parent.window.$.messager.progress({
            msg: '导出Excel中...'
        });
        $.ajax({
            url: '/Log/ExcelLog',
            type: 'post',
            data: { userName: userName, content: content, datetime1: datetime1, datetime2: datetime2 },
            success: function (data) {
                window.location = data;
                parent.window.$.messager.progress('close');
            }
        });
    };

    //详情页面
    exports.FixedInfo = function () {
        var row = $('#gridTool').datagrid('getSelected');
        if (row === null || row === undefined) {
            parent.window.$.messager.alert('提示', '请先选择需要查看详情的数据行！', 'info');
            return;
        }
        var url = '/Log/ViewInfo?id=' + row.Id;
        easyui.ShowParentWindow('#myWindow', '日志详情', url, 700, 300);
    };

})