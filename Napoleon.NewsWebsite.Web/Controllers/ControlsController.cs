using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.PublicCommon.Format;
using Newtonsoft.Json;

namespace Napoleon.NewsWebsite.Web.Controllers
{
    public class ControlsController : Controller
    {

        private ISystemNavMenuService _navMenu;
        private INewsContentsService _newsContents;
        private INewsMenuService _newsMenu;

        public ControlsController(ISystemNavMenuService navMenu, INewsContentsService newsContents, INewsMenuService newsMenu)
        {
            _navMenu = navMenu;
            _newsContents = newsContents;
            _newsMenu = newsMenu;
        }

        /// <summary>
        ///  菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-03 13:47:05
        public PartialViewResult NavMenu()
        {
            DataTable dt = _navMenu.GetSystemNavMenuTable("", PublicFields.Use);
            ViewData["NavMenu"] = GetNavMenuList(dt);
            return PartialView();
        }

        /// <summary>
        ///  获取导航菜单列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-05 09:38:41
        private string GetNavMenuList(DataTable dt)
        {
            StringBuilder navMenu = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                navMenu.Append("<li>");
                navMenu.Append("<a href=\"");
                if (!string.IsNullOrWhiteSpace(row["MenuId"].ToString()))
                {
                    navMenu.AppendFormat("/Home/List?id={0}", row["MenuId"]);
                }
                else
                {
                    navMenu.AppendFormat("{0}", row["MenuUrl"]);
                }
                navMenu.AppendFormat("\">{0}", row["MenuName"]);
                navMenu.Append("</li>");
            }
            return navMenu.ToString();
        }

        /// <summary>
        ///  滚动图片
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-03 14:01:52
        public PartialViewResult RollPic()
        {
            ViewData["NewsOa"] = PublicFields.NewsOa;//后台系统地址
            DataTable dt = _newsContents.GetIndexImages(PublicFields.Verify);
            return PartialView(dt);
        }

        #region 列表视图

        /// <summary>
        ///  单列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-06 09:44:57
        public ActionResult SingleList(string newsMenuId, string top, string width)
        {
            DataTable dt = List(newsMenuId, top);
            ViewData["ulWidth"] = width;
            ViewData["titleWidth"] = width.StringToInt(400) - 150;
            return PartialView(dt);
        }

        /// <summary>
        ///  排序列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-21 22:34:03
        public ActionResult OrderList(string newsMenuId, string top, string width)
        {
            DataTable dt = List(newsMenuId, top);
            ViewData["ulWidth"] = width;
            ViewData["titleWidth"] = width.StringToInt(400) - 85;
            return PartialView(dt);
        }

        /// <summary>
        ///  公共列表函数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-30 21:31:43
        private DataTable List(string newsMenuId, string top)
        {
            //newsMenuId为父菜单的话,查询所有子菜单
            DataTable parents = _newsMenu.GetNewsMenuParentId(newsMenuId);
            if (parents.Rows.Count > 0)
            {
                newsMenuId = "";
            }
            foreach (DataRow parent in parents.Rows)
            {
                newsMenuId += parent["Id"] + ",";
            }
            DataTable dt = _newsContents.GetNewsContentsForList(newsMenuId.Trim(',').SwitchArray(), PublicFields.Verify, top.StringToInt(6));
            return dt;
        }

        #endregion

        #region 当前位置

        /// <summary>
        ///  当前位置
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-31 21:37:36
        public PartialViewResult Position(string id)
        {
            DataTable dt = _newsMenu.GetNewsMenuTable("");
            int i = 10;
            SortedDictionary<int, DataRow> position = GetPosition(i, id, dt, new SortedDictionary<int, DataRow>());
            StringBuilder sb = new StringBuilder();
            DataRow row;
            foreach (var dic in position)
            {
                row = dic.Value;
                sb.AppendFormat(" &gt; <a href='{0}' >{1}</a>", "/Home/List?id=" + row["Id"], row["MenuName"]);
            }
            ViewData["position"] = sb.ToString();
            return PartialView();
        }

        /// <summary>
        ///  获取位置列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-04 10:52:20
        private SortedDictionary<int, DataRow> GetPosition(int i, string id, DataTable dt, SortedDictionary<int, DataRow> position)
        {
            SortedDictionary<int, DataRow> dic = position;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Id"].ToString().Equals(id))
                {
                    i--;
                    dic.Add(i, row);
                    id = row["ParentId"].ToString();
                    break;
                }
            }
            if (!id.Equals("0"))
            {
                GetPosition(i, id, dt, dic);
            }
            return dic;
        }

        #endregion

        #region 左侧菜单列表

        /// <summary>
        ///  左侧菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-31 21:47:26
        public ActionResult Menu(string id)
        {
            DataTable dt = _newsMenu.GetNewsMenuTable("");
            string parentId = GetMenuParentId(id, dt);
            ViewData["Menu"] = GetMenuList(parentId, dt);
            return PartialView();
        }

        /// <summary>
        ///  上一级菜单的ID
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-04 19:42:24
        private string GetMenuParentId(string id, DataTable dt)
        {
            DataRow[] rows = dt.Select("ParentId='" + id + "'");
            if (rows.Count() <= 1)//有子菜单的节点
            {
                rows = dt.Select("Id='" + id + "'");
                id = rows[0]["ParentId"].ToString();
            }
            return id;
        }

        /// <summary>
        ///  获取整个菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-04 20:18:51
        private string GetMenuList(string parentId, DataTable dt)
        {
            StringBuilder menu = new StringBuilder();
            foreach (DataRow parentRow in dt.Rows)
            {
                if (parentRow["Id"].ToString().Equals(parentId))
                {
                    //父菜单
                    menu.AppendFormat("<div class=\"cTitleBx\"><p><img src=\"../../Content/Image/Controls/ico20.jpg\" />{0}</p></div>", parentRow["MenuName"]);
                    menu.Append("<ul class=\"ul5\">");
                    foreach (DataRow childRow in dt.Rows)
                    {
                        if (childRow["ParentId"].Equals(parentRow["Id"]))
                        {
                            //子菜单
                            menu.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", "/Home/List?id=" + childRow["Id"], childRow["MenuName"]);
                        }
                    }
                    menu.Append("</ul>");
                }
            }
            return menu.ToString();
        }

        #endregion

        #region 内容列表(分页)

        /// <summary>
        ///  内容列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-05 14:23:05
        public PartialViewResult ContentList(string id)
        {
            DataTable dt = _newsMenu.GetNewsMenuTable("");
            ViewData["MenuName"] = GetMenuName(dt, ref id);
            ViewData["Id"] = id;
            return PartialView();
        }

        /// <summary>
        ///  获取当前菜单名称
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-06 11:01:19
        private string GetMenuName(DataTable dt, ref string id)
        {
            DataRow[] rows = dt.Select("ParentId='" + id + "'");
            if (rows.Count() <= 0)
            {
                rows = dt.Select("Id='" + id + "'");
            }
            id = rows[0]["Id"].ToString();//有子节点的话,子节点id覆盖父节点id
            return rows[0]["MenuName"].ToString();
        }

        /// <summary>
        ///  加载内容分页列表
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页个数</param>
        /// Author  : Napoleon
        /// Created : 2015-08-05 20:29:59
        public ActionResult LoadList(string id, int currentPage, int pageSize)
        {
            int start = (currentPage - 1) * pageSize;
            int end = currentPage * pageSize;
            DataTable content = _newsContents.GetNewsContents(id, "", "", start, end);
            int total = _newsContents.GetNewsContentsCount(id, "", "");
            int pageCount = total % pageSize != 0 ? total / pageSize + 1 : total / pageSize;//总页数
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.DateFormatString = "yyyy-MM-dd";
            string json = "{\"PageCount\":" + pageCount + ",\"CurrentPage\":" + currentPage + ",\"List\":" + JsonConvert.SerializeObject(content, setting) + "}";
            return Content(json);
        }

        #endregion


    }
}
