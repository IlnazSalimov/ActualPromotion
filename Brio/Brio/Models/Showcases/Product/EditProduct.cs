using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brio.Models
{
    public class EditProduct
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
