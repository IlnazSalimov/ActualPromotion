using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.WebHost;
using System.Web.Http;
using Brio.Models;
using Brio;


namespace TIK
{
    /// <summary>
    /// Предоставляет методы конфигурации разрешения зависемостей
    /// </summary>
    public class DependencyConfigure
    {
        /// <summary>
        /// Инициализация и конфигурирование контейнера IoC. Регистрация зависемостей.
        /// </summary>
        public static void Initialize()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IDataContext>().To<Entities>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IArticleRepository>().To<ArticleRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IAuthentication>().To<CustomAuthentication>().InRequestScope();
            kernel.Bind<IBrioContext>().To<BrioContext>();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            kernel.Bind<IFeedbackRepository>().To<FeedbackRepository>();
            kernel.Bind<IPriceListRepository>().To<PriceListRepository>();
            kernel.Bind<IReviewRepository>().To<ReviewRepository>();
            kernel.Bind<IDocumentRepository>().To<DocumentRepository>();

            DependencyResolver.SetResolver(new CustomDependencyResolver(kernel));
            GlobalConfiguration.Configuration.DependencyResolver =
                new NinjectWebApiResolver(kernel);
        }
    }
}