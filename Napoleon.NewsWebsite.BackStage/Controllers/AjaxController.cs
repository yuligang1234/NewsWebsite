using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Napoleon.NewsWebsite.BackStage.UserModule;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.Model;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;

namespace Napoleon.NewsWebsite.BackStage.Controllers
{
    public class AjaxController : Controller
    {

        UserModuleService _userModule = new UserModuleService();

        private ISystemCodeService _systemCode;
        private INewsMenuService _newsMenu;
        private INewsUploadFileService _newsUploadFile;

        public AjaxController(ISystemCodeService systemCode, INewsMenuService newsMenu, INewsUploadFileService newsUploadFile)
        {
            _systemCode = systemCode;
            _newsMenu = newsMenu;
            _newsUploadFile = newsUploadFile;
        }

        #region Easyui公共函数

        /// <summary>
        ///  获取菜单操作
        /// </summary>
        /// <param name="menuId">menuId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:06:03
        public ActionResult GetOperate(string menuId)
        {
            string ruleId = PublicFields.RuleIdCookies.ReadCookie();
            List<SystemMenu> operations = _userModule.GetOperationXml(ruleId, menuId, PublicFields.ProjectId).ToList();
            string html = FormatOperate(operations);
            return Content(html);
        }

        /// <summary>
        ///  将菜单操作格式化成html
        /// </summary>
        /// <param name="menus">operation</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:11:46
        private string FormatOperate(List<SystemMenu> menus)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<td colspan=\"{0}\">", menus.Count);
            foreach (SystemMenu menu in menus)
            {
                sb.AppendFormat(
                    "<div style=\"float:left;\"><a href=\"javascript:void(0);\" id=\"{0}\" class=\"easyui-linkbutton\" data-options=\"iconCls:'{1}',plain:true\" style=\"float: left;\">{2}</a> <div class=\"datagrid-btn-separator\"></div></div>",
                    menu.HtmlId, menu.Icon, menu.Name);
            }
            sb.Append("</td>");
            return sb.ToString();
        }

        /// <summary>
        ///  树节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-10 19:46:37
        public ActionResult GetTree()
        {
            string ruleId = PublicFields.RuleIdCookies.ReadCookie();
            string json = _userModule.GetMenuJson(ruleId, PublicFields.ProjectId);
            return Content(json);
        }


        #endregion

        #region 下拉框(Combobox)

        /// <summary>
        ///  加载父菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadParentId()
        {
            DataTable dt = _newsMenu.GetNewsMenuParentTable(PublicFields.Parent);
            string json = dt.ConvertToComboboxJson("Id", "MenuName", defaultText: "无");
            return Content(json);
        }

        /// <summary>
        ///  是否父菜单
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadIsParent()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.IsParent);
            string json = dt.ConvertToComboboxJson("Id", "CodeName");
            return Content(json);
        }

        /// <summary>
        ///  是否启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadIsUse()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.IsUse);
            string json = dt.ConvertToComboboxJson("Id", "CodeName");
            return Content(json);
        }

        /// <summary>
        ///  是否审核通过
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadIsVerify()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.IsVerify);
            string json = dt.ConvertToComboboxJson("Id", "CodeName");
            return Content(json);
        }

        /// <summary>
        ///  菜单类型
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadMenuType()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.MenuType);
            string json = dt.ConvertToComboboxJson("Id", "CodeName");
            return Content(json);
        }

        /// <summary>
        ///  新闻类型(内容/链接/附件)--查询使用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadNewsType()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.NewsType);
            string json = dt.ConvertToComboboxJson("Id", "CodeName", defaultId: "", defaultText: "无");
            return Content(json);
        }

        /// <summary>
        ///  新闻类型(内容/链接/附件)--新建编辑使用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-09 14:22:54
        public ActionResult LoadNewsTypes()
        {
            DataTable dt = _systemCode.GetSystemCodeTable(PublicFields.NewsType);
            string json = dt.ConvertToComboboxJson("Id", "CodeName");
            return Content(json);
        }

        /// <summary>
        ///  新闻菜单(全部)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-10 16:41:57
        public ActionResult LoadMenuList()
        {
            DataTable dt = _newsMenu.GetNewsMenuGrid(PublicFields.Use);
            string json = dt.ConvertToComboboxJson("Id", "MenuName", defaultId: "", defaultText: "无");
            return Content(json);
        }

        /// <summary>
        ///  新闻菜单(非父节点)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-24 11:13:54
        public ActionResult LoadNoParentMenu()
        {
            DataTable dt = _newsMenu.GetNewsMenuGrid(PublicFields.Use, "1");
            string json = dt.ConvertToComboboxJson("Id", "MenuName");
            return Content(json);
        }

        /// <summary>
        ///  新闻菜单(父节点)
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-24 11:15:43
        public ActionResult LoadParentMenu()
        {
            DataTable dt = _newsMenu.GetNewsMenuGrid(PublicFields.Use, "0");
            string json = dt.ConvertToComboboxJson("Id", "MenuName");
            return Content(json);
        }

        #endregion

        #region WebUploader

        /// <summary>
        ///  上传文件到服务器
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">type</param>
        /// <param name="file">file</param>
        /// <param name="folderName">文件夹名称</param>
        /// Author  : Napoleon
        /// Created : 2015-05-08 16:14:24
        public ActionResult UploadFile(string id, string type, HttpPostedFileBase file, string folderName)
        {
            try
            {
                string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "UploadFiles\\" + folderName);
                if (Request.Files.Count == 0)//没有上传的文件
                {
                    return Json(new { jsonrpc = 2.0, message = "没有文件可以上传!", id });
                }
                string filePathName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                string serverPath = Path.Combine(localPath, filePathName);
                file.SaveAs(serverPath);//保存到服务器端
                NewsUploadFile files = new NewsUploadFile();
                files.Id = CustomId.GetCustomId();
                files.FileTilte = file.FileName;
                files.FileUrl = "/UploadFiles/" + folderName + "/" + filePathName;
                files.UploadTime = DateTime.Now;
                int i = _newsUploadFile.InsertNewsUploadFile(files);//文件信息保存到数据库
                if (i > 0)
                {
                    return Json(new { jsonrpc = 2.0, message = "success", id, hiddenId = files.Id });
                }
                return Json(new { jsonrpc = 2.0, message = "文件保存失败!", id });
            }
            catch (Exception)
            {
                return Json(new { jsonrpc = 2.0, message = "文件保存出错!", id });
            }
        }

        /// <summary>
        ///  从服务器上删除文件
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-05-08 16:15:27
        public ActionResult DeleteFile(string id)
        {
            string result = "failue";
            try
            {
                DataTable dt = _newsUploadFile.GetNewsUploadFileTable(id);
                if (dt.Rows.Count > 0)//有文件信息存在
                {
                    string url = Server.MapPath(dt.Rows[0]["FileUrl"].ToString());
                    if (System.IO.File.Exists(url))//删除服务器上的文件片
                    {
                        System.IO.File.Delete(url);
                    }
                    int i = _newsUploadFile.DeleteNewsUploadFile(id);
                    if (i > 0)
                    {
                        result = "success";
                    }
                }
            }
            catch (Exception)
            {
                result = "failue";
            }
            return Content(result);
        }

        #endregion

        #region Umeditor

        /// <summary>
        ///  Umeditor图片上传
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-05-12 15:39:07
        public ActionResult UploadUmeditorImg()
        {
            var files = HttpContext.Request.Files;
            string serverPath = "/UploadFiles/Content";
            int size = 10;//大小限制,单位M
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };//图片格式
            Hashtable imgInfo = Uploader.UpFile(files[0], serverPath, filetype, size);
            string json = BuildJson(imgInfo);
            return Content(json);
        }

        /// <summary>
        ///  格式化Umeditor返回值
        /// </summary>
        /// <param name="info">The information.</param>
        /// Author  : Napoleon
        /// Created : 2015-05-13 14:27:37
        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "original", "title", "url", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }

        #endregion

    }
}
