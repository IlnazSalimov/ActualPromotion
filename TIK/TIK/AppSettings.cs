using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TIK
{
    public static class AppSettings
    {
        private static ICompanyRepository companyRepository = DependencyResolver.Current.GetService<ICompanyRepository>();
        private static int currentCompanyId = Convert.ToInt32(ConfigurationSettings.AppSettings["CurrentCompany"]);
        public static Company CurrentCompany
        {
            get
            {
                return companyRepository.GetById(currentCompanyId);
            }
        }
        
    }
}