namespace WebApplication2.Models
{
    public class SQLDepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext context;
        public SQLDepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Department AddDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();

            return department;
        }

        public List<Department>? GetAllDepartment()
        {
            return context.Departments.ToList();
        }

        public Department DeleteDepartment(int id)
        {
            Department depToDelete = context.Departments.FirstOrDefault(dep => dep.Id == id);

            if (depToDelete != null)
            {
                context.Departments.Remove(depToDelete);
                context.SaveChanges();
            }

            return depToDelete;
        }

        public Department GetDepartment(int id)
        {
            return context.Departments.FirstOrDefault(dep => dep.Id == id);
        }
    }
}
