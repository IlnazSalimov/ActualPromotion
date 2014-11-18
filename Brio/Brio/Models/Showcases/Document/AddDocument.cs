using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public class AddDocument
    {
        [Required]
        [Display(Name = "Документ")]
        public string DocumentPath { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string DocumentTitle { get; set; }
    }
}