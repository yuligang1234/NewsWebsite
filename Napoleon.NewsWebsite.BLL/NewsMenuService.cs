
using System.Data;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.BLL
{

    public class NewsMenuService : INewsMenuService
    {

        private INewsMenuDao _newsmenuDao;

        public NewsMenuService(INewsMenuDao newsmenuDao)
        {
            _newsmenuDao = newsmenuDao;
        }

        /// <summary>
        ///  获取菜单列表数据
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 10:59:11
        public DataTable GetNewsMenuGrid(string isUse, string parentId = "-1")
        {
            return _newsmenuDao.GetNewsMenuGrid(isUse, parentId);
        }

        /// <summary>
        ///  查询父菜单数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuParentTable(string isParent)
        {
            return _newsmenuDao.GetNewsMenuParentTable(isParent);
        }

        /// <summary>
        ///  查询是否有子菜单数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuParentId(string parentId)
        {
            return _newsmenuDao.GetNewsMenuParentId(parentId);
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuTable(string id)
        {
            return _newsmenuDao.GetNewsMenuTable(id);
        }

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int InsertNewsMenu(NewsMenu model)
        {
            return _newsmenuDao.InsertNewsMenu(model);
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int UpdateNewsMenu(NewsMenu model)
        {
            return _newsmenuDao.UpdateNewsMenu(model);
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int DeleteNewsMenu(string id)
        {
            int i;
            DataTable dt = _newsmenuDao.GetNewsMenuParentId(id);
            if (dt.Rows.Count > 0)//有子菜单,需要先删除
            {
                i = -1;
            }
            else
            {
                i = _newsmenuDao.DeleteNewsMenu(id);
            }
            return i;
        }

    }
}