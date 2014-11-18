using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JelDorNiiProject
{
    public static class AppSettings
    {
        public static int CurrentCompany = Convert.ToInt32(ConfigurationSettings.AppSettings["CurrentCompany"]);
    }
}