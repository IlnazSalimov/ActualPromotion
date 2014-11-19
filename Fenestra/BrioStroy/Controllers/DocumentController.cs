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
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IProductRepository productRepository;
        private string priceUploadDirectory = "//Files//Documents//";
        public DocumentController(IDocumentRepository _documentRepository, IProductRepository _productRepository)
        {
            this.documentRepository = _documentRepository;
            this.productRepository = _productRepository;
        }
        public ActionResult GetAll()
        {
            return View(documentRepository.GetCompanyDocuments(AppSettings.CurrentCompany));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Document doc = documentRepository.GetById(id);
            return View(new EditDocument
            {
                ID = doc.ID,
                DocumentTitle = doc.DocumentTitle
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditDocument model)
        {
            if (ModelState.IsValid)
            {
                Document doc = documentRepository.GetById(model.ID);
                doc.DocumentTitle = model.DocumentTitle;
                documentRepository.Update(doc);
                documentRepository.SaveChanges();
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
                Document doc = documentRepository.GetById(id);
                documentRepository.Delete(doc);
                documentRepository.SaveChanges();
                return Json(new { success = true, message = "Запись была успешно удалена" });
            }
            else
                return Json(new
                {
                    success = false,
                    message = "Произошла ошибка в удалении, попробуйте еще раз"
                });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddProductDocument(int productId)
        {
            return View(new AddProductDocument { ProductId = productId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(AddDocument postDoc, HttpPostedFileBase DocumentPath)
        {
            if (ModelState.IsValid && (DocumentPath != null && DocumentPath.ContentLength > 0))
            {
                Document newDoc = new Document();

                newDoc.DocumentTitle = postDoc.DocumentTitle;
                newDoc.UploadDate = DateTime.Now;
                newDoc.CompanyId = AppSettings.CurrentCompany;
                newDoc.PageId = (int)PagesEnum.Documents;

                /*Сохранение фото*/
                var fileName = Path.GetFileName(DocumentPath.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(priceUploadDirectory), fileName);
                DocumentPath.SaveAs(savingPath);
                newDoc.DocumentPath = VirtualPathUtility.ToAbsolute(Path.Combine(priceUploadDirectory, fileName));

                documentRepository.Insert(newDoc);
                documentRepository.SaveChanges();

                return RedirectToAction("GetAll");
                //throw new HttpException(403, "Forbidden");
            }
            else
                return View(postDoc);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddProductDocument(AddProductDocument postDoc, HttpPostedFileBase DocumentPath)
        {
            if (ModelState.IsValid && (DocumentPath != null && DocumentPath.ContentLength > 0))
            {
                Document newDoc = new Document();

                newDoc.DocumentTitle = postDoc.DocumentTitle;
                newDoc.UploadDate = DateTime.Now;
                newDoc.CompanyId = AppSettings.CurrentCompany;
                newDoc.PageId = (int)PagesEnum.Products;
                newDoc.ProductId = postDoc.ProductId;

                /*Сохранение фото*/
                var fileName = Path.GetFileName(DocumentPath.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(priceUploadDirectory), fileName);
                DocumentPath.SaveAs(savingPath);
                newDoc.DocumentPath = VirtualPathUtility.ToAbsolute(Path.Combine(priceUploadDirectory, fileName));

                documentRepository.Insert(newDoc);
                documentRepository.SaveChanges();

                return RedirectToAction("GetAll", "Product");
                //throw new HttpException(403, "Forbidden");
            }
            else
                return View(postDoc);
        }

        public ActionResult Show(int id)
        {
            Document doc = documentRepository.GetById(id);
            string path = HttpContext.Server.MapPath(doc.DocumentPath);
            string mime = MimeMapping.GetMimeMapping(doc.DocumentPath);
            return File(path, mime);
        }

        public ActionResult Download(int id)
        {
            Document doc = documentRepository.GetById(id);
            string path = HttpContext.Server.MapPath(doc.DocumentPath);
            string mime = MimeMapping.GetMimeMapping(doc.DocumentPath);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = doc.DocumentTitle,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(path, mime);
        }

    }
}
