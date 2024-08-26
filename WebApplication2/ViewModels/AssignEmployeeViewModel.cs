using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AssignEmployeeViewModel
    {
        public List<Employee>? UnassignedEmployees { get; set; }
        public List<Department>? Departments { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Ошибка: должность не может быть пустой")]
        [MinLength(2, ErrorMessage = "Ошибка: название должности должно иметь минимум 2 символа")]
        [MaxLength(64, ErrorMessage = "Ошибка: название должности не может быть длиннее 64 символов")]
        public string Position { get; set; }
    }
}
