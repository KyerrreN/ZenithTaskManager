using System.ComponentModel.DataAnnotations;
using WebApplication2.CustomValidationAttributes;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AssignTaskViewModel
    {
        // Passed Employee object do display it
        public Employee Emp { get; set; }

        // POST Request Validation
        [Required(ErrorMessage = "Ошибка: название задачи не может быть пустым")]
        [MaxLength(100, ErrorMessage = "Ошибка: название задачи не может быть длиннее 100 символов. Выносите детали в описание задачи.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ошибка: описание задачи не может быть пустым")]
        [MaxLength(2000, ErrorMessage = "Ошибка: описание задачи слишком длинное")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ошибка: вы должны указать дедлайн для задачи")]
        [DataType(DataType.DateTime)]
        [TimeValidation(ErrorMessage = "Ошибка: дедлайн для задачи должен быть позже даты выдачи задачи на 1 час")]
        public DateTime Deadline { get; set; }
    }
}
