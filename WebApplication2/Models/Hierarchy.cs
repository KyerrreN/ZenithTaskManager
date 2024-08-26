using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public enum Hierarchy
    {
        [Display(Name = "Сотрудник")]
        Employee,
        [Display(Name = "Начальник отдела")]
        Boss
    }
}
