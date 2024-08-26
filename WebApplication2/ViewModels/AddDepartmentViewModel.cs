using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AddDepartmentViewModel
    {
        [Required(ErrorMessage = "Ошибка: имя отдела не может быть пустым")]
        [MinLength(3, ErrorMessage = "Ошибка: имя отдела не может быть короче 3 символов")]
        [MaxLength(64, ErrorMessage = "Ошибка: имя отдела не может быть длиннее 64 символов")]
        public string Name { get; set; }

        [Required]
        public string DepartmentHead { get; set; }

        public List<Boss>? BossList { get; set; }
    }
}
