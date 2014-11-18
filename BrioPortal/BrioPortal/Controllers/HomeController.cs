using Brio;
using Brio.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
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

        public HomeController(ICompanyRepository companyRepository, IRoleRepository roleRepository)
        {
            this.companyRepository = companyRepository;
            this.roleRepository = roleRepository;
        }

        public ActionResult Index()
        {
            IQueryable companies = companyRepository.GetAll();
            SelectList companies_sel = new SelectList(companies, "ID", "CompanyName");
            ViewBag.Companies = companies_sel;

            ViewBag.Roles = roleRepository.GetAll().ToList();
            return View();
        }
    }
}
