using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class AddCompanyWork
    {
        [Required]
        [Display(Name = "Обложка работы")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }
    }
}
