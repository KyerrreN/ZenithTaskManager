namespace WebApplication2.Models
{
    //public class EmployeeRepository : IEmployeeRepository
    // НЕНУЖНЫЙ КЛАСС, УДАЛИТЬ
    public class EmployeeRepository
    {
        private List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = new List<Employee>();
        }
        public Employee Delete(int id)
        {
            Employee emp = _employees.FirstOrDefault(e => e.Id == id);

            if (emp is not null)
            {
                _employees.Remove(emp);
            }

            return emp;
        }
        public Employee Update(Employee empToUpdate)
        {
            Employee emp = _employees.FirstOrDefault(e => e.Id == empToUpdate.Id);

            if (emp is not null)
            {
                emp.Name = empToUpdate.Name;
                emp.Surname = empToUpdate.Surname;
                emp.Patronymic = empToUpdate.Patronymic;
                emp.Department = empToUpdate.Department;
                emp.Position = empToUpdate.Position;
                emp.Employer = empToUpdate.Employer;
            }

            return emp;
        }
        public Employee Add(Employee emp)
        {
            _employees.Add(emp);

            return emp;
        }
        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }
        public List<Employee> GetAllEmployee()
        {
            return _employees;
        }

        public List<Employee> GetUnassignedEmployees()
        {
            return _employees.Where(e => e.Department == null).ToList();
        }
    }
}
