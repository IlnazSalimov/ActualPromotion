using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о проектах
        /// </summary>
        private readonly IProjectRepository projectRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о проектах
        /// </summary>
        private readonly IProjectStepRepository projectStepRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о документах проекта
        /// </summary>
        private readonly IProjectDocumentRepository projectDocumentRepository;

        /// <summary>
        /// Экземпляр класса BrioContext, предоставляет доступ к системным данным приложения.
        /// Может быть использован для доступа к текущему авторизованному пользователю
        /// </summary>
        private readonly IBrioContext brioContext;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о информационных карточках сотрудников
        /// </summary>
        private readonly IInfoCardRepository infoCardRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IUserRepository userRepository;

        public ProjectController(IProjectRepository _projectRepository, IProjectDocumentRepository _projectDocumentRepository,
            IBrioContext _brioContext, IInfoCardRepository _infoCardRepository, IUserRepository _userRepository,
            IProjectStepRepository _projectStepRepository)
        {
            this.projectRepository = _projectRepository;
            this.projectDocumentRepository = _projectDocumentRepository;
            this.brioContext = _brioContext;
            this.infoCardRepository = _infoCardRepository;
            this.userRepository = _userRepository;
            this.projectStepRepository = _projectStepRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Users = new SelectList(userRepository.GetAll().ToList(), "ID", "FullName");

            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Message = TempData["Message"];

            ViewBag.Project = TempData["Project"] != null ? TempData["Project"] : new CreateProject();
            ViewBag.ProjectStep = TempData["ProjectStep"] != null ? TempData["ProjectStep"] : new CreateProjectStep();

            return View(projectRepository.GetCompanyProjects(brioContext.CurrentUser.CompanyId));
        }

        [HttpPost]
        public JsonResult GetProject(int id)
        {
            Project project = projectRepository.GetById(id);

            List<ProjectDocument> documents = projectDocumentRepository.GetProjectDocuments(id).ToList();
            List<ProjectDocumentDTO> documentsDTO = new List<ProjectDocumentDTO>();


            foreach (ProjectDocument pd in documents)
            {
                documentsDTO.Add(new ProjectDocumentDTO
                {
                    DocumentPath = pd.DocumentPath,
                    DocumentTitle = pd.DocumentTitle,
                    Id = pd.ID,
                    ProjectId = pd.ProjectId.HasValue ? pd.ProjectId.Value : 0,
                    UploadDate = pd.UploadDate
                });
            }

            ProjectDTO projectTransferObj = new ProjectDTO
            {
                Id = project.ID,
                CompanyId = infoCardRepository.GetUserInfoCard(brioContext.CurrentUser.ID).CompanyId,
                CreateDate = DateTime.Now,
                Description = project.Description,
                EndDate = project.EndDate.HasValue ? project.EndDate.Value : DateTime.MinValue,
                ResponsibleUserId = project.ResponsibleUserId,
                StartDate = project.StartDate.HasValue ? project.StartDate.Value : DateTime.MinValue,
                Tile = project.Tile,
                Documents = documentsDTO
            };
            

            if (project != null)
            {
                return Json(ResponseProcessing.Success(projectTransferObj, "Проект успешно извлечен."));
            }
            else
            {
                return Json(ResponseProcessing.Error(String.Format("Проекта с идентификатором {0} не существует.", id)));
            }
        }

        [HttpPost]
        public JsonResult GetProjectStep(int id)
        {
            ProjectStep projectSteps = projectStepRepository.GetById(id);

            if (projectSteps != null)
            {
                ProjectStepDTO projectStepsDTO = new ProjectStepDTO
                {
                    Id = projectSteps.ID,
                    Description = projectSteps.Description,
                    Title = projectSteps.Title,
                    ProjectId = projectSteps.ProjectId
                };

                return Json(ResponseProcessing.Success(projectStepsDTO, "Проект успешно извлечен."));
            }
            else
            {
                return Json(ResponseProcessing.Error(String.Format("У проекта с идентификатором {0} этапов не существует.", id)));
            }
        }

        [HttpPost]
        public ActionResult Create(CreateProject createProject, HttpPostedFileBase projectDocument)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();

                project.CompanyId = infoCardRepository.GetUserInfoCard(brioContext.CurrentUser.ID).CompanyId;
                project.CreateDate = DateTime.Now;
                project.Description = createProject.Description;
                project.EndDate = createProject.EndDate;
                project.ResponsibleUserId = createProject.ResponsibleUserId;
                project.StartDate = createProject.StartDate;
                project.Tile = createProject.Tile;
                project.StateId = (int)ProjectStates.Active;

                int insertedProjectId = projectRepository.Insert(project);

                if (projectDocument != null && projectDocument.ContentLength > 0)
                {
                    ProjectDocument doc = new ProjectDocument();

                    var fileName = Path.GetFileName(projectDocument.FileName);
                    doc.DocumentTitle = createProject.DocumentTitle != String.Empty ? createProject.DocumentTitle : fileName;
                    doc.UploadDate = DateTime.Now;
                    doc.ProjectId = insertedProjectId;

                    var savingPath = Path.Combine(HttpContext.Server.MapPath(AppSettings.PROJECT_DOC_SAVING_PATH), fileName);
                    projectDocument.SaveAs(savingPath);
                    doc.DocumentPath = VirtualPathUtility.ToAbsolute(Path.Combine(AppSettings.PROJECT_DOC_SAVING_PATH, fileName));

                    projectDocumentRepository.Insert(doc);
                }

                projectRepository.SaveChanges();

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Проект успешно создан!";
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Проект не был создан, т.к. не заполнены все поля. Пожалуйста повторите попытку заполнив все поля.";
                TempData["Project"] = createProject;
                
                

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

            
        }
        [HttpPost]
        public ActionResult CreateProjectStep(CreateProjectStep createProjectStep)
        {
            if (ModelState.IsValid)
            {
                ProjectStep projectStep = new ProjectStep
                {
                    Description = createProjectStep.Description,
                    ProjectId = createProjectStep.ProjectId,
                    Title = createProjectStep.Title
                };

                projectStepRepository.Insert(projectStep);
                projectStepRepository.SaveChanges();

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Этап проекта успешно создан!";
                TempData["ProjectStep"] = createProjectStep;
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Этап проекта не был создан, т.к. не заполнены все поля. Пожалуйста повторите попытку заполнив все поля.";
                TempData["ProjectStep"] = createProjectStep;
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(EditProject model)
        {
            if (ModelState.IsValid)
            {
                Project project = projectRepository.GetById(model.Id);

                project.Description = model.Description;
                project.EndDate = model.EndDate;
                project.ResponsibleUserId = model.ResponsibleUserId;
                project.StartDate = model.StartDate;
                project.Tile = model.Tile;

                try
                {
                    projectRepository.Update(project);
                    projectRepository.SaveChanges();
                }
                catch(Exception e)
                {
                    TempData["IsSuccess"] = false;
                    TempData["Message"] = "Что-то пошло не так и проект не был сохранен.";
                    return RedirectToAction("Index");
                }
                

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Проект успешно сохранен!";
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Этап проекта не был создан, т.к. не заполнены все поля. Пожалуйста повторите попытку заполнив все поля.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                Project p = projectRepository.GetById(id);
                projectRepository.Delete(p);
                projectRepository.SaveChanges();

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Проект успешно удален!";
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Произошла ошибка, пожалуйста повторите попытку!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteStep(int id)
        {
            if (id > 0)
            {
                ProjectStep ps = projectStepRepository.GetById(id);
                projectStepRepository.Delete(ps);
                projectStepRepository.SaveChanges();

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Этап успешно удален!";
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Произошла ошибка, пожалуйста повторите попытку!";
            }

            return RedirectToAction("Index");
        }
    }
}
