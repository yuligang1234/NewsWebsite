
using System.Data;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.IDAL
{
    public interface ISystemNavMenuDao
    {

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        DataTable GetSystemNavMenuTable(string id);

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        DataTable GetSystemNavMenuTable(string id, string isUsed);

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        int InsertSystemNavMenu(SystemNavMenu model);

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        int UpdateSystemNavMenu(SystemNavMenu model);

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        int DeleteSystemNavMenu(string id);

    }
}