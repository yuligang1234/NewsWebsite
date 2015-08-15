using System.Data;
using System.Text;
using System.Web.Mvc;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.IBLL;

namespace Napoleon.NewsWebsite.Web.Controllers
{
    public class HomeController : Controller
    {

        private INewsContentsService _newsContents;

        public HomeController(INewsContentsService newsContents)
        {
            _newsContents = newsContents;
        }

        /// <summary>
        ///  首页
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-31 21:28:07
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  目录列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-07-31 21:27:53
        public ActionResult List(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        /// <summary>
        ///  内容详情
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-07 09:49:10
        public ActionResult Contents(string id)
        {
            DataTable contents = _newsContents.GetNewsContentById(id);
            ViewData["NewsMenuId"] = contents.Rows[0]["NewsMenuId"];//菜单节点
            ViewData["NewsTitles"] = GetContentsTitle(contents);//新闻标题
            ViewData["NewsContent"] = contents.Rows[0]["NewsContent"];//新闻内容
            ViewData["Attachs"] = GetContentsAttach(contents);//新闻附件
            return View();
        }

        /// <summary>
        ///  获取新闻标题
        /// </summary>
        /// Author  : Napoleon
        /// Created : 0001-01-01 00:00:00
        private string GetContentsTitle(DataTable dt)
        {
            StringBuilder titles = new StringBuilder();
            titles.AppendFormat("<h1>{0}</h1><p><span>发布日期:{1}</span>&nbsp;&nbsp;&nbsp;&nbsp;<span>点击数:{2}</span></p>",
                dt.Rows[0]["NewsTitle"], dt.Rows[0]["RleaseTime"], dt.Rows[0]["NewsHit"]);
            return titles.ToString();
        }

        /// <summary>
        ///  获取新闻附件
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-08-07 10:36:03
        private string GetContentsAttach(DataTable dt)
        {
            StringBuilder attach = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachContentTitle1"].ToString()))
            {
                attach.AppendFormat("<li><a href=\"{0}\" title=\"{1}\" >{1}</a></li>", PublicFields.NewsOa + dt.Rows[0]["AttachUrl1"], dt.Rows[0]["AttachContentTitle1"]);
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachContentTitle2"].ToString()))
            {
                attach.AppendFormat("<li><a href=\"{0}\" title=\"{1}\" >{1}</a></li>", PublicFields.NewsOa + dt.Rows[0]["AttachUrl2"], dt.Rows[0]["AttachContentTitle2"]);
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachContentTitle3"].ToString()))
            {
                attach.AppendFormat("<li><a href=\"{0}\" title=\"{1}\" >{1}</a></li>", PublicFields.NewsOa + dt.Rows[0]["AttachUrl3"], dt.Rows[0]["AttachContentTitle3"]);
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["AttachContentTitle3"].ToString()))
            {
                attach.AppendFormat("<li><a href=\"{0}\" title=\"{1}\" >{1}</a></li>", PublicFields.NewsOa + dt.Rows[0]["AttachUrl4"], dt.Rows[0]["AttachContentTitle4"]);
            }
            return attach.ToString();
        }

    }
}
