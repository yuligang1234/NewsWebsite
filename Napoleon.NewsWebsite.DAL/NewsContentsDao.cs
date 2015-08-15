
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;
using Napoleon.PublicCommon.Format;

namespace Napoleon.NewsWebsite.DAL
{
    public class NewsContentsDao : INewsContentsDao
    {

        /// <summary>
        ///  获取新闻内容列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-19 10:25:30
        public DataTable GetNewsContents(string newsMenuId, string newsTitle, string newsType, int start, int end)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT new.number,new.Id,new.NewsMenuId,new.NewsTitle,new.NewsType,new.IndexImg,new.HttpUrl,new.AttachId,new.RleaseTime,new.NewsStatus,new.NewsHit FROM (SELECT ROW_NUMBER() OVER(ORDER BY RleaseTime) AS number,nc.Id,nm.MenuName AS NewsMenuId,nc.NewsTitle,sc.CodeName AS NewsType,IndexImg,HttpUrl,uf.FileTilte AS AttachId,RleaseTime,sc1.CodeName AS NewsStatus,NewsHit FROM dbo.News_Contents AS nc LEFT JOIN dbo.System_Code AS sc ON sc.Id=nc.NewsType LEFT JOIN dbo.News_Menu AS nm ON nm.Id=nc.NewsMenuId LEFT JOIN dbo.System_Code AS sc1 ON sc1.Id=nc.NewsStatus LEFT JOIN dbo.News_UploadFile AS uf ON uf.Id=nc.AttachId");
                if (!string.IsNullOrWhiteSpace(newsMenuId))
                {
                    sql.Append(" where nc.NewsMenuId=@newsMenuId");
                    sqlParameters.Add(new SqlParameter("@newsMenuId", newsMenuId));
                }
                sql.Append(") AS new WHERE new.number>@start AND new.number<=@end");
                sqlParameters.Add(new SqlParameter("@start", start));
                sqlParameters.Add(new SqlParameter("@end", end));
                if (!string.IsNullOrWhiteSpace(newsTitle))
                {
                    sql.Append(" AND new.NewsTitle like @newsTitle");
                    sqlParameters.Add(new SqlParameter("@newsTitle", string.Format("%{0}", newsTitle)));
                }
                if (!string.IsNullOrWhiteSpace(newsType))
                {
                    sql.Append(" AND new.NewsType = @newsType");
                    sqlParameters.Add(new SqlParameter("@newsType", newsType));
                }
                dt = DbHelper.GetDataTable(sql.ToString(), sqlParameters.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  获取新闻内容列表的总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-19 11:04:39
        public int GetNewsContentsCount(string newsMenuId, string newsTitle, string newsType)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT count(*) FROM (SELECT ROW_NUMBER() OVER(ORDER BY RleaseTime) AS number,nc.Id,nm.MenuName AS NewsMenuId,nc.NewsTitle,sc.CodeName AS NewsType,IndexImg,HttpUrl,AttachId,RleaseTime,sc1.CodeName AS NewsStatus,NewsHit FROM dbo.News_Contents AS nc LEFT JOIN dbo.System_Code AS sc ON sc.Id=nc.NewsType LEFT JOIN dbo.News_Menu AS nm ON nm.Id=nc.NewsMenuId LEFT JOIN dbo.System_Code AS sc1 ON sc1.Id=nc.NewsStatus");
                if (!string.IsNullOrWhiteSpace(newsMenuId))
                {
                    sql.Append(" where nc.NewsMenuId=@newsMenuId");
                    sqlParameters.Add(new SqlParameter("@newsMenuId", newsMenuId));
                }
                sql.Append(") AS new WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(newsTitle))
                {
                    sql.Append(" AND new.NewsTitle like @newsTitle");
                    sqlParameters.Add(new SqlParameter("@newsTitle", string.Format("%{0}", newsTitle)));
                }
                if (!string.IsNullOrWhiteSpace(newsType))
                {
                    sql.Append(" AND new.NewsType like @newsType");
                    sqlParameters.Add(new SqlParameter("@newsType", newsType));
                }
                dt = DbHelper.GetDataTable(sql.ToString(), sqlParameters.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt.Rows[0][0].ToString().StringToInt();
        }

        /// <summary>
        ///  根据ID查询单条数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-06-24 14:49:57
        public DataTable GetNewsContentById(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format("SELECT nc.Id,nc.NewsMenuId,nc.NewsTitle,nc.NewsType,nc.HttpUrl,uf.Id AS AttachId,uf.FileTilte AS AttachTitle,uf1.Id AS AttachContentId1,uf1.FileTilte AS AttachContentTitle1,uf1.FileUrl as AttachUrl1,uf2.Id AS AttachContentId2,uf2.FileTilte AS AttachContentTitle2,uf2.FileUrl as AttachUrl2,uf3.Id AS AttachContentId3,uf3.FileTilte AS AttachContentTitle3,uf3.FileUrl as AttachUrl3,uf4.Id AS AttachContentId4,uf4.FileTilte as AttachContentTitle4,uf4.FileUrl as AttachUrl4,uf5.Id AS indexId,uf5.FileTilte AS indexImg,nc.NewsContent,nc.NewsHit,nc.RleaseTime,sc.CodeName AS NewsStatus FROM dbo.News_Contents AS nc LEFT JOIN dbo.News_UploadFile AS uf ON uf.Id=nc.AttachId LEFT JOIN dbo.News_UploadFile AS uf1 ON uf1.Id=AttachContent1 LEFT JOIN dbo.News_UploadFile AS uf2 ON uf2.Id=nc.AttachContent2 LEFT JOIN dbo.News_UploadFile AS uf3 ON uf3.Id=nc.AttachContent3 LEFT JOIN dbo.News_UploadFile AS uf4 ON uf4.Id=nc.AttachContent4 LEFT JOIN dbo.News_UploadFile AS uf5 ON uf5.Id=nc.IndexImg LEFT JOIN dbo.System_Code AS sc ON sc.Id=nc.NewsStatus where nc.Id=@id ORDER BY nc.RleaseTime DESC");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@id",id) 
                };
                dt = DbHelper.GetDataTable(sql, parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  根据菜单ID查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        public DataTable GetNewsContentsTable(string newsMenuId, string newsStatus)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT nc.Id,nc.NewsMenuId,nc.NewsTitle,nc.NewsType,nc.HttpUrl,uf.Id AS AttachId,uf.FileTilte AS AttachTitle,uf1.Id AS AttachContentId1,uf1.FileTilte AS AttachContentTitle1,uf2.Id AS AttachContentId2,uf2.FileTilte AS AttachContentTitle2,uf3.Id AS AttachContentId3,uf3.FileTilte AS AttachContentTitle3,uf4.Id AS AttachContentId4,uf4.FileTilte as AttachContentTitle4,uf5.Id AS indexId,uf5.FileUrl AS indexUrl,uf5.FileTilte AS indexImg,nc.NewsContent,nc.NewsHit,nc.RleaseTime,sc.CodeName AS NewsStatus FROM dbo.News_Contents AS nc LEFT JOIN dbo.News_UploadFile AS uf ON uf.Id=nc.AttachId LEFT JOIN dbo.News_UploadFile AS uf1 ON uf1.Id=AttachContent1 LEFT JOIN dbo.News_UploadFile AS uf2 ON uf2.Id=nc.AttachContent2 LEFT JOIN dbo.News_UploadFile AS uf3 ON uf3.Id=nc.AttachContent3 LEFT JOIN dbo.News_UploadFile AS uf4 ON uf4.Id=nc.AttachContent4 LEFT JOIN dbo.News_UploadFile AS uf5 ON uf5.Id=nc.IndexImg LEFT JOIN dbo.System_Code AS sc ON sc.Id=nc.NewsStatus where nc.NewsMenuId = @NewsMenuId");
                parameters.Add(new SqlParameter("@NewsMenuId", newsMenuId));
                if (!string.IsNullOrWhiteSpace(newsStatus))
                {
                    sql.Append(" and nc.NewsStatus = @NewsStatus");
                    parameters.Add(new SqlParameter("@NewsStatus", newsStatus));
                }
                sql.Append(" ORDER BY nc.RleaseTime DESC");
                dt = DbHelper.GetDataTable(sql.ToString(), parameters.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  根据菜单ID查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        public DataTable GetNewsContentsForList(string newsMenuId, string newsStatus = "", int top = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT");
                if (top > 0)
                {
                    sql.AppendFormat(" Top {0}", top);
                }
                sql.AppendFormat(" nc.Id,nm.MenuName,nc.NewsMenuId,nc.NewsTitle,nc.NewsType,nc.HttpUrl,uf.Id AS AttachId,uf.FileTilte AS AttachTitle,uf1.Id AS AttachContentId1,uf1.FileTilte AS AttachContentTitle1,uf2.Id AS AttachContentId2,uf2.FileTilte AS AttachContentTitle2,uf3.Id AS AttachContentId3,uf3.FileTilte AS AttachContentTitle3,uf4.Id AS AttachContentId4,uf4.FileTilte as AttachContentTitle4,uf5.Id AS indexId,uf5.FileUrl AS indexUrl,uf5.FileTilte AS indexImg,nc.NewsContent,nc.NewsHit,nc.RleaseTime,sc.CodeName AS NewsStatus FROM dbo.News_Contents AS nc LEFT JOIN News_Menu AS nm ON nm.Id = nc.NewsMenuId LEFT JOIN dbo.News_UploadFile AS uf ON uf.Id=nc.AttachId LEFT JOIN dbo.News_UploadFile AS uf1 ON uf1.Id=AttachContent1 LEFT JOIN dbo.News_UploadFile AS uf2 ON uf2.Id=nc.AttachContent2 LEFT JOIN dbo.News_UploadFile AS uf3 ON uf3.Id=nc.AttachContent3 LEFT JOIN dbo.News_UploadFile AS uf4 ON uf4.Id=nc.AttachContent4 LEFT JOIN dbo.News_UploadFile AS uf5 ON uf5.Id=nc.IndexImg LEFT JOIN dbo.System_Code AS sc ON sc.Id=nc.NewsStatus where nc.NewsMenuId in ({0}) ", newsMenuId);
                if (!string.IsNullOrWhiteSpace(newsStatus))
                {
                    sql.Append(" and nc.NewsStatus = @NewsStatus");
                    parameters.Add(new SqlParameter("@NewsStatus", newsStatus));
                }
                sql.Append(" ORDER BY nc.RleaseTime DESC");
                dt = DbHelper.GetDataTable(sql.ToString(), parameters.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  查询首页图片,根据日期排序
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-13 14:08:00
        public DataTable GetIndexImages(string newsStatus)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select nc.Id,nc.NewsMenuId,nc.NewsTitle,nu.Id as indexId,nu.FileUrl as IndexUrl,nu.FileTilte as indexImg from News_Contents as nc LEFT JOIN News_UploadFile as nu on nu.Id=nc.IndexImg where LEN(nc.IndexImg)>0");
                if (!string.IsNullOrWhiteSpace(newsStatus))
                {
                    sql.Append(" and nc.NewsStatus = @NewsStatus");
                    parameters.Add(new SqlParameter("@NewsStatus", newsStatus));
                }
                sql.Append(" ORDER BY nc.RleaseTime DESC");
                dt = DbHelper.GetDataTable(sql.ToString(), parameters.ToArray());
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
        public List<NewsContents> GetNewsContentsByIds(string id)
        {
            List<NewsContents> contents = new List<NewsContents>();
            try
            {
                string[] ids = id.Split(',');
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Select Id,NewsMenuId,NewsTitle,NewsType,IndexImg,NewsContent,HttpUrl,AttachId,RleaseTime,NewsStatus,NewsHit from News_Contents where Id in @Id");
                contents = DbHelper.GetEnumerables<NewsContents>(sb.ToString(), new { @Id = ids });
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
        /// Created :2015-06-13 10:54:06
        public int InsertNewsContents(NewsContents model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Insert into News_Contents(Id,NewsMenuId,NewsType,NewsTitle,IndexImg,NewsContent,HttpUrl,AttachId,AttachContent1,AttachContent2,AttachContent3,AttachContent4,RleaseTime,NewsStatus,NewsHit) values(@Id,@NewsMenuId,@NewsType,@NewsTitle,@IndexImg,@NewsContent,@HttpUrl,@AttachId,@AttachContent1,@AttachContent2,@AttachContent3,@AttachContent4,@RleaseTime,@NewsStatus,@NewsHit)");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        public int UpdateNewsContents(NewsContents model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Update News_Contents set NewsMenuId=@NewsMenuId,NewsType=@NewsType,NewsTitle=@NewsTitle,IndexImg=@IndexImg,NewsContent=@NewsContent,HttpUrl=@HttpUrl,AttachId=@AttachId,AttachContent1=@AttachContent1,AttachContent2=@AttachContent2,AttachContent3=@AttachContent3,AttachContent4=@AttachContent4,NewsHit=@NewsHit where Id=@Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  更新审核结果
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-25 11:14:48
        public int UpdateNewsVerify(string id, string verifyId)
        {
            int i;
            try
            {
                string[] ids = id.Split(',');
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Update News_Contents set NewsStatus=@NewsStatus where Id in @Id");
                i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids, @NewsStatus = verifyId });
            }
            catch (Exception exception)
            {
                i = -1;
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        public int DeleteNewsContents(string id)
        {
            string[] ids = id.Split(',');
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete News_Contents where Id in @Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids });
            return i;
        }

    }
}