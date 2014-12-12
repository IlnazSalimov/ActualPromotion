using Brio;
using Brio.Models;
using Brio.Filters;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IInfoCardRepository infoCardRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IBrioContext brioContext;

        public HomeController(ICompanyRepository _companyRepository, IRoleRepository _roleRepository, IInfoCardRepository _infoCardRepository, IBrioContext _brioContext)
        {
            this.brioContext = _brioContext;
            this.companyRepository = _companyRepository;
            this.roleRepository = _roleRepository;
            this.infoCardRepository = _infoCardRepository;

            User currentUser = brioContext.CurrentUser;
            if (currentUser != null)
            {
                if (currentUser.RoleId == (int)Brio.Models.Roles.Admin)
                {
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    RedirectToAction("Index", "Project");
                }
            }
            else
            {
                RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Index()
        {
            IQueryable companies = companyRepository.GetAll();
            SelectList companies_sel = new SelectList(companies, "ID", "CompanyName");
            ViewBag.Companies = companies_sel;

            ViewBag.Roles = roleRepository.GetAll().ToList();

            ViewBag.Admins = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Admin).ToList();
            ViewBag.Clients = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Client).ToList();
            ViewBag.Employees = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Employee).ToList();
            ViewBag.ProjectManager = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.ProjectManager).ToList();

            return View();
        }
    }
}
