
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
    public class SystemNavMenuDao : ISystemNavMenuDao
    {

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public DataTable GetSystemNavMenuTable(string id, string isUsed)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select sn.Id,sn.MenuName,sc2.CodeName AS MenuType, MenuId,sn.MenuUrl,sc1.CodeName as IsUse,sn.OperationTime,sn.OrderBy from System_NavMenu AS sn LEFT JOIN dbo.News_Menu AS sc ON sc.Id=sn.MenuId LEFT JOIN dbo.System_Code AS sc1 ON sc1.Id=sn.IsUse LEFT JOIN dbo.System_Code AS sc2 ON sc2.Id=sn.MenuType where 1=1");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    sb.AppendFormat(" and sn.Id=@id");
                    parameters.Add(new SqlParameter("@id", id));
                }
                if (!string.IsNullOrWhiteSpace(isUsed))
                {
                    sb.AppendFormat(" and sn.IsUse=@isused ");
                    parameters.Add(new SqlParameter("isused", isUsed));
                }
                sb.AppendFormat(" order by sn.OrderBy");
                dt = DbHelper.GetDataTable(sb.ToString(), parameters.ToArray());
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
        /// Created :2015-06-06 02:02:44
        public DataTable GetSystemNavMenuTable(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,MenuName,MenuType,MenuId,MenuUrl,IsUse,OperationTime,OrderBy from System_NavMenu where 1=1");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    sb.AppendFormat(" and Id=@id");
                    parameters.Add(new SqlParameter("@id", id));
                }
                dt = DbHelper.GetDataTable(sb.ToString(), parameters.ToArray());
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
        /// Created :2015-06-06 02:02:44
        public int InsertSystemNavMenu(SystemNavMenu model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("insert into System_NavMenu(Id,MenuName,MenuType,MenuId,MenuUrl,IsUse,OperationTime,OrderBy) values(@Id,@MenuName,@MenuType,@MenuId,@MenuUrl,@IsUse,@OperationTime,@OrderBy)");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public int UpdateSystemNavMenu(SystemNavMenu model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update System_NavMenu set Id=@Id,MenuName=@MenuName,MenuType=@MenuType,MenuId=@MenuId,MenuUrl=@MenuUrl,IsUse=@IsUse,OperationTime=@OperationTime,OrderBy=@OrderBy where Id=@Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:44
        public int DeleteSystemNavMenu(string id)
        {
            string[] ids = id.Split(',');
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete System_NavMenu where Id in @Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids });
            return i;
        }

    }
}