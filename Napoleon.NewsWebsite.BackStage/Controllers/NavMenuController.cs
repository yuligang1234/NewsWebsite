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
    public class NavMenuController : Controller
    {

        private ISystemNavMenuService _navMenuService;

        public NavMenuController(ISystemNavMenuService menuService)
        {
            _navMenuService = menuService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  加载新闻菜单列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 10:48:05
        public ActionResult LoadNavMenuGrid()
        {
            DataTable menu = _navMenuService.GetSystemNavMenuTable("", PublicFields.Use);
            string json = menu.ConvertTableToGridJson(menu.Rows.Count);
            return Content(json);
        }

        #region 新增

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        ///  保存新增的导航菜单
        /// </summary>
        /// <param name="menuName">导航菜单名称</param>
        /// <param name="menuType">菜单类型</param>
        /// <param name="menuId">新闻菜单</param>
        /// <param name="menuUrl">链接地址</param>
        /// <param name="isUse">是否可用</param>
        /// <param name="orderBy">排序</param>
        /// Author  : Napoleon
        /// Created : 2015-06-09 15:37:10
        public ActionResult SaveAdd(string menuName, string menuType, string menuId, string menuUrl, string isUse, decimal orderBy)
        {
            string result;
            SystemNavMenu nav = new SystemNavMenu();
            nav.Id = CustomId.GetCustomId();
            nav.MenuName = menuName;
            nav.MenuType = int.Parse(menuType);
            nav.MenuId = menuId;
            nav.MenuUrl = menuUrl;
            nav.IsUse = int.Parse(isUse);
            nav.OrderBy = orderBy;
            nav.OperationTime = DateTime.Now;
            int i = _navMenuService.InsertSystemNavMenu(nav);
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
            ViewData["nav"] = _navMenuService.GetSystemNavMenuTable(id);
            return View();
        }

        /// <summary>
        ///  更新新闻菜单
        /// </summary>
        /// <param name="hiddenId">ID</param>
        /// <param name="menuName">导航菜单名称</param>
        /// <param name="menuType">菜单类型</param>
        /// <param name="menuId">新闻菜单</param>
        /// <param name="menuUrl">链接地址</param>
        /// <param name="isUse">是否可用</param>
        /// <param name="orderBy">排序</param>
        /// Author  : Napoleon
        /// Created : 2015-06-09 15:37:10
        public ActionResult UpdateEdit(string hiddenId, string menuName, string menuType, string menuId, string menuUrl, string isUse, decimal orderBy)
        {
            string result;
            SystemNavMenu nav = new SystemNavMenu();
            nav.Id = hiddenId;
            nav.MenuName = menuName;
            nav.MenuType = string.IsNullOrWhiteSpace(menuType) ? 0 : int.Parse(menuType);
            nav.MenuId = menuId;
            nav.MenuUrl = menuUrl;
            nav.IsUse = int.Parse(isUse);
            nav.OrderBy = orderBy;
            nav.OperationTime = DateTime.Now;
            int i = _navMenuService.UpdateSystemNavMenu(nav);
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
            int i = _navMenuService.DeleteSystemNavMenu(id);
            switch (i)
            {
                case 1:
                    result = "success-删除成功!";
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
