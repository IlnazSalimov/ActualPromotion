using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioStroy
{
    public class ArticlesController : Controller
    {
        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// Экземпляр класса InvestContext, предоставляет доступ к системным данным приложения.
        /// Может быть использован для доступа к текущему авторизованному пользователю
        /// </summary>
        private readonly IBrioContext brioContext;

        public ArticlesController(IArticleRepository _articleRepository, IBrioContext _brioContext)
        {
            this.articleRepository = _articleRepository;
            this.brioContext = _brioContext;
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
    }
}
