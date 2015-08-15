using System;
using System.Web.Mvc;
using Napoleon.NewsWebsite.Common;
using Napoleon.NewsWebsite.BackStage.UserModule;
using Napoleon.NewsWebsite.DAL;
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
            string status = "failue", msg, json;
            try
            {
                SystemUser user = _service.CheckUserXml(userName, passWord, PublicFields.ProjectId);
                if (user != null)
                {
                    user.WriteCookie(PublicFields.UserCookies);
                    //用户权限
                    SystemUserAndRule rule = _service.GetRuleXml(user.Id, PublicFields.ProjectId);
                    if (rule == null)
                    {
                        msg = "登录失败,该账号不能登录本系统!";
                    }
                    else
                    {
                        rule.RuleId.WriteCookie(PublicFields.RuleIdCookies);
                        status = "success";
                        msg = "登录成功!";
                    }
                }
                else
                {
                    msg = "登录失败,账号或密码错误!";
                }
            }
            catch (Exception exception)
            {
                msg = "登录出错!";
                Log4Dao.InsertLog4(exception.Message);
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
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
