using System.Web.Mvc;
using Napoleon.NewsWebsite.BackStage.UserModule;
using Napoleon.NewsWebsite.Common;
using Napoleon.PublicCommon.Http;

namespace Napoleon.NewsWebsite.BackStage.Controllers
{
    public class HomeController : BaseController
    {

        UserModuleService _userModule = new UserModuleService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeRight()
        {
            return View();
        }

        public ActionResult ChangePw()
        {
            ViewData["User"] = PublicFields.UserCookies.ReadCookie<SystemUser>();
            return View();
        }

        /// <summary>
        ///  保存密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <param name="newPw">newPw</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 13:30:42
        public ActionResult SaveUser(string id, string password, string newPw)
        {
            int count = _userModule.ChangePwXml(id, password, newPw);
            string status = "failue", msg = "修改失败!", json;
            switch (count)
            {
                case -1:
                    msg = "修改失败，请输入正确的原密码！";
                    break;
                case 1:
                    status = "success";
                    msg = "修改成功,请重新登陆系统";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }



    }
}
