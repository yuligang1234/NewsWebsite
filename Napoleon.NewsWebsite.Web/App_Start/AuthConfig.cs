
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Napoleon.NewsWebsite.BLL;
using Napoleon.NewsWebsite.DAL;
using Napoleon.NewsWebsite.IBLL;
using Napoleon.NewsWebsite.IDAL;

namespace Napoleon.NewsWebsite.Web
{
    public class AuthConfig
    {

        /// <summary>
        ///  初始化Autofac
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 10:56:19
        public static void InitAutofc()
        {
            ContainerBuilder builder = new ContainerBuilder();
            SetupResolveRules(builder);//注册类
            builder.RegisterControllers(Assembly.GetExecutingAssembly());//注册控制器
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
        }


        /// <summary>
        ///  需要用到的类进行注册
        /// </summary>
        /// Author  :Napoleon
        /// Created :2015-06-06 10:56:19
        private static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<NewsContentsDao>().As<INewsContentsDao>();
            builder.RegisterType<NewsContentsService>().As<INewsContentsService>();
            builder.RegisterType<NewsMenuDao>().As<INewsMenuDao>();
            builder.RegisterType<NewsMenuService>().As<INewsMenuService>();
            builder.RegisterType<NewsUploadFileDao>().As<INewsUploadFileDao>();
            builder.RegisterType<NewsUploadFileService>().As<INewsUploadFileService>();
            builder.RegisterType<SystemCodeDao>().As<ISystemCodeDao>();
            builder.RegisterType<SystemCodeService>().As<ISystemCodeService>();
            builder.RegisterType<SystemNavMenuDao>().As<ISystemNavMenuDao>();
            builder.RegisterType<SystemNavMenuService>().As<ISystemNavMenuService>();
        }


    }
}