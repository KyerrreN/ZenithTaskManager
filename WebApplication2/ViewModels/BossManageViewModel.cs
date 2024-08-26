using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class BossManageViewModel
    {
        public Boss? SignedInBoss { get; set; }
        public Department? Department { get; set; }
        public List<Employee>? Employees { get; set; }
        public List<Models.Tasks>? Tasks { get; set; }
        public Dictionary<Employee, List<Models.Tasks?>>? TasksToEmployee { get; set; } = [];  
    }
}
