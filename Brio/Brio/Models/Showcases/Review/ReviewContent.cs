using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public class ReviewContent
    {
        [Required]
        [Display(Name = "Имя автора")]
        public string AuthorName { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Message { get; set; }
        [Required]
        [Display(Name = "Должность автора")]
        public string AuthorPosition { get; set; }
        [Required]
        [Display(Name = "Компания")]
        public string AuthorCompany { get; set; }
        [Required]
        [Display(Name = "Фото")]
        public string AuthorAvatar { get; set; }
    }
}