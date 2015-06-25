
using System;

namespace Napoleon.NewsWebsite.Common
{
    public class PublicFields
    {

        /// <summary>
        ///  用户Cookies的Key
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-06 16:04:50
        public static string UserCookies = "User";

        /// <summary>
        ///  项目ID
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-06 17:00:02
        public static string ProjectId = "XWGLXT";

        /// <summary>
        ///  Rc2密钥
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:20:38
        public static string Rc2Key = "Napoleon";

        /// <summary>
        ///  用户权限ID
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-20 14:49:09
        public static string RuleIdCookies = "RuleId";

        #region SystemCode(系统编码)

        /// <summary>
        ///  是否启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:34:55
        public static string IsUse = "2015001";

        /// <summary>
        ///  启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 16:10:52
        public static string Use = "2015002";

        /// <summary>
        ///  是否父节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:34:55
        public static string IsParent = "2015004";

        /// <summary>
        ///  为父节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:40:00
        public static string Parent = "2015005";

        /// <summary>
        ///  是否审核通过
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:34:55
        public static string IsVerify = "2015007";

        /// <summary>
        ///  菜单类型
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-10 16:35:58
        public static string MenuType = "2015010";

        /// <summary>
        ///  新闻类型
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-10 16:38:22
        public static string NewsType = "2015013";

        #endregion

        /// <summary>
        ///  日志导出Excel文件名
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-15 14:41:10
        public static string LogExcelName = DateTime.Now.ToString("yyyyMMddhhmmssff") + ".xls";

    }
}
