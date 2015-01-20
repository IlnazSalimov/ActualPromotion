using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class CreateProject
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Tile { get; set; }
        [Required]
        [Display(Name = "Ответственный")]
        public int ResponsibleUserId { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Начало")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Конец")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Название документа")]
        public string DocumentTitle { get; set; }
    }
}
