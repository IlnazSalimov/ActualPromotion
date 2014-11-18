using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    public class InfoCardController : Controller
    {
        //
        // GET: /InfoCard/

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly IInfoCardRepository infoCardRepository;

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInfoCard(int id)
        {
            InfoCard review = infoCardRepository.GetById(id);
            InfoCardDTO response = new InfoCardDTO
            {
                Id = review.ID,
                Adress = review.Adress,
                BirthDay = review.BirthDay.Value,
                CompanyId = review.CompanyId,
                Email = review.Email,
                GetJobDate = review.GetJobDate.Value,
                Name = review.Name,
                Patronymic = review.Patronymic,
                Phone = review.Phone,
                Post = review.Post,
                Surname = review.Surname,
                UserId = review.UserId
            };
            return Json(response);
        }

    }
}
