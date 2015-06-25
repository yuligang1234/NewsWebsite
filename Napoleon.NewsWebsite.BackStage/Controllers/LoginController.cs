using System.Web.Mvc;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.BackStage.UserModule;
using Napoleon.PublicCommon.Cryptography;
using Napoleon.PublicCommon.Http;

namespace Napoleon.NewsWebsite.BackStage.Controllers
{
    public class LoginController : Controller
    {

        UserModuleService _service = new UserModuleService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  验证用户信息
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-06 16:29:44
        public ActionResult CheckUser(string userName, string passWord)
        {
            string result = "failue-登录失败,账号或密码错误!";
            SystemUser user = _service.CheckUserXml(userName, passWord.EncrypteRc2(PublicFields.Rc2Key), PublicFields.ProjectId);
            if (user != null)
            {
                user.WriteCookie(PublicFields.UserCookies);
                //用户权限
                SystemUserAndRule rule = _service.GetRuleXml(user.Id, PublicFields.ProjectId);
                if (rule == null)
                {
                    return Content("登录失败,该账号不能登录本系统!");
                }
                rule.RuleId.WriteCookie(PublicFields.RuleIdCookies);
                result = "success-登录成功!";
            }
            return Content(result);
        }

        /// <summary>
        ///  退出登录
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-06-08 14:05:35
        public void LoginOut()
        {
            PublicFields.UserCookies.DeleteCookie();
        }

    }
}
