using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Brio
{
    /// <summary>
    /// Перопределяет основные функциональные возможности аутентифицированного участника
    /// </summary>
    public class UserProvider : IPrincipal
    {
        private UserIndentity userIdentity { get; set; }

        /// <summary>
        /// Текущий участник
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return userIdentity;
            }
        }

        /// <summary>
        /// Определяет, относится ли текущий участник к указанной роли
        /// </summary>
        /// <param name="role">Имя роли, для которой требуется проверить членство.</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (userIdentity.User == null)
            {
                return false;
            }
            else
            {
                return userIdentity.User.InRoles(role);
            }
            
        }

        /// <summary>
        /// Инициализирует экземпляр UserProvider.
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="repository">Предоставляет доступ к хранилищу данных о комментариях к пользователям</param>
        public UserProvider(string name, IUserRepository repository)
        {
            userIdentity = new UserIndentity();
            userIdentity.Init(name, repository);
        }

        /// <summary>
        /// Возвращает строку, представляющую аутентифицированного участника
        /// </summary>
        /// <returns>Строка, представляющая аутентифицированного участника</returns>
        public override string ToString()
        {
            return userIdentity.Name;
        }
    }
}