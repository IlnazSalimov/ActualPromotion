using Brio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TIK
{
    public class HomeController : Controller
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

        public HomeController(IArticleRepository _articleRepository, IBrioContext _brioContext)
        {
            this.articleRepository = _articleRepository;
            this.brioContext = _brioContext;
        }

        public ActionResult Index()
        {
            return View(articleRepository.GetByPage("About", AppSettings.CurrentCompany.ID));
        }

    }
}
