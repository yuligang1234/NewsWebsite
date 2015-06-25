using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Napoleon.NewsWebsite.DAL;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.Model;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Format;
using Napoleon.PublicCommon.Frame;

namespace Napoleon.NewsWebsite.BackStage.Controllers
{
    public class ContentsController : Controller
    {

        private INewsContentsService _newsContentsService;

        public ContentsController(INewsContentsService newsContentsService)
        {
            _newsContentsService = newsContentsService;
        }

        #region 列表

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  加载新闻内容列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-19 10:22:55
        public ActionResult LoadContentGrid(string newsMenuId, string newsTitle, string newsType,
            int page, int rows)
        {
            int count = _newsContentsService.GetNewsContentsCount(newsMenuId, newsTitle, newsType);
            DataTable contents = _newsContentsService.GetNewsContents(newsMenuId, newsTitle, newsType,
                rows * (page - 1), rows * page);
            string json = contents.ConvertTableToGridJson(count);
            return Content(json);
        }

        #endregion

        #region 新增

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        ///  保存
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-23 14:52:35
        [ValidateInput(false)]
        public ActionResult SaveAdd(string newsMenuId, string newsType, string newsTitle, string httpUrl, string attachId, string indexImg, string attachContent1, string attachContent2, string attachContent3, string attachContent4, string content)
        {
            string result = "failue-保存失败!";
            try
            {
                NewsContents contents = new NewsContents();
                contents.Id = CustomId.GetCustomId();
                contents.NewsMenuId = newsMenuId;
                contents.NewsTitle = newsTitle;
                contents.NewsType = newsType.StringToInt();
                switch (contents.NewsType)
                {
                    case 2015014:
                        contents.AttachContent1 = attachContent1;
                        contents.AttachContent2 = attachContent2;
                        contents.AttachContent3 = attachContent3;
                        contents.AttachContent4 = attachContent4;
                        contents.IndexImg = indexImg;
                        contents.NewsContent = content;
                        break;
                    case 2015015:
                        contents.HttpUrl = httpUrl;
                        break;
                    case 2015016:
                        contents.AttachId = attachId;
                        break;
                }
                contents.RleaseTime = DateTime.Now;
                contents.NewsStatus = 2015009;//未通过审核
                int i = _newsContentsService.InsertNewsContents(contents);
                if (i > 0)
                {
                    result = "success-保存成功!";
                }
            }
            catch (Exception exception)
            {
                result = "error-保存出错!";
                Log4Dao.InsertLog4(exception.Message);
            }
            return Content(result);
        }

        #endregion

        #region 编辑

        public ActionResult Edit(string id)
        {
            ViewData["NewsContent"] = _newsContentsService.GetNewsContentById(id);
            return View();
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-24 15:32:17
        [ValidateInput(false)]
        public ActionResult UpdateEdit(string hiddenId, string newsMenuId, string newsType, string newsTitle, string httpUrl, string attachId, string indexImg, string attachContent1, string attachContent2, string attachContent3, string attachContent4, string content)
        {
            string result = "failue-更新失败!";
            try
            {
                NewsContents contents = new NewsContents();
                contents.Id = hiddenId;
                contents.NewsMenuId = newsMenuId;
                contents.NewsTitle = newsTitle;
                contents.NewsType = newsType.StringToInt();
                switch (contents.NewsType)
                {
                    case 2015014:
                        contents.AttachContent1 = attachContent1;
                        contents.AttachContent2 = attachContent2;
                        contents.AttachContent3 = attachContent3;
                        contents.AttachContent4 = attachContent4;
                        contents.IndexImg = indexImg;
                        contents.NewsContent = content;
                        break;
                    case 2015015:
                        contents.HttpUrl = httpUrl;
                        break;
                    case 2015016:
                        contents.AttachId = attachId;
                        break;
                }
                int i = _newsContentsService.UpdateNewsContents(contents);
                if (i > 0)
                {
                    result = "success-更新成功!";
                }
            }
            catch (Exception exception)
            {
                result = "error-更新出错!";
                Log4Dao.InsertLog4(exception.Message);
            }
            return Content(result);
        }

        #endregion

        /// <summary>
        ///  查看
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-25 10:51:50
        public ActionResult Info(string id)
        {
            ViewData["NewsContent"] = _newsContentsService.GetNewsContentById(id);
            return View();
        }

        /// <summary>
        ///  根据ID删除数据
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-24 15:59:36
        public ActionResult Delete(string ids)
        {
            string result = "failue-删除失败!";
            try
            {
                int i = _newsContentsService.DeleteNewsContents(ids);
                if (i > 0)
                {
                    result = "success-删除成功!";
                }
            }
            catch (Exception exception)
            {
                result = "error-删除出错!";
                Log4Dao.InsertLog4(exception.Message);
            }
            return Content(result);
        }

        /// <summary>
        ///  审核
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-25 10:51:38
        public ActionResult Verify(string ids)
        {
            ViewData["hiddenId"] = ids;
            return View();
        }

        /// <summary>
        ///  确认
        /// </summary>
        /// <param name="ids">操作的ID</param>
        /// <param name="verifyId">审核结果</param>
        /// Author  : Napoleon
        /// Created : 2015-06-25 11:13:01
        public ActionResult SureVerify(string ids, string verifyId)
        {
            string result = "failue-确认失败!";
            try
            {
                if (_newsContentsService.UpdateNewsVerify(ids, verifyId) > 0)
                {
                    result = "success-确认成功!";
                }
            }
            catch (Exception exception)
            {
                result = "error-确认出错!";
                Log4Dao.InsertLog4(exception.Message);
            }
            return Content(result);
        }

    }
}
