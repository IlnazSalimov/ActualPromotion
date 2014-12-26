using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class EditPortalAccount
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Должность")]
        public string Post { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Отдел")]
        public int DivisionId { get; set; }

        [Display(Name = "Фото")]
        public string AvatarUrl { get; set; }
    }
}
