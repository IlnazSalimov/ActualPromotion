using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

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
        private readonly IDivisionRepository divisionRepository;

        public EmployeeController(ICompanyRepository _companyRepository, IRoleRepository _roleRepository,
            IInfoCardRepository _infoCardRepository, IDivisionRepository _divisionRepository)
        {
            this.companyRepository = _companyRepository;
            this.roleRepository = _roleRepository;
            this.infoCardRepository = _infoCardRepository;
            this.divisionRepository = _divisionRepository;
        }
        public ActionResult Index()
        {
            IQueryable companies = companyRepository.GetAll();
            SelectList companies_sel = new SelectList(companies, "ID", "CompanyName");
            ViewBag.Companies = companies_sel;

            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Message = TempData["Message"];
            ViewBag.CreateDivision = TempData["CreateDivision"] != null ? TempData["CreateDivision"] : new CreateDivision();

            return View(companyRepository.GetAll().ToList());
        }

        [HttpPost]
        public ActionResult AddDivision(CreateDivision model)
        {
            if (ModelState.IsValid)
            {
                Division division = new Division
                {
                    Name = model.Name,
                    Head = model.Head,
                    CompanyId = model.CompanyId
                };

                try
                {
                    divisionRepository.Insert(division);
                    divisionRepository.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["IsSuccess"] = false;
                    TempData["Message"] = "Сохранение отдела закончилось неудачей. Попробуйте повторить попвтку.";
                    TempData["CreateDivision"] = model;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Пожалуйста проверьте введенные данные.";
                TempData["CreateDivision"] = model;
                return RedirectToAction("Index");
            }

            TempData["IsSuccess"] = true;
            TempData["Message"] = "Отдел успешно создан!";
            TempData["CreateDivision"] = model;
            return RedirectToAction("Index");
        }

    }
}
