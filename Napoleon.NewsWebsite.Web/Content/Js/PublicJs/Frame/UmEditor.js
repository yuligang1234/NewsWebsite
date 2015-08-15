define(function (require, exports, module) {

    //实例化编辑器(精简版)
    exports.InitializeUmeditor = function () {
        var um = UM.getEditor('content', {
            initialFrameWidth: '100%',
            initialFrameHeight: 500
        });
    };

});