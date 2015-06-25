
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.DAL
{
    public class NewsUploadFileDao : INewsUploadFileDao
    {

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public DataTable GetNewsUploadFileTable(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,FileTilte,FileUrl,UploadTime,OrderBy from News_UploadFile");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    sb.AppendFormat(" where Id=@id");
                    SqlParameter[] parameters = 
                    {
                        new SqlParameter("@id",id)
                    };
                    dt = DbHelper.GetDataTable(sb.ToString(), parameters);
                }
                else
                {
                    dt = DbHelper.GetDataTable(sb.ToString());
                }
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  根据ID数组查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        public List<NewsUploadFile> GetNewsUploadFilesByIds(string id)
        {
            List<NewsUploadFile> contents = new List<NewsUploadFile>();
            try
            {
                string[] ids = id.Split(',');
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,FileTilte,FileUrl,UploadTime,OrderBy from News_UploadFile where Id in @id");
                contents = DbHelper.GetEnumerables<NewsUploadFile>(sb.ToString(), new { @id = ids });
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return contents;
        }

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int InsertNewsUploadFile(NewsUploadFile model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("insert into News_UploadFile(Id,FileTilte,FileUrl,UploadTime,OrderBy) values(@Id,@FileTilte,@FileUrl,@UploadTime,@OrderBy)");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int UpdateNewsUploadFile(NewsUploadFile model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update News_UploadFile set Id=@Id,FileTilte=@FileTilte,FileUrl=@FileUrl,UploadTime=@UploadTime,OrderBy=@OrderBy where Id=@Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:28
        public int DeleteNewsUploadFile(string id)
        {
            string[] ids = id.Split(',');
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete News_UploadFile where Id in @Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids });
            return i;
        }

    }
}