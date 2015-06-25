
using System.Data;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.BLL
{

    public class NewsUploadFileService : INewsUploadFileService
    {

        private INewsUploadFileDao _newsuploadfileDao;

        public NewsUploadFileService(INewsUploadFileDao newsuploadfileDao)
        {
            _newsuploadfileDao = newsuploadfileDao;
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public DataTable GetNewsUploadFileTable(string id)
        {
            return _newsuploadfileDao.GetNewsUploadFileTable(id);
        }

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int InsertNewsUploadFile(NewsUploadFile model)
        {
            return _newsuploadfileDao.InsertNewsUploadFile(model);
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int UpdateNewsUploadFile(NewsUploadFile model)
        {
            return _newsuploadfileDao.UpdateNewsUploadFile(model);
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int DeleteNewsUploadFile(string id)
        {
            return _newsuploadfileDao.DeleteNewsUploadFile(id);
        }

    }
}