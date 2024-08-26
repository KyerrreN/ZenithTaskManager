
namespace WebApplication2.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();

            return employee;
        }

        public Employee Delete(int id)
        {
            Employee emp = context.Employees.Find(id);

            if (emp is not null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }

            return emp;
        }

        public List<Employee> GetAllEmployee()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee Update(Employee employeeUpdated)
        {
            var emp = context.Employees.Attach(employeeUpdated);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return employeeUpdated;
        }

        public List<Employee> GetUnassignedEmployees()
        {
            return context.Employees.Where(e => e.Department == null).ToList();
        }

        public Employee AssignEmployeeToDepartment(string empSurname, string depName, string position)
        {
            Employee emp = context.Employees.FirstOrDefault(emp => emp.Surname == empSurname);
            Department dep = context.Departments.FirstOrDefault(dep => dep.Name == depName);
            Boss boss = context.Bosses.FirstOrDefault(boss => boss.DepartmentId == dep.Id);

            emp.Department = dep.Name;
            emp.Position = position;
            emp.Employer = boss.Surname + " " + boss.Name + " "+ boss.Patronymic;
            emp.BossId = boss.Id;

            Update(emp);

            return emp;
        }

        public List<Employee> GetEmployeesAssignedToSpecificDepartment(int employerId)
        {
            return context.Employees.Where(emp => emp.BossId == employerId).ToList();
        }

        public Employee UnassignEmployee(int id)
        {
            Employee empToUnassign = GetEmployee(id);

            empToUnassign.Employer = null;
            empToUnassign.Department = null;
            empToUnassign.BossId = null;
            empToUnassign.Position = null;

            Update(empToUnassign);

            return empToUnassign;
        }

        public Employee GetEmployeeByUserId(string userId)
        {
            return context.Employees.FirstOrDefault(emp => emp.UserId == userId);
        }
    }
}
