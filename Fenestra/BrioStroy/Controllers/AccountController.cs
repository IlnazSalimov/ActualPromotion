
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioStroy
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
        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IBrioContext brioContext)
        {
            this._brioContext = brioContext;
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
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
        /// Метод отвечающий за бизнес логику на странице выхода из профиля.</summary>
        /// <returns>Экземпляр ViewResult, который выполняет визуализацию представления.</returns>
        public ActionResult Logout()
        {
            _brioContext.Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
