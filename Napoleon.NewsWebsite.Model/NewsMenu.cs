using System;

namespace Napoleon.NewsWebsite.Model
{
    public class NewsMenu
    {

        private string _id;
        /// <summary>
        ///  主键,自定义
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _parentId;
        /// <summary>
        ///  父节点
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private string _menuName;
        /// <summary>
        ///  新闻菜单名称
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }

        private string _isParent;
        /// <summary>
        ///  是否为父节点
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public string IsParent
        {
            get { return _isParent; }
            set { _isParent = value; }
        }

        private int _isUse;
        /// <summary>
        ///  0-禁用,1-启用
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public int IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        private DateTime _operationTime;
        /// <summary>
        ///  操作时间
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public DateTime OperationTime
        {
            get { return _operationTime; }
            set { _operationTime = value; }
        }

        private decimal _orderBy;
        /// <summary>
        ///  显示顺序
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:09:49
        public decimal OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }

    }
}
