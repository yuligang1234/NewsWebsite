using System;
using System.Web;
using System.IO;
using System.Collections;
using Napoleon.PublicCommon.Base;

namespace Napoleon.NewsWebsite.Common
{

    /// <summary>
    /// UEditor编辑器通用上传类
    /// </summary>
    public static class Uploader
    {
        static string _state = "SUCCESS";
        static string _url;
        static string _currentType;
        static string _uploadpath;
        static string _filename;
        static string _original;
        static HttpPostedFileBase _uploadFile;

        /**
      * 上传文件的主处理方法
      */
        public static Hashtable UpFile(HttpPostedFileBase file, string pathbase, string[] filetype, int size)
        {
            pathbase = pathbase + "/";
            _uploadpath = HttpContext.Current.Server.MapPath(pathbase);//获取文件上传路径
            try
            {
                _uploadFile = file;
                _original = _uploadFile.FileName;

                //目录创建
                CreateFolder();

                //格式验证
                if (CheckType(filetype))
                {
                    _state = "不允许的文件类型";
                }
                //大小验证
                if (CheckSize(size))
                {
                    _state = "文件大小超出网站限制";
                }
                //保存图片
                if (_state == "SUCCESS")
                {
                    _filename = ReName();
                    _uploadFile.SaveAs(_uploadpath + _filename);
                    _url = pathbase + _filename;
                }
            }
            catch (Exception)
            {
                _state = "未知错误";
                _url = "";
            }
            return GetUploadInfo();
        }

        /**
         * 获取上传信息
         */
        private static Hashtable GetUploadInfo()
        {
            Hashtable infoList = new Hashtable();
            infoList.Add("state", _state);
            infoList.Add("url", _url);
            infoList.Add("original", _original);
            infoList.Add("title", _original);
            infoList.Add("size", _uploadFile.ContentLength);
            infoList.Add("type", Path.GetExtension(_original));
            return infoList;
        }

        /**
         * 重命名文件
         */
        private static string ReName()
        {
            return CustomId.GetCustomId() + GetFileExt();
        }

        /**
         * 文件类型检测
         */
        private static bool CheckType(string[] filetype)
        {
            _currentType = GetFileExt();
            return Array.IndexOf(filetype, _currentType) == -1;
        }

        /**
         * 文件大小检测
         */
        private static bool CheckSize(int size)
        {
            return _uploadFile.ContentLength >= (size * 1024 * 1024);
        }

        /**
         * 获取文件扩展名
         */
        private static string GetFileExt()
        {
            string[] temp = _uploadFile.FileName.Split('.');
            return "." + temp[temp.Length - 1].ToLower();
        }

        /**
         * 按照日期自动创建存储文件夹
         */
        private static void CreateFolder()
        {
            if (!Directory.Exists(_uploadpath))
            {
                Directory.CreateDirectory(_uploadpath);
            }
        }

        /**
         * 删除存储文件夹
         */
        public static void DeleteFolder(string path)
        {
        }
    }

}
