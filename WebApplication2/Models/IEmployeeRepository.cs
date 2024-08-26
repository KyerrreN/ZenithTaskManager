namespace WebApplication2.Models
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);
        Employee Update(Employee employeeUpdated);
        Employee Delete(int id);
        Employee GetEmployee(int id);
        List<Employee> GetAllEmployee();
        List<Employee> GetUnassignedEmployees();
        Employee UnassignEmployee(int id);
        Employee AssignEmployeeToDepartment(string empSurname, string depName, string position);
        List<Employee> GetEmployeesAssignedToSpecificDepartment(int employerId);
        Employee GetEmployeeByUserId(string userId);
    }
}
