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
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private string priceUploadDirectory = "//Files//PriceLists//";
        public ProductController(IProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }

        public ActionResult GetAll()
        {
            return View(productRepository.GetCompanyProducts(AppSettings.CurrentCompany));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Product prod = productRepository.GetById(id);
            return View(new EditProduct
            {
                ID = prod.ID,
                Title = prod.Title,
                Description = prod.Description,
                CompanyId = prod.CompanyId
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditProduct model)
        {
            if (ModelState.IsValid)
            {
                Product prod = productRepository.GetById(model.ID);
                prod.Title = model.Title;
                prod.Description = model.Description;
                productRepository.Update(prod);
                productRepository.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("GetAll");
        }

        /*[Authorize(Roles = "Admin")]
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
        }*/
    }
}
