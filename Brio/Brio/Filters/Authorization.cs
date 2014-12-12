using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Brio.Filters
{
    public class CheckAuthorize : ActionFilterAttribute
    {
        public Roles[] Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IBrioContext brioContext = (IBrioContext)DependencyResolver.Current.GetService(typeof(IBrioContext));
            //Your code to get the user
            var user = brioContext.CurrentUser;

            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();

            if (user != null)
            {
                if (user.RoleId == (int)Brio.Models.Roles.Admin)
                {
                    redirectTargetDictionary.Add("action", "Index");
                    redirectTargetDictionary.Add("controller", "Home");
                }
                else
                {
                    redirectTargetDictionary.Add("action", "Index");
                    redirectTargetDictionary.Add("controller", "Project");
                }
            }
            else
            {
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Account");
            }

            filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
        }
    }
}