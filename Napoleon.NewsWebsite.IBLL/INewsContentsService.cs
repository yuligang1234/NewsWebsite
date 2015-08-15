
using System.Collections.Generic;
using System.Data;
using Napoleon.NewsWebsite.Model;

namespace Napoleon.NewsWebsite.IBLL
{
    public interface INewsContentsService
    {

        /// <summary>
        ///  获取新闻内容列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-19 10:25:30
        DataTable GetNewsContents(string newsMenuId, string newsTitle, string newsType,
            int start, int end);

        /// <summary>
        ///  获取新闻内容列表的总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-19 11:04:39
        int GetNewsContentsCount(string newsMenuId, string newsTitle, string newsType);

        /// <summary>
        ///  根据ID查询单条数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-06-24 14:49:57
        DataTable GetNewsContentById(string id);

        /// <summary>
        ///  根据菜单ID查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:20
        DataTable GetNewsContentsTable(string newsMenuId, string newsStatus);

        /// <summary>
        ///  根据菜单ID查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        DataTable GetNewsContentsForList(string newsMenuId, string newsStatus = "", int top = 0);

        /// <summary>
        ///  查询首页图片,根据日期排序
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-13 14:08:00
        DataTable GetIndexImages(string newsStatus);

        /// <summary>
        ///  根据ID数组查询数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-13 10:54:06
        List<NewsContents> GetNewsContentsByIds(string id);

        /// <summary>
        ///  新增数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:20
        int InsertNewsContents(NewsContents model);

        /// <summary>
        ///  更新数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:20
        int UpdateNewsContents(NewsContents model);

        /// <summary>
        ///  更新审核结果
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-25 11:14:48
        int UpdateNewsVerify(string id, string verifyId);

        /// <summary>
        ///  删除数据
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 02:02:20
        int DeleteNewsContents(string ids);

    }
}