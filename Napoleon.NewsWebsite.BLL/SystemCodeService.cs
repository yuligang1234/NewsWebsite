
using System.Data;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.BLL
{

    public class SystemCodeService : ISystemCodeService
    {

        private ISystemCodeDao _systemcodeDao;

        public SystemCodeService(ISystemCodeDao systemcodeDao)
        {
            _systemcodeDao = systemcodeDao;
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public DataTable GetSystemCodeTable(string id)
        {
            return _systemcodeDao.GetSystemCodeTable(id);
        }

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public int InsertSystemCode(SystemCode model)
        {
            return _systemcodeDao.InsertSystemCode(model);
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public int UpdateSystemCode(SystemCode model)
        {
            return _systemcodeDao.UpdateSystemCode(model);
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public int DeleteSystemCode(string id)
        {
            return _systemcodeDao.DeleteSystemCode(id);
        }

    }
}