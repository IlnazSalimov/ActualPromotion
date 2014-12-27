using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brio
{
    public static class BrioAppSettings
    {
        private static IBrioContext brioContext = DependencyResolver.Current.GetService(typeof(IBrioContext)) as IBrioContext;

        public static int CurrentUserCompany = Convert.ToInt32(brioContext.CurrentUser.CompanyId);
    }
}
