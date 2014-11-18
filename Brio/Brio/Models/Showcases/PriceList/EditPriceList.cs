using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public class EditPriceList
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Прайс-лист")]
        public string Path { get; set; }
    }
}