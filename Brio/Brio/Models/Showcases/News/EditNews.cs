using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class EditNews
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Текст новости")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Фото")]
        public string PhotoPath { get; set; }
    }
}
