
define(function (require, exports, module) {

    var seriaze = require('../PublicJs/Format/SerializeFunc.js');

    //加载内容列表和分页
    exports.LoadList = function (id) {
        var pageSize = 10, currentPage = 1, pageCount = 0, json;
        $.ajax({
            url: '/Controls/LoadList',
            type: 'POST',
            datatype: 'json',
            data: { id: id, currentPage: currentPage, pageSize: pageSize },
            success: function (data) {
                exports.LoadTable("#content", data);
                json = seriaze.DeserializeJson(data);
                pageCount = json.PageCount; //取到总页数
                currentPage = json.CurrentPage; //得到当前页
                $.jqPaginator('#paging', {
                    totalPages: pageCount,
                    visiblePages: pageSize,
                    currentPage: currentPage,
                    onPageChange: function (num, type) {
                        $.ajax({
                            url: '/Controls/LoadList',
                            type: 'POST',
                            datatype: 'json',
                            data: { id: id, currentPage: num, pageSize: pageSize },
                            success: function (callData) {
                                exports.LoadTable("#content", callData);
                            }
                        });
                    }
                });
            }
        });
    };

    //将返回的数据组装成列表
    exports.LoadTable = function (selector, data) {
        if (data != null) {
            $(selector).empty();//清空
            $.each(seriaze.DeserializeJson(data).List, function (index, item) {
                $(selector).append("<li><span><a href='/Home/Contents?id=" + item.Id + "' target='_blank'>" + item.NewsTitle + "</a></span><ii>" + item.RleaseTime + "</ii></li>");
            });
        }
    };

});