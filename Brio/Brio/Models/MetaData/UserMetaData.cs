using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Brio
{
    /// <summary>
    /// Класс для установки мета-данных соответствующей модели
    /// </summary>
    public class UserMetaData
    {
        [Required]
        [Display(Name = "E-mail")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Некорректный E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}