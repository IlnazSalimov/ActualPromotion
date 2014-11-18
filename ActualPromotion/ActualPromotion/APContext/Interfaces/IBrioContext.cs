using BrioLab;
using BrioLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace ActualPromotion
{
    public interface IBrioContext 
    {
        IAuthentication Auth { get; set; }
        User CurrentUser { get; }
    }
}
