using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.WebHost;
using System.Web.Http;
using ActualPromotion.Models;

namespace ActualPromotion
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
            kernel.Bind<IDataContext>().To<ActualPromotionEntities>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IBidRepository>().To<BidRepository>();
            

            DependencyResolver.SetResolver(new CustomDependencyResolver(kernel));
            GlobalConfiguration.Configuration.DependencyResolver =
                new NinjectWebApiResolver(kernel);
        }
    }
}