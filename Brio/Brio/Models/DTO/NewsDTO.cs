using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brio.Models
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int AuthorUserId { get; set; }
        public string AuthorUserFullName
        {
            get
            {
                IInfoCardRepository infoCardRepository = (IInfoCardRepository)DependencyResolver.Current.GetService(typeof(IInfoCardRepository));
                InfoCard ic = infoCardRepository.GetUserInfoCard(this.AuthorUserId);
                return ic != null ? ic.FullName : "..пользователь удален..";
            }
        }
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}
