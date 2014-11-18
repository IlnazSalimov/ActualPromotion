using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public class EditDocument
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Документ")]
        public string DocumentPath { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string DocumentTitle { get; set; }
    }
}