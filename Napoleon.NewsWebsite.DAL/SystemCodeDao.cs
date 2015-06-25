
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.NewsWebsite.IDAL;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.DAL
{
    public class SystemCodeDao : ISystemCodeDao
    {

        /// <summary>
        ///  查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public DataTable GetSystemCodeTable(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("select Id,ParentId,CodeName,OrderBy from System_Code");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    sb.AppendFormat(" where ParentId=@ParentId");
                    SqlParameter[] parameters = 
                    {
                        new SqlParameter("@ParentId",id)
                    };
                    dt = DbHelper.GetDataTable(sb.ToString(), parameters);
                }
                else
                {
                    DbHelper.GetDataTable(sb.ToString());
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
        /// Created :2015-06-06 02:02:41
        public int InsertSystemCode(SystemCode model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("insert into System_Code(Id,ParentId,CodeName,OrderBy) values(@Id,@ParentId,@CodeName,@OrderBy)");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public int UpdateSystemCode(SystemCode model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update System_Code set Id=@Id,ParentId=@ParentId,CodeName=@CodeName,OrderBy=@OrderBy where Id=@Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), model);
            return i;
        }

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:41
        public int DeleteSystemCode(string id)
        {
            string[] ids = id.Split(',');
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("delete System_Code where Id in @Id");
            int i = DbHelper.ExecuteSql(sql.ToString(), new { @Id = ids });
            return i;
        }

    }
}