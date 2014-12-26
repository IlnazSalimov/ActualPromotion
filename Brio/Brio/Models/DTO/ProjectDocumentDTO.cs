using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brio
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ResponsibleUserId { get; set; }
        public string Tile { get; set; }
        public string ResponsibleUserFullName
        {
            get
            {
                IInfoCardRepository infoCardRepository = (IInfoCardRepository)DependencyResolver.Current.GetService(typeof(IInfoCardRepository));
                InfoCard ic = infoCardRepository.GetUserInfoCard(this.ResponsibleUserId);
                return ic != null ? ic.FullName : "..пользователь удален..";
            }
        }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<ProjectDocumentDTO> Documents { get; set; }
    }
}
