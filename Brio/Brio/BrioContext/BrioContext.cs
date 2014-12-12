using Brio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using Brio.Models;

namespace Brio
{
    /// <summary>
    /// Инкапсулирует сведения и предоставляет доступ к системным данным и службам приложения.
    /// </summary>
    public class BrioContext : IBrioContext
    {
        /// <summary>
        /// Название аутентификационного cookie, в котором будет храниться данные о текущем аутентифицированном пользователе
        /// </summary>
        private const string COOKIENAME = "__AUTH_COOKIE";

        /// <summary>
        /// Аутентификационная служба
        /// </summary>
        public IAuthentication Auth { get; set; }

        /// <summary>
        /// Предоставляет доступ к хранилищу пользователей.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>  
        /// Инициализирует новый экземпляр InvestContext с внедрением зависемостей к хранилищу пользователей и с аутентификационной службой.</summary>  
        /// <param name="userRepository">Экземпляр класса UserRepository, предоставляющий доступ к хранилищу пользователей.</param>
        /// <param name="authentication">аутентификационная служба</param>
        public BrioContext(IUserRepository userRepository, IAuthentication authentication)
        {
            this._userRepository = userRepository;
            this.Auth = DependencyResolver.Current.GetService<IAuthentication>();
        }

        /// <summary>
        /// Возвращает текущего аутентифицированного пользователя
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }

    }
}
