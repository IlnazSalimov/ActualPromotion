
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BrioPortal.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Предоставляет доступ к хранилищу данных о пользователях
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о ролях пользователей
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Предоставляет доступ к хранилищу данных о информационных картах пользователей
        /// </summary>
        private readonly IInfoCardRepository _infoCardRepository;

        /// <summary>
        /// Экземпляр класса BrioContext, предоставляет доступ к системным данным приложения.
        /// Может быть использован для доступа к текущему авторизованному пользователю
        /// </summary>
        private readonly IBrioContext _brioContext;

        /// <summary>
        /// Инициализирует новый экземпляр AccountController с внедрением зависемостей к хранилищу данных о пользователях и их сообщениях
        /// </summary>
        /// <param name="userRepository">Экземпляр класса UserRepository, предоставляющий доступ к хранилищу данных о пользователях</param>
        /// <param name="roleRepository">Экземпляр класса RoleRepository, предоставляющий доступ к хранилищу данных о ролях пользователей</param>
        /// <param name="investContext">Экземпляр класса BrioContext, предоставляющий доступ к системным данным приложения</param>
        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IBrioContext brioContext, IInfoCardRepository infoCardRepository)
        {
            this._brioContext = brioContext;
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._infoCardRepository = infoCardRepository;
        }

        /// <summary>  
        /// Метод отвечающий за бизнес логику на главной странице аутентификации.
        /// </summary>
        /// <returns>Экземпляр ViewResult, который выполняет визуализацию представления.</returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _brioContext.Auth.Login(model.Email, model.Password, model.RememberMe);

                if (user != null)
                {
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Имя пользователя или пароль является не корректным.");
                }
            }

            return View(model);
        }

        /// <summary>  
        /// Метод отвечающий за бизнес логику на странице регистрации для get-запроса.</summary>
        /// <returns>Экземпляр ViewResult, который выполняет визуализацию представления.</returns>
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>  
        /// Метод отвечающий за бизнес логику на странице регистрации для post-запроса.</summary>
        /// <param name="model">Модель регистрации</param>
        /// <returns>Экземпляр ViewResult, который выполняет визуализацию представления.</returns>
        [HttpPost]
        public ActionResult SignUp(CreatePortalAccount model)
        {
            if (ModelState.IsValid)
            {
                var anyUser = _userRepository.GetAll().Any(p => p.Email.Equals(model.Email));
                if (anyUser)
                {
                    return View();
                }

                Regex rgx = new Regex("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$");
                if (!rgx.IsMatch(model.Email))
                {
                    return View();
                }

                anyUser = _userRepository.GetAll().Any(p => p.Email.Equals(model.Email));
                if (anyUser)
                {
                    return View(model);
                }

                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    return View(model);
                }

                int userId = _userRepository.Insert(new User { Email = model.Email, Password = model.Password, RoleId = model.RoleId });
                _infoCardRepository.Insert(new InfoCard { CompanyId = model.CompanyId, Email = model.Email, Name = model.Name, Surname = model.Surname, Patronymic = model.Patronymic, Phone = model.Phone, UserId = userId });

                _userRepository.SaveChanges();
            }
            return View("Index", "Home");
        }

        /// <summary>  
        /// Метод отвечающий за бизнес логику на странице выхода из профиля.</summary>
        /// <returns>Экземпляр ViewResult, который выполняет визуализацию представления.</returns>
        public ActionResult Logout()
        {
            _brioContext.Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
