using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee SignedInEmployee { get; set; }
        public List<Tasks>? Tasks { get; set; }
    }
}
