using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Ninject;
using Brio;
using Brio.Models;

namespace Brio
{
    /// <summary>
    /// Предоставляет методы обеспечивающие аутентификационную деятельность пользователя
    /// </summary>
    public class CustomAuthentication : IAuthentication
    {
        /// <summary>
        /// Название аутентификационного cookie, в котором будет храниться данные о текущем аутентифицированном пользователе
        /// </summary>
        private const string cookieName = "__AUTH_COOKIE";

        /// <summary>
        /// Инкапсулирует все конкретных HTTP-сведения об индивидуальном запросе HTTP.
        /// </summary>
        public HttpContext HttpContext { get; set; }

        /// <summary>
        /// Предоставляет доступ к хранилищу пользователей.
        /// </summary>
        [Inject]
        public IUserRepository _userRepository { get; set; }

        #region IAuthentication Members

        /// <summary>
        /// Производит аутентификацую пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <param name="isPersistent">true , если билет будет храниться с постоянным файлом Cookie (сохраняемым между сеансами браузера);
        /// в противном случае — false. Если билет хранится в URL-адресе, это значение игнорируется.</param>
        /// <returns>аутентифицированный пользователь</returns>
        public User Login(string userName, string Password, bool isPersistent)
        {
            User retUser = _userRepository.Login(userName, Password);
            if (retUser != null)
            {
                CreateCookie(userName, isPersistent);
            }
            return retUser;
        }

        public User Login(string email)
        {
            User retUser = _userRepository.GetAll().FirstOrDefault(p => string.Compare(p.Email, email, true) == 0);
            if (retUser != null)
            {
                CreateCookie(email);
            }
            return retUser;
        }

        /// <summary>
        /// Создает аутентификационные данные в Cookie
        /// </summary>
        /// <param name="userName">Имя пользователя, связанное с билетом. </param>
        /// <param name="isPersistent">true , если билет будет храниться с постоянным файлом Cookie (сохраняемым между сеансами браузера);
        /// в противном случае — false. Если билет хранится в URL-адресе, это значение игнорируется.</param>
        private void CreateCookie(string userName, bool isPersistent = true)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.AddDays(2),   
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            HttpContext.Response.Cookies.Set(AuthCookie);
        }

        /// <summary>
        /// Производит очистку  аутентификационных данных текущего ползователя из Cookie
        /// </summary>
        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        private IPrincipal _currentUser;

        /// <summary>
        /// Возвращает текущего аутентифицированного пользователя
        /// </summary>
        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, _userRepository);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }
        #endregion
    }
}