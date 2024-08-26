using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AddEmployeeViewModel
    {
        // ДОДЕЛАТЬ ПОЛНУЮ ВАЛИДАЦИЮ
        [Required(ErrorMessage = "Ошибка: имя не может быть пустым")]
        [StringLength(24, ErrorMessage = "Ошибка: количество букв в имени не должно превышать 24 символа")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ошибка: фамилия не может быть пустой")]
        [StringLength(40, ErrorMessage = "Ошибка: количество букв в фамилии не должно превышать 40 символов")]
        public string Surname { get; set; }

        [StringLength(40, ErrorMessage = "Ошибка: количество букв в отчестве не должно превышать 40 символов")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Ошибка: логин не может быть пустым")]
        [MinLength(8, ErrorMessage = "Ошибка: логин не может быть короче 8 символов")]
        [MaxLength(20, ErrorMessage = "Ошибка: логин не может быть длиннее 32 символов")]
        [Remote(action: "IsLoginTaken", controller: "admin", ErrorMessage = "Ошибка: этот логин уже используется")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Ошибка: пароль не может быть пустым")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Ошибка: пароль не может быть короче 8 символов")]
        [MaxLength(32, ErrorMessage = "Ошибка: пароль не может быть длиннее 32 символов")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Позиция")]
        public Hierarchy Hierarchy { get; set; }
    }
}
