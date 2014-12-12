using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioStroy
{
    public class ArticlesController : Controller
    {
        private string photoUploadDirecory = "//Files//Photos//";

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о выполненных работах компании
        /// </summary>
        private readonly ICompanyWorkRepository companyWorkRepository;

        /// <summary>
        /// Экземпляр класса InvestContext, предоставляет доступ к системным данным приложения.
        /// Может быть использован для доступа к текущему авторизованному пользователю
        /// </summary>
        private readonly IBrioContext brioContext;

        public ArticlesController(IArticleRepository _articleRepository, IBrioContext _brioContext, ICompanyWorkRepository _companyWorkRepository)
        {
            this.articleRepository = _articleRepository;
            this.brioContext = _brioContext;
            this.companyWorkRepository = _companyWorkRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(articleRepository.GetByPage(PagesEnum.About, AppSettings.CurrentCompany));
        }

        [Authorize (Roles="Admin")]
        public ActionResult Edit(int articleId)
        {
            Article article = articleRepository.GetById(articleId);
            EditArticle addArticle = new EditArticle
            {
                ID = article.ID,
                Title = article.Title,
                Text = article.Text
            };
            return View(addArticle);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditArticle model)
        {
            if (ModelState.IsValid)
            {
                Article article = articleRepository.GetById(model.ID);
                article.Title = model.Title;
                article.Text = model.Text;
                articleRepository.Update(article);
                articleRepository.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("About");
        }

        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(AddArticle model)
        {
            if (ModelState.IsValid)
            {
                Article newArticle = new Article
                {
                    CompanyId = AppSettings.CurrentCompany,
                    CreateDate = DateTime.Now,
                    PageId = (int)PagesEnum.About,
                    Text = model.Text,
                    Title = model.Title,
                    Author = brioContext.CurrentUser.ID
                };
                articleRepository.Insert(newArticle);
                articleRepository.SaveChanges();
                return RedirectToAction("About");
            }
            else
                return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddCompanyWork()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddCompanyWork(AddCompanyWork model, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid && (ImageUrl != null && ImageUrl.ContentLength > 0))
            {
                CompanyWork companyWork = new CompanyWork
                {
                    CompanyId = AppSettings.CurrentCompany,
                    Title = model.Title,
                    Description = model.Description
                };

                var fileName = Path.GetFileName(ImageUrl.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(photoUploadDirecory), fileName);
                ImageUrl.SaveAs(savingPath);
                companyWork.ImagesUrl = VirtualPathUtility.ToAbsolute(Path.Combine(photoUploadDirecory, fileName));

                companyWorkRepository.Insert(companyWork);
                companyWorkRepository.SaveChanges();
                return RedirectToAction("About");
            }
            else
                return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditCompanyWork(int id)
        {
            CompanyWork companyWork = companyWorkRepository.GetById(id);
            EditCompanyWork editCompanyWork = new EditCompanyWork
            {
                ID = companyWork.ID,
                Title = companyWork.Title,
                Description = companyWork.Description
            };
            return View(editCompanyWork);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditCompanyWork(EditCompanyWork model)
        {
            if (ModelState.IsValid)
            {
                CompanyWork companyWork = companyWorkRepository.GetById(model.ID);
                companyWork.Title = model.Title;
                companyWork.Description = model.Description;
                companyWorkRepository.Update(companyWork);
                companyWorkRepository.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("About");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult DeleteCompanyWork(int id)
        {
            if (id > 0)
            {
                CompanyWork companyWork = companyWorkRepository.GetById(id);
                companyWorkRepository.Delete(companyWork);
                companyWorkRepository.SaveChanges();
                return Json(new { success = true, message = "Запись была успешно удалена" });
            }
            else
                return Json(new { success = false, message = "Произошла ошибка в удалении, попробуйте еще раз" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Delete(int articleId)
        {
            if (articleId > 0)
            {
                Article article = articleRepository.GetById(articleId);
                articleRepository.Delete(article);
                articleRepository.SaveChanges();
                return Json(new { success = true, message = "Запись была успешно удалена" });
            }
            else
                return Json(new { success = false, message = "Произошла ошибка в удалении, попробуйте еще раз" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult DeleteCompanyWorks(int id)
        {
            if (id > 0)
            {
                CompanyWork companyWork = companyWorkRepository.GetById(id);
                companyWorkRepository.Delete(companyWork);
                companyWorkRepository.SaveChanges();
                return Json(new { success = true, message = "Запись была успешно удалена" });
            }
            else
                return Json(new { success = false, message = "Произошла ошибка в удалении, попробуйте еще раз" });
        }

        public JsonResult GetCompanyWorks()
        {
            List<CompanyWork> companyWorks = companyWorkRepository.GetCompanyWorks(AppSettings.CurrentCompany);
            List<CompanyWorkDTO> companyWorksDTO = new List<CompanyWorkDTO>();
            foreach (var c in companyWorks)
            {
                companyWorksDTO.Add(new CompanyWorkDTO
                {
                    CompanyId = c.CompanyId,
                    Description = c.Description,
                    Id = c.ID,
                    ImagesUrl = c.ImagesUrl,
                    Title = c.Title
                });
            }

            return Json(new { success = true, message = "Все удачно", works = companyWorksDTO });
        }
    }
}
