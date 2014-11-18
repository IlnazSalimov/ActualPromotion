using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VolgaStroyComplect
{
    public class PriceListController : Controller
    {
        private readonly IPriceListRepository _priceListRepository;
        private string priceUploadDirectory = "//Files//PriceLists//";
        public PriceListController(IPriceListRepository priceListRepository)
        {
            this._priceListRepository = priceListRepository;
        }

        public ActionResult GetAll()
        {
            return View(_priceListRepository.GetCompanyPriceLists(AppSettings.CurrentCompany));
        }

        public ActionResult Show(int id)
        {
            PriceList price = _priceListRepository.GetById(id);
            string path = HttpContext.Server.MapPath(price.Path);
            string mime = MimeMapping.GetMimeMapping(price.Path);
            return File(path, mime);
        }

        public ActionResult Download(int id)
        {
            PriceList price = _priceListRepository.GetById(id);
            string path = HttpContext.Server.MapPath(price.Path);
            string mime = MimeMapping.GetMimeMapping(price.Path);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = price.Title, 

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false, 
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(path, mime);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Add(AddPriceList postPrice, HttpPostedFileBase PricePath)
        {
            if (ModelState.IsValid && (PricePath != null && PricePath.ContentLength > 0))
            {
                PriceList newPrice = new PriceList();
                
                newPrice.Title = postPrice.Title;
                newPrice.UploadDate = DateTime.Now;
                newPrice.CompanyId = AppSettings.CurrentCompany;

                /*Сохранение фото*/
                var fileName = Path.GetFileName(PricePath.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(priceUploadDirectory), fileName);
                PricePath.SaveAs(savingPath);
                newPrice.Path = VirtualPathUtility.ToAbsolute(Path.Combine(priceUploadDirectory, fileName));

                _priceListRepository.Insert(newPrice);
                _priceListRepository.SaveChanges();

                return RedirectToAction("GetAll");
                //throw new HttpException(403, "Forbidden");
            }
            else
                return View(postPrice);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PriceList price = _priceListRepository.GetById(id);
            return View(new EditPriceList
            {
                ID = price.ID,
                Title = price.Title
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditPriceList model)
        {
            if (ModelState.IsValid)
            {
                PriceList pice = _priceListRepository.GetById(model.ID);
                pice.Title = model.Title;
                _priceListRepository.Update(pice);
                _priceListRepository.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("GetAll");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (id > 0)
            {
                PriceList price = _priceListRepository.GetById(id);
                _priceListRepository.Delete(price);
                _priceListRepository.SaveChanges();
                return Json(new { success = true, message = "Запись была успешно удалена" });
            }
            else
                return Json(new { 
                    success = false, 
                    message = "Произошла ошибка в удалении, попробуйте еще раз" 
                });
        }
    }
}
