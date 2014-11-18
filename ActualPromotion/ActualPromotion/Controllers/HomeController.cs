using ActualPromotion.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualPromotion.Controllers
{
    public class HomeController : Controller
    {
        private readonly string bidUploadDirectory = ConfigurationManager.AppSettings["BidUploadDirectory"].ToString();

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IBidRepository bidRepository;

        public HomeController(IBidRepository bidRepository)
        {
            this.bidRepository = bidRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendFeedback(PostBid model, HttpPostedFileBase TermPath)
        {
            if (ModelState.IsValid)
            {
                Bid newBid = new Bid();

                newBid.Name = model.Name;
                newBid.Email = model.Email;
                newBid.Phone = model.Phone;
                newBid.Message = model.Message;
                newBid.Date = DateTime.Now;

                /*Сохранение файла*/
                var fileName = Path.GetFileName(TermPath.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(bidUploadDirectory), fileName);
                TermPath.SaveAs(savingPath);
                newBid.TermPath = VirtualPathUtility.ToAbsolute(Path.Combine(bidUploadDirectory, fileName));

                bidRepository.Insert(newBid);
                bidRepository.SaveChanges();
                
            }
            return PartialView("_BidForm");
        }
    }
}
