using Brio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о новостях
        /// </summary>
        private readonly INewsRepository newsRepository;

        public NewsController(INewsRepository _newsRepository)
        {
            this.newsRepository = _newsRepository;
        }

        public ActionResult Index()
        {
            return View(newsRepository.GetAll());
        }

        public ActionResult Add()
        {
            return View(newsRepository.GetAll());
        }
    }
}
