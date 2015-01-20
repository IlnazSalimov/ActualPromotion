using Brio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal
{
    public static class AppSettings
    {
        public static string PROJECT_DOC_SAVING_PATH = ConfigurationSettings.AppSettings["projectDocSavingPath"];
        public static string AVATAR_SAVING_PATH = ConfigurationSettings.AppSettings["avatarSavingPath"];
        public static string DEFAULT_USER_AVATAR = ConfigurationSettings.AppSettings["defaultUserAvatar"];
        public static string NEWS_PHOTO_SAVING_PATH = ConfigurationSettings.AppSettings["newsPhotoSavingPath"];

        private static IBrioContext brioContext = DependencyResolver.Current.GetService(typeof(IBrioContext)) as IBrioContext;
        public static int CurrentUserCompany = Convert.ToInt32(brioContext.CurrentUser.CompanyId);
    }
}