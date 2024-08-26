namespace WebApplication2.Models
{
    public interface IDepartmentRepository
    {
        public Department AddDepartment(Department department);
        public List<Department>? GetAllDepartment();
        public Department DeleteDepartment(int id);
        public Department GetDepartment(int id);
    }
}
