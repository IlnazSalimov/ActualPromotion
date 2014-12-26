using Brio;
using Brio.Models;
using Brio.Filters;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace BrioPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных об отделах компании
        /// </summary>
        private readonly IDivisionRepository divisionRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о компаниях
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IInfoCardRepository infoCardRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IBrioContext brioContext;

        private string avatarDirectory = "//Files//Documents//";

        public HomeController(ICompanyRepository _companyRepository, IRoleRepository _roleRepository, 
            IInfoCardRepository _infoCardRepository, IBrioContext _brioContext,
            IDivisionRepository _divisionRepository, IUserRepository _userRepository)
        {
            this.brioContext = _brioContext;
            this.companyRepository = _companyRepository;
            this.roleRepository = _roleRepository;
            this.infoCardRepository = _infoCardRepository;
            this.divisionRepository = _divisionRepository;
            this.userRepository = _userRepository;
        }

        public ActionResult Index()
        {
            User currentUser = brioContext.CurrentUser;
            if (currentUser.RoleId != (int)Brio.Models.Roles.Admin)
            {
                return RedirectToAction("Index", "Project");
            }

            InfoCard currentUserInfoCard = infoCardRepository.GetUserInfoCard(brioContext.CurrentUser.ID);
            List<Division> divisions = divisionRepository.GetCompanyDivisions(currentUserInfoCard.CompanyId).ToList();

            if (divisions.Count < 1)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Не существует ни одного отдела, для создания аккаунта необходимо создать отдел. Создайте отдел в разделе \"Сотрудники\"";
            }

            SelectList divisions_sel = new SelectList(divisions, "ID", "Name");
            ViewBag.Divisions = divisions_sel;

            ViewBag.Roles = roleRepository.GetAll().ToList();


            ViewBag.Admins = infoCardRepository.GetInfoCardsByRole(Roles.Admin);
            ViewBag.Clients = infoCardRepository.GetInfoCardsByRole(Roles.Client);
            ViewBag.Employees = infoCardRepository.GetInfoCardsByRole(Roles.Employee);
            ViewBag.ProjectManager = infoCardRepository.GetInfoCardsByRole(Roles.ProjectManager);

            ViewBag.IsSuccess = TempData["IsSuccess"];
            ViewBag.Message = TempData["Message"];
            ViewBag.UpdateAccount = TempData["UpdateAccount"] != null ? TempData["UpdateAccount"] : new EditPortalAccount();

            if (TempData["Account"] != null) 
            {
                return View(TempData["Account"] as CreatePortalAccount);
            }
            return View();
        }

        public ActionResult EditAccount(EditPortalAccount model, HttpPostedFileBase AvatarUrl)
        {
            if (model != null)
            {
                InfoCard infoCard = infoCardRepository.GetByEmail(model.Email);

                if (infoCard != null)
                {
                    infoCard.Email = model.Email;
                    infoCard.Post = model.Post;
                    infoCard.Name = model.Name;
                    infoCard.Surname = model.Surname;
                    infoCard.Patronymic = model.Patronymic;
                    infoCard.Phone = model.Phone;
                    infoCard.DivisionId = model.DivisionId;

                    if (AvatarUrl != null && AvatarUrl.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(AvatarUrl.FileName);

                        var savingPath = Path.Combine(HttpContext.Server.MapPath(avatarDirectory), fileName);
                        AvatarUrl.SaveAs(savingPath);
                        infoCard.AvatarUrl = VirtualPathUtility.ToAbsolute(Path.Combine(avatarDirectory, fileName));
                    }


                    try
                    {
                        infoCardRepository.Update(infoCard);
                        infoCardRepository.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        TempData["IsSuccess"] = false;
                        TempData["Message"] = "Сохранение аккаунта закончилось неудачей. Попробуйте повторить попытку.";
                        TempData["UpdateAccount"] = model;
                        return RedirectToAction("Index");
                    }

                    TempData["IsSuccess"] = true;
                    TempData["Message"] = "Успех.";
                }
                else
                {
                    TempData["IsSuccess"] = false;
                    TempData["Message"] = "Сохранение аккаунта закончилось неудачей. Попробуйте повторить попытку.";
                    TempData["UpdateAccount"] = model;
                }
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Сохранение аккаунта закончилось неудачей. Попробуйте повторить попытку.";
                TempData["UpdateAccount"] = model;
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAccount(int id)
        {
            if (id > 0)
            {
                InfoCard infoCard = infoCardRepository.GetById(id);
                if (infoCard != null)
                {
                    try
                    {
                        infoCardRepository.Delete(infoCard);
                        infoCardRepository.SaveChanges();

                        userRepository.Delete(userRepository.GetById(infoCard.UserId));
                        userRepository.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        TempData["IsSuccess"] = false;
                        TempData["Message"] = "Произошла ошибка при сохранении изменений";
                        return RedirectToAction("Index");
                    }

                    TempData["IsSuccess"] = true;
                    TempData["Message"] = "Пользователь успешно удален!";
                }
                else
                {
                    TempData["IsSuccess"] = false;
                    TempData["Message"] = "Произошла ошибка, данного пользователя не уществует в базе или отправлен неверный идентификатор.";
                }
            }
            else
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Произошла ошибка, неверный идентификатор пользовтеля";
            }

            return RedirectToAction("Index");
        }
    }
}
