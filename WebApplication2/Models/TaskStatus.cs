using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public enum TaskStatus
    {
        [Display(Description = "В процессе выполнения")]
        InProgress,
        [Display(Description = "Завершена")]
        Done,
        [Display(Description = "Просрочена")]
        Expired
    }
}
