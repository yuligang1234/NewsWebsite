define(function (require, exports, module) {

    //图片显示区的控件ID,选择图片的控件ID,上传控件的ID,删除控件的ID,隐藏控件的ID
    exports.InitializeUploadImage = function (listId, browserId, uploadId, deleteId, hiddenId) {
        var $list = $('#' + listId),
            uploader;
        // 初始化Web Uploader
        uploader = WebUploader.create({
            // 自动上传。
            auto: false,
            // swf文件路径
            swf: '/include/webuploader/Uploader.swf',
            // 文件接收服务端。
            server: '/Ajax/UploadFile?folderName=Images',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#' + browserId,
            compress: false, //不压缩图片
            // 只允许选择文件，可选。
            accept: {
                title: '*.jpg,*.png',
                extensions: 'jpg,jpeg,png',
                mimeTypes: 'image/*'
            }
        });

        //文件被添加进来之前
        uploader.on('beforeFileQueued', function (file) {
            var imgCount = $('#' + listId + ' .info').length;
            if (imgCount > 0) {
                parent.$.messager.alert('提示', '只能上传一张图片!', 'info');
                return false;
            }
            if (file.size > 2097153) {
                parent.$.messager.alert('提示', '图片大小不能超过2M!', 'info');
                return false;
            }
            return true;
        });

        // 当有文件添加进来的时候
        uploader.on('fileQueued', function (file) {
            var $li = $(
                '<div id="' + file.id + '" class="file-item thumbnail">' +
                '<div class="info">' + file.name +
                '<a style="cursor:pointer;margin-left:10px;" id="' + deleteId + '"><img src="/Content/Image/Button/Delete.gif" /></a>' + '</div>' +
                '</div>'
            );
            $list.append($li);
        });

        //上传服务器
        $('#' + uploadId).on('click', function () {
            parent.$.messager.progress({
                msg: '上传中......'
            });
            uploader.upload();
        });

        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');
            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<p class="progress"><span></span></p>')
                    .appendTo($li)
                    .find('span');
            }
            $percent.css('width', percentage * 100 + '%');
        });

        // 文件上传成功，给item添加成功class, 用样式标记上传成功。
        uploader.on('uploadSuccess', function (file, response) {
            var result = response.message;
            parent.$.messager.progress('close');
            if (result === "success") {
                parent.$.messager.alert('提示', '上传成功!', 'info');
                $('#' + hiddenId).val(response.hiddenId);
            } else {
                parent.$.messager.alert('提示', result, 'info');
            }
        });

        // 文件上传失败，现实上传出错。
        uploader.on('uploadError', function (file, reason) {
            parent.$.messager.progress('close');
            parent.$.messager.alert('提示', "上传失败,请重新选择图片!", 'info');
        });

        // 完成上传完了，成功或者失败，先删除进度条。
        uploader.on('uploadComplete', function (file) {
            parent.$.messager.progress('close');
            $('#' + file.id).find('.progress').remove();
        });

        //删除
        $list.on('click', '#' + deleteId, function () {
            var id = $(this).parent().parent().attr("id");
            var file = uploader.getFile(id);
            var hiddenValue = $('#' + hiddenId).val(); //该图片是已经存在服务器上的,需要从服务器上删除
            if (hiddenValue != "" && hiddenValue != undefined) {
                //从服务器端删除图片信息
                $.ajax({
                    url: '/Ajax/DeleteFile',
                    type: 'post',
                    data: { id: hiddenValue },
                    complete: function (data) {
                        if (data.responseText === "success") {
                            $('#' + hiddenId).val("");
                        }
                    }
                });
            } else {
                uploader.removeFile(file, true);
            }
            $(this).parent().remove();
        });
    };

    //文件显示区的控件ID,选择文件的控件ID,上传控件的ID,删除控件的ID,隐藏控件的ID
    exports.InitializeUploadFile = function (listId, browserId, uploadId, deleteId, hiddenId) {
        var $list = $('#' + listId),
            uploader;
        // 初始化Web Uploader
        uploader = WebUploader.create({
            // 自动上传。
            auto: false,
            // swf文件路径
            swf: '/include/webuploader/Uploader.swf',
            // 文件接收服务端。
            server: '/Ajax/UploadFile?folderName=AttachFiles',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#' + browserId,
            compress: false, //不压缩文件
        });

        //文件被添加进来之前
        uploader.on('beforeFileQueued', function (file) {
            var imgCount = $('#' + listId + ' .info').length;
            if (imgCount > 0) {
                parent.$.messager.alert('提示', '只能上传一个文件!', 'info');
                return false;
            }
            if (file.size > 10485765) {
                parent.$.messager.alert('提示', '文件大小不能超过10M!', 'info');
                return false;
            }
            return true;
        });

        // 当有文件添加进来的时候
        uploader.on('fileQueued', function (file) {
            var $li = $(
                '<div id="' + file.id + '" class="file-item thumbnail">' +
                '<div class="info">' + file.name +
                '<a style="cursor:pointer;margin-left:10px;" id="' + deleteId + '"><img src="/Content/Image/Button/Delete.gif" /></a>' + '</div>' +
                '</div>'
            );
            $list.append($li);
        });

        //上传服务器
        $('#' + uploadId).on('click', function () {
            uploader.upload();
        });

        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');
            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<p class="progress"><span></span></p>')
                    .appendTo($li)
                    .find('span');
            }
            $percent.css('width', percentage * 100 + '%');
        });

        // 文件上传成功，给item添加成功class, 用样式标记上传成功。
        uploader.on('uploadSuccess', function (file, response) {
            var result = response.message;
            if (result === "success") {
                parent.$.messager.alert('提示', '上传成功!', 'info');
                $('#' + hiddenId).val(response.hiddenId);
            } else {
                parent.$.messager.alert('提示', result, 'info');
            }
        });

        // 文件上传失败，现实上传出错。
        uploader.on('uploadError', function (file, reason) {
            parent.$.messager.alert('提示', "上传失败,请重新选择文件!", 'info');
        });

        // 完成上传完了，成功或者失败，先删除进度条。
        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.progress').remove();
        });

        //删除
        $list.on('click', '#' + deleteId, function () {
            var id = $(this).parent().parent().attr("id");
            var file = uploader.getFile(id);
            var hiddenValue = $('#' + hiddenId).val(); //该文件是已经存在服务器上的,需要从服务器上删除
            if (hiddenValue != "" && hiddenValue != undefined) {
                //从服务器端删除文件信息
                $.ajax({
                    url: '/Ajax/DeleteFile',
                    type: 'post',
                    data: { id: hiddenValue },
                    complete: function (data) {
                        if (data.responseText === "success") {
                            $('#' + hiddenId).val("");
                        }
                    }
                });
            } else {
                uploader.removeFile(file, true);
            }
            $(this).parent().remove();
        });
    };

});