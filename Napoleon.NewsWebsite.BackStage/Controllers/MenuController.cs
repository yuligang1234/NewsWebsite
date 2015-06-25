using System;
using System.Data;
using System.Web.Mvc;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.Model;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Frame;

namespace Napoleon.NewsWebsite.BackStage.Controllers
{
    public class MenuController : Controller
    {

        private INewsMenuService _menuService;

        public MenuController(INewsMenuService menuService)
        {
            _menuService = menuService;
        }

        #region 列表

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  加载新闻菜单列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 10:48:05
        public ActionResult LoadMenuGrid()
        {
            DataTable menu = _menuService.GetNewsMenuGrid(PublicFields.Use);
            string json = menu.ConvertToTreeGridJson();
            return Content(json);
        }

        #endregion

        #region 新增

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        ///  保存新增的新闻菜单
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="isParent">是否父菜单</param>
        /// <param name="parentId">父菜单ID</param>
        /// <param name="isUse">是否可用</param>
        /// <param name="orderBy">排序</param>
        /// Author  : Napoleon
        /// Created : 2015-06-09 15:37:10
        public ActionResult SaveAdd(string menuName, string isParent, string parentId, string isUse, decimal orderBy)
        {
            string result;
            NewsMenu menu = new NewsMenu();
            menu.Id = CustomId.GetCustomId();
            menu.MenuName = menuName;
            menu.ParentId = parentId;
            menu.IsParent = isParent;
            menu.IsUse = int.Parse(isUse);
            menu.OrderBy = orderBy;
            menu.OperationTime = DateTime.Now;
            int i = _menuService.InsertNewsMenu(menu);
            switch (i)
            {
                case 1:
                    result = "success-新增成功!";
                    break;
                default:
                    result = "failue-新增失败!";
                    break;
            }
            return Content(result);
        }

        #endregion

        #region 编辑

        public ActionResult Edit(string id)
        {
            ViewData["menu"] = _menuService.GetNewsMenuTable(id);
            return View();
        }

        /// <summary>
        ///  更新新闻菜单
        /// </summary>
        /// <param name="hiddenId">ID</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="isParent">是否父菜单</param>
        /// <param name="parentId">父菜单ID</param>
        /// <param name="isUse">是否可用</param>
        /// <param name="orderBy">排序</param>
        /// Author  : Napoleon
        /// Created : 2015-06-09 15:37:10
        public ActionResult UpdateEdit(string hiddenId, string menuName, string isParent, string parentId, string isUse, decimal orderBy)
        {
            string result;
            NewsMenu menu = new NewsMenu();
            menu.Id = hiddenId;
            menu.MenuName = menuName;
            menu.ParentId = parentId;
            menu.IsParent = isParent;
            menu.IsUse = int.Parse(isUse);
            menu.OrderBy = orderBy;
            menu.OperationTime = DateTime.Now;
            int i = _menuService.UpdateNewsMenu(menu);
            switch (i)
            {
                case 1:
                    result = "success-更新成功!";
                    break;
                default:
                    result = "failue-更新失败!";
                    break;
            }
            return Content(result);
        }

        #endregion

        #region 删除数据

        /// <summary>
        ///  根据ID删除新闻菜单数据
        /// </summary>
        /// <param name="id">新闻菜单ID</param>
        /// Author  : Napoleon
        /// Created : 2015-06-09 16:23:50
        public ActionResult Delete(string id)
        {
            string result;
            int i = _menuService.DeleteNewsMenu(id);
            switch (i)
            {
                case 1:
                    result = "success-删除成功!";
                    break;
                case -1:
                    result = "failue-请先删除对应的子菜单!";
                    break;
                default:
                    result = "error-删除出错!";
                    break;
            }
            return Content(result);
        }

        #endregion


    }
}
