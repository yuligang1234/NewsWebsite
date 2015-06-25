using System;

namespace Napoleon.NewsWebsite.Model
{
    public class SystemNavMenu
    {

        private string _id;
        /// <summary>
        ///  主键,自定义
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _menuName;
        /// <summary>
        ///  菜单名称
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }

        private int _menuType;
        /// <summary>
        ///  1-链接,2-栏目
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
        public int MenuType
        {
            get { return _menuType; }
            set { _menuType = value; }
        }

        private string _menuId;
        /// <summary>
        ///  新闻菜单ID
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
        public string MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        private string _menuUrl;
        /// <summary>
        ///  链接地址
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
        public string MenuUrl
        {
            get { return _menuUrl; }
            set { _menuUrl = value; }
        }

        private int _isUse;
        /// <summary>
        ///  0-禁用,1-启用
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:46
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
        /// Created :2015-06-06 02:11:46
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
        /// Created :2015-06-06 02:11:46
        public decimal OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }



    }
}
