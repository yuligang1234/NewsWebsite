
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.DAL
{
    public class NewsMenuDao : INewsMenuDao
    {

        /// <summary>
        ///  获取菜单列表数据
        /// </summary>
        /// <param name="isUse">是否启用</param>
        /// <param name="parentId">0表示父节点,1表示非父节点,-1表示所有节点</param>
        /// Author  : Napoleon
        /// Created : 2015-06-24 11:00:20
        public DataTable GetNewsMenuGrid(string isUse, string parentId = "-1")
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("SELECT nm.Id,nm.ParentId,nm.MenuName,sc1.CodeName AS IsParent,sc.CodeName AS IsUse,nm.OperationTime,nm.OrderBy FROM dbo.News_Menu AS nm LEFT JOIN dbo.System_Code AS sc ON sc.Id=nm.IsUse LEFT JOIN dbo.System_Code AS sc1 ON sc1.Id=nm.IsParent where nm.IsUse=@IsUse");
                switch (parentId)
                {
                    case "-1":
                        sql.Append(" and nm.ParentId!=@ParentId");
                        break;
                    case "0":
                        sql.Append(" and nm.ParentId=@ParentId");
                        break;
                    case "1":
                        parentId = "0";
                        sql.Append(" and nm.ParentId!=@ParentId and nm.IsParent!='2015005'");//二级父节点也去掉
                        break;
                }
                sql.Append(" ORDER BY nm.OrderBy");
                SqlParameter[] parameters =
                {
                    new SqlParameter("@IsUse", isUse),
                    new SqlParameter("@ParentId",parentId)
                };
                dt = DbHelper.GetDataTable(sql.ToString(), parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  查询父菜单数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuParentTable(string isParent)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,ParentId,MenuName,IsParent,IsUse,OperationTime,OrderBy from News_Menu where IsParent=@IsParent ORDER BY OrderBy");
                SqlParameter[] parameters = 
                    {
                        new SqlParameter("@IsParent",isParent)
                    };
                dt = DbHelper.GetDataTable(sb.ToString(), parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  查询是否有子菜单数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuParentId(string parentId)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,ParentId,MenuName,IsParent,IsUse,OperationTime,OrderBy from News_Menu where ParentId=@ParentId ");
                SqlParameter[] parameters = 
                    {
                        new SqlParameter("@ParentId",parentId)
                    };
                dt = DbHelper.GetDataTable(sb.ToString(), parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public DataTable GetNewsMenuTable(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,ParentId,MenuName,IsParent,IsUse,OperationTime,OrderBy from News_Menu");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    sb.AppendFormat(" where Id=@id Order by OrderBy");
                    SqlParameter[] parameters = 
                    {
                        new SqlParameter("@id",id)
                    };
                    dt = DbHelper.GetDataTable(sb.ToString(), parameters);
                }
                else
                {
                    sb.AppendFormat(" Order by OrderBy");
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
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int InsertNewsMenu(NewsMenu model)
        {
            int i;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("insert into News_Menu(Id,ParentId,MenuName,IsParent,IsUse,OperationTime,OrderBy) values(@Id,@ParentId,@MenuName,@IsParent,@IsUse,@OperationTime,@OrderBy)");
                i = DbHelper.ExecuteSql(sql.ToString(), model);
            }
            catch (Exception exception)
            {
                i = -1;
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int UpdateNewsMenu(NewsMenu model)
        {
            int i = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("update News_Menu set Id=@Id,ParentId=@ParentId,MenuName=@MenuName,IsParent=@IsParent,IsUse=@IsUse,OperationTime=@OperationTime,OrderBy=@OrderBy where Id=@Id");
                i = DbHelper.ExecuteSql(sql.ToString(), model);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:25
        public int DeleteNewsMenu(string id)
        {
            int i = 0;
            try
            {
                string[] ids = id.Split(',');
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("delete News_Menu where Id in @Id");
                i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids });
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

    }
}