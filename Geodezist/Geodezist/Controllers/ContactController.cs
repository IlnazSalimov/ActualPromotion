using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geodezist.Controllers
{
    public class ContactController : Controller
    {
        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу обращений посетителей
        /// </summary>
        private readonly IFeedbackRepository feedbackRepository;

        public ContactController(ICompanyRepository _contactRepository, IFeedbackRepository _feedbackRepository)
        {
            this.companyRepository = _contactRepository;
            this.feedbackRepository = _feedbackRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Contacts = companyRepository.GetByCompany(AppSettings.CurrentCompany);
            return View();
        }

        [HttpPost]
        public JsonResult SendFeedback(SendFeedback model)
        {
            if (ModelState.IsValid)
            {
                feedbackRepository.Insert(new Feedback { 
                    Email = model.Email, 
                    Name = model.Name, 
                    Phone = model.Phone, 
                    Message = model.Message 
                });

                return Json(new
                {
                    success = true,
                    message = "Отправлено"
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Ошибка"
                });
            }

           
        }
    }
}
