using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TIK
{
    public class ReviewContent
    {
        [Required]
        [Display(Name = "Имя автора")]
        public string AuthorName { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Message { get; set; }
        [Display(Name = "Должность автора")]
        public string AuthorPosition { get; set; }
        [Display(Name = "Компания")]
        public string AuthorCompany { get; set; }
        [Display(Name = "Фото")]
        public string AuthorAvatar { get; set; }
    }
}