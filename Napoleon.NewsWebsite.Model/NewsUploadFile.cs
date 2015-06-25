using System;

namespace Napoleon.NewsWebsite.Model
{
    public class NewsUploadFile
    {

        private string _id;
        /// <summary>
        ///  主键,自定义
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:10:28
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _fileTilte;
        /// <summary>
        ///  附件名称
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:10:28
        public string FileTilte
        {
            get { return _fileTilte; }
            set { _fileTilte = value; }
        }

        private string _fileUrl;
        /// <summary>
        ///  附件地址
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:10:28
        public string FileUrl
        {
            get { return _fileUrl; }
            set { _fileUrl = value; }
        }

        private DateTime _uploadTime;
        /// <summary>
        ///  上传时间
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:10:28
        public DateTime UploadTime
        {
            get { return _uploadTime; }
            set { _uploadTime = value; }
        }

        private decimal _orderBy;
        /// <summary>
        ///  显示顺序
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:10:28
        public decimal OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }



    }
}
