
using System;

namespace Napoleon.NewsWebsite.Model
{
    public class NewsContents
    {

        private string _id;
        /// <summary>
        ///  主键,自定义
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _newsMenuId;
        /// <summary>
        ///  新闻菜单ID
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string NewsMenuId
        {
            get { return _newsMenuId; }
            set { _newsMenuId = value; }
        }

        private string _newsTitle;
        /// <summary>
        ///  新闻标题
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string NewsTitle
        {
            get { return _newsTitle; }
            set { _newsTitle = value; }
        }

        private int _newsType;
        /// <summary>
        ///  新闻类型(内容/链接/附件)
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public int NewsType
        {
            get { return _newsType; }
            set { _newsType = value; }
        }

        private string _httpUrl;
        /// <summary>
        ///  新闻地址
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string HttpUrl
        {
            get { return _httpUrl; }
            set { _httpUrl = value; }
        }

        private string _attachId;
        /// <summary>
        ///  附件ID
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string AttachId
        {
            get { return _attachId; }
            set { _attachId = value; }
        }

        private string _attachContent1;
        /// <summary>
        ///  新闻内容附件1
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string AttachContent1
        {
            get { return _attachContent1; }
            set { _attachContent1 = value; }
        }

        private string _attachContent2;
        /// <summary>
        ///  新闻内容附件2
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string AttachContent2
        {
            get { return _attachContent2; }
            set { _attachContent2 = value; }
        }

        private string _attachContent3;
        /// <summary>
        ///  新闻内容附件3
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string AttachContent3
        {
            get { return _attachContent3; }
            set { _attachContent3 = value; }
        }

        private string _attachContent4;
        /// <summary>
        ///  新闻内容附件4
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string AttachContent4
        {
            get { return _attachContent4; }
            set { _attachContent4 = value; }
        }

        private string _indexImg;
        /// <summary>
        ///  首页图片
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string IndexImg
        {
            get { return _indexImg; }
            set { _indexImg = value; }
        }

        private string _newsContent;
        /// <summary>
        ///  新闻内容
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public string NewsContent
        {
            get { return _newsContent; }
            set { _newsContent = value; }
        }

        private DateTime _rleaseTime;
        /// <summary>
        ///  新闻发布时间
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public DateTime RleaseTime
        {
            get { return _rleaseTime; }
            set { _rleaseTime = value; }
        }

        private int _newsStatus;
        /// <summary>
        ///  0-审核不通过,1-审核通过
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public int NewsStatus
        {
            get { return _newsStatus; }
            set { _newsStatus = value; }
        }

        private Int64 _newsHit;
        /// <summary>
        ///  点击率
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-24 09:46:17
        public Int64 NewsHit
        {
            get { return _newsHit; }
            set { _newsHit = value; }
        }



    }
}
