
namespace Napoleon.NewsWebsite.Model
{
    public class SystemCode
    {

        private string _id;
        /// <summary>
        ///  主键,自定义
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:35
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
        /// Created :2015-06-06 02:11:35
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private string _codeName;
        /// <summary>
        ///  代码名称
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:35
        public string CodeName
        {
            get { return _codeName; }
            set { _codeName = value; }
        }

        private decimal _orderBy;
        /// <summary>
        ///  显示顺序
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:11:35
        public decimal OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }



    }
}
