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
        private IInfoCardRepository infoCardRepository = (IInfoCardRepository)DependencyResolver.Current.GetService(typeof(IInfoCardRepository));

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

        public int CompanyId
        {
            get
            {
                InfoCard infoCard = infoCardRepository.GetUserInfoCard(this.ID);
                if (infoCard != null)
                {
                    return infoCard.CompanyId;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int DivisionId
        {
            get
            {
                InfoCard infoCard = infoCardRepository.GetUserInfoCard(this.ID);
                if (infoCard != null)
                {
                    return infoCard.DivisionId.HasValue ? infoCard.DivisionId.Value : 0;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}