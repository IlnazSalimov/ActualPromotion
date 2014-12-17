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
        /// Предоставляет доступ к хранилищу данных об отделах компании
        /// </summary>
        private readonly IDivisionRepository divisionRepository;

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

        public HomeController(ICompanyRepository _companyRepository, IRoleRepository _roleRepository, 
            IInfoCardRepository _infoCardRepository, IBrioContext _brioContext,
            IDivisionRepository _divisionRepository)
        {
            this.brioContext = _brioContext;
            this.companyRepository = _companyRepository;
            this.roleRepository = _roleRepository;
            this.infoCardRepository = _infoCardRepository;
            this.divisionRepository = _divisionRepository;

            
        }

        public ActionResult Index()
        {
            User currentUser = brioContext.CurrentUser;
            if (currentUser.RoleId != (int)Brio.Models.Roles.Admin)
            {
                return RedirectToAction("Index", "Project");
            }

            InfoCard currentUserInfoCard = infoCardRepository.GetUserInfoCard(brioContext.CurrentUser.ID);
            List<Division> divisions = divisionRepository.GetCompanyDivisions(currentUserInfoCard.CompanyId).ToList();

            if (divisions.Count < 1)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Не существует ни одного отдела, для создания аккаунта необходимо создать отдел. Создайте отдел в разделе \"Сотрудники\"";
            }

            SelectList divisions_sel = new SelectList(divisions, "ID", "Name");
            ViewBag.Divisions = divisions_sel;

            ViewBag.Roles = roleRepository.GetAll().ToList();

            ViewBag.Admins = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Admin).ToList();
            ViewBag.Clients = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Client).ToList();
            ViewBag.Employees = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.Employee).ToList();
            ViewBag.ProjectManager = infoCardRepository.GetAll().Where(u => u.User.RoleId == (int)Roles.ProjectManager).ToList();

            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Message = TempData["Message"];
            if (TempData["Account"] != null)
            {
                return View(TempData["Account"] as CreatePortalAccount);
            }
            else
            {
                return View();
            }
            
        }
    }
}
