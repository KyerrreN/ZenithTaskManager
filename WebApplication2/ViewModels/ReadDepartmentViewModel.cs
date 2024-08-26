using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class ReadDepartmentViewModel
    {
        public Department SingleDepartment { get; set; }
        public Boss SingleBoss { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
