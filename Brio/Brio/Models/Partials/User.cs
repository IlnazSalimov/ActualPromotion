using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Brio;
using Ninject;
using System.Web.Mvc;

namespace Brio.Models
{
    public partial class User : IEntity
    {
        /// <summary>
        /// Хранилище данных о информационных картах
        /// </summary>
        private IInfoCardRepository infoCardRepository { get; set; }

        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                var hasRole = this.Role.RoleName.Equals(role);
                if (hasRole)
                {
                    return true;
                }
            }
            return false;
        }

        public int ID
        {
            get { return this.Id; }
        }

        public string FullName
        {
            get 
            {
                infoCardRepository = (IInfoCardRepository)DependencyResolver.Current.GetService(typeof(IInfoCardRepository));
                InfoCard infoCard = infoCardRepository.GetUserInfoCard(this.ID);
                if (infoCard != null){
                    return infoCard.FullName;
                }
                else
                {
                    return "anonym";
                }
            }
        }
    }
}