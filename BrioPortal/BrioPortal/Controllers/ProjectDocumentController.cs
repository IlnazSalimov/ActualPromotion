using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal
{
    [Authorize]
    public class ProjectDocumentController : Controller
    {
        //
        // GET: /ProjectDocument/

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о проектах
        /// </summary>
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly IBrioContext brioContext;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных документах проектов
        /// </summary>
        private readonly IProjectDocumentRepository projectDocumentRepository;

        public ProjectDocumentController(IProjectRepository _projectRepository, IProjectDocumentRepository _projectDocumentRepository,
            IBrioContext _brioContext)
        {
            this.projectRepository = _projectRepository;
            this.projectDocumentRepository = _projectDocumentRepository;
            this.brioContext = _brioContext;
        }

        public ActionResult Index()
        {
            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Message = TempData["Message"];

            return View(projectRepository.GetCompanyProjects(brioContext.CurrentUser.CompanyId));
        }

        public ActionResult GetProjectDocument()
        {
            return View();
        }

        public ActionResult Add(CreateProjectDocument projDoc, HttpPostedFileBase files)
        {
            if (ModelState.IsValid && (files != null && files.ContentLength > 0))
            {
                ProjectDocument projectDocument = new ProjectDocument();

                var fileName = Path.GetFileName(files.FileName);
                projectDocument.DocumentTitle = projDoc.Title;
                projectDocument.UploadDate = DateTime.Now;
                projectDocument.ProjectId = projDoc.ProjectId;

                var savingPath = Path.Combine(HttpContext.Server.MapPath(AppSettings.PROJECT_DOC_SAVING_PATH), fileName);
                files.SaveAs(savingPath);
                projectDocument.DocumentPath = VirtualPathUtility.ToAbsolute(Path.Combine(AppSettings.PROJECT_DOC_SAVING_PATH, fileName));

                projectDocumentRepository.Insert(projectDocument);
                projectDocumentRepository.SaveChanges();
                //throw new HttpException(403, "Forbidden");
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Документ не был добавлен, т.к. не заполнены все поля. Пожалуйста повторите попытку заполнив все поля.";
                return RedirectToAction("Index");
            }

            TempData["IsSuccess"] = true;
            TempData["Message"] = "Документ успешно добавлен!";
            return RedirectToAction("Index");
        }

        public FileResult Download(int id)
        {
            ProjectDocument doc = projectDocumentRepository.GetById(id);
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

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                ProjectDocument doc = projectDocumentRepository.GetById(id);
                projectDocumentRepository.Delete(doc);
                projectDocumentRepository.SaveChanges();

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Документ успешно удален!";
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Произошла ошибка, пожалуйста повторите попытку!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Serch(string q)
        {
            if (q != null && q != "")
            {
                IQueryable<ProjectDocument> docs = projectDocumentRepository.GetCompanyDocuments(brioContext.CurrentUser.CompanyId);

                IQueryable<ProjectDocument> result =
                    from d in docs
                    where d.DocumentTitle.Contains(q)
                    select d;

                return View(result);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}
