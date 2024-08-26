using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class UnassignedEmployeeViewModel
    {
        public List<Employee> UnassignedEmployees { get; set; }
        public List<Boss> UnassignedBosses { get; set; }
    }
}
