using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brio.Models
{
    public class EditArticle
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }
        [Required]
        public int PageId { get; set; }
        [Required]
        [Display(Name = "Текст")]
        [AllowHtml]
        public string Text { get; set; }
    }
}