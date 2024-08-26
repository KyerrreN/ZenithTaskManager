using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ошибка: поле с вводом логина пустое")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Ошибка: поле с вводом пароля пустое")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
