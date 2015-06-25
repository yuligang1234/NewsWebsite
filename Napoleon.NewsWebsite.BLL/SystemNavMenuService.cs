
using System.Data;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.BLL
{

    public class SystemNavMenuService : ISystemNavMenuService
    {

        private ISystemNavMenuDao _systemnavmenuDao;

        public SystemNavMenuService(ISystemNavMenuDao systemnavmenuDao)
        {
            _systemnavmenuDao = systemnavmenuDao;
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public DataTable GetSystemNavMenuTable(string id)
        {
            return _systemnavmenuDao.GetSystemNavMenuTable(id);
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public DataTable GetSystemNavMenuTable(string id, string isUsed)
        {
            return _systemnavmenuDao.GetSystemNavMenuTable(id, isUsed);
        }

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public int InsertSystemNavMenu(SystemNavMenu model)
        {
            return _systemnavmenuDao.InsertSystemNavMenu(model);
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public int UpdateSystemNavMenu(SystemNavMenu model)
        {
            return _systemnavmenuDao.UpdateSystemNavMenu(model);
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public int DeleteSystemNavMenu(string id)
        {
            return _systemnavmenuDao.DeleteSystemNavMenu(id);
        }

    }
}