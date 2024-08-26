namespace WebApplication2.Models
{
    public interface ITaskRepository
    {
        public Tasks GetTask(int id);
        public Tasks UpdateTask(Tasks task);
        public Tasks DeleteTask(int id);
        public Tasks AddTask(Tasks task);
        public List<Tasks> GetTasksAssignToEmployee(int employeeId);
        public List<Tasks> GetTasksAssignedToDepartment(List<Employee> employees);
        public bool IsHaveTasks(int employeeId);
        public Tasks UpdateTaskStatusIfExpired(int id);
        public List<Tasks> GetTasksAssignedFromBoss(int bossId);
        public List<Tasks> GetTasksAssignedToEmployeeAndNotDone(int employeeId);
    }
}
