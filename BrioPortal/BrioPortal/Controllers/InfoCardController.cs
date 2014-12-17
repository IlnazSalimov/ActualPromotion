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
    public class InfoCardController : Controller
    {
        //
        // GET: /InfoCard/

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly IInfoCardRepository infoCardRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly ICompanyRepository companyRepository;

        public InfoCardController(IInfoCardRepository _infoCardRepository, ICompanyRepository _companyRepository)
        {
            this.infoCardRepository = _infoCardRepository;
            this.companyRepository = _companyRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInfoCard(int id)
        {
            InfoCard infoCard = infoCardRepository.GetById(id);
            if (infoCard != null)
            {
                InfoCardDTO response = new InfoCardDTO
                {
                    Id = infoCard.ID,
                    Adress = infoCard.Adress,
                    BirthDay = infoCard.BirthDay.HasValue ? infoCard.BirthDay.Value : DateTime.MinValue,
                    CompanyId = infoCard.CompanyId,
                    Email = infoCard.Email,
                    GetJobDate = infoCard.GetJobDate.HasValue ? infoCard.GetJobDate.Value : DateTime.MinValue,
                    Name = infoCard.Name,
                    Patronymic = infoCard.Patronymic,
                    Phone = infoCard.Phone,
                    Post = infoCard.Post,
                    Surname = infoCard.Surname,
                    UserId = infoCard.UserId,
                    CompanyName = companyRepository.GetById(infoCard.CompanyId).CompanyName
                };

                return Json(ResponseProcessing.Success(response));
            }
            else
            {
                return Json(ResponseProcessing.Error("Невозможно извлечь данные о сотруднике. Обновите страницу и повторите попытку."));
            }
            
        }

    }
}
