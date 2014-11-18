using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualPromotion.Models
{
    public class PostBid
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Тел.")]
        public string Phone { get; set; }

        [Display(Name = "Текст сообщения")]
        public string Message { get; set; }

        [Display(Name = "ТЗ")]
        public string TermPath { get; set; }
        public System.DateTime Date { get; set; }
    }
}