
using System.Data;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.IBLL
{
    public interface INewsMenuService
    {

        /// <summary>
        ///  获取菜单列表数据
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 10:59:11
        DataTable GetNewsMenuGrid(string isUse, string parentId = "-1");

        /// <summary>
        ///  查询父菜单数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        DataTable GetNewsMenuParentTable(string isParent);

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        DataTable GetNewsMenuTable(string id);

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        int InsertNewsMenu(NewsMenu model);

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        int UpdateNewsMenu(NewsMenu model);

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        int DeleteNewsMenu(string id);

    }
}