using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Ninject;
using Brio;
using Brio.Models;

namespace Brio
{
    /// <summary>
    /// Определяет основную функциональность объекта, удостоверяющего личность.
    /// </summary>
    public class UserIndentity : IIdentity, IUserProvider
    {
        /// <summary>
        /// Текщий пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Аутентификационная служба
        /// </summary>
        [Inject]
        public IAuthentication Auth { get; set; }


        /// <summary>
        /// Текущий аутентифицированный пользователь
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }

        /// <summary>
        /// Получает используемый тип аутентификации.
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        /// <summary>
        /// Получает значение, указывающее, произвел ли пользователь проверку подлинности.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        /// <summary>
        /// Имя текущего пользователя
        /// </summary>
        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Email;
                }
                //иначе аноним
                return "anonym";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="repository"></param>
        public void Init(string email, IUserRepository repository)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User = repository.GetByEmail(email);
            }
        }
    }
}