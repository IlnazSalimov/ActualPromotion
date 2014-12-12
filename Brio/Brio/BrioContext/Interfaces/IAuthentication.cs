using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Brio
{
    /// <summary>
    /// Предоставляет основной интерфейс аутентификационных инструментов
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Инкапсулирует все конкретных HTTP-сведения об индивидуальном запросе HTTP.
        /// </summary>
        HttpContext HttpContext { get; set; }

        /// <summary>
        /// Производит аутентификацую пользователя
        /// </summary>
        User Login(string login, string password, bool isPersistent);

        /// <summary>
        /// Производит аутентификацую пользователя
        /// </summary>
        User Login(string login);

        /// <summary>
        /// Производит очистку  аутентификационных данных текущего ползователя из Cookie
        /// </summary>
        void LogOut();

        /// <summary>
        /// Возвращает текущего аутентифицированного пользователя
        /// </summary>
        IPrincipal CurrentUser { get; }
    }
}
