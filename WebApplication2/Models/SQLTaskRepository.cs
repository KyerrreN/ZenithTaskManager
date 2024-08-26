using System.Runtime.CompilerServices;

namespace WebApplication2.Models
{
    public class SQLTaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;
        public SQLTaskRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Tasks AddTask(Tasks task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();

            return task;
        }

        public Tasks DeleteTask(int id)
        {
            Tasks task = context.Tasks.Find(id);

            if (task is not null)
            {
                context.Tasks.Remove(task);
                context.SaveChanges();
            }

            return task;
        }

        public Tasks GetTask(int id)
        {
            return context.Tasks.Find(id);
        }

        public List<Tasks> GetTasksAssignedFromBoss(int bossId)
        {
            return context.Tasks.Where(taskToFind => taskToFind.BossId == bossId && taskToFind.Status == TaskStatus.Done).ToList();
        }

        public List<Tasks> GetTasksAssignedToDepartment(List<Employee> employees)
        {
            List<Tasks> tasks = new List<Tasks>();

            foreach (var emp in employees)
            {
                List<Tasks> assignedTasks = GetTasksAssignToEmployee(emp.Id);
                tasks.AddRange(assignedTasks);
            }

            return tasks;
        }

        public List<Tasks> GetTasksAssignedToEmployeeAndNotDone(int employeeId)
        {
            return context.Tasks.Where(task => task.EmployeeId == employeeId &&
                                               task.Status != TaskStatus.Done)
                .ToList();
        }

        public List<Tasks> GetTasksAssignToEmployee(int employeeId)
        {
            return context.Tasks.Where(task => task.EmployeeId == employeeId).ToList();
        }

        public bool IsHaveTasks(int employeeId)
        {
            if (context.Tasks.FirstOrDefault(task => task.EmployeeId == employeeId) is not null)
            {
                return true;
            }

            return false;
        }

        public Tasks UpdateTask(Tasks task)
        {
            var taskToUpdate = context.Tasks.Attach(task);
            taskToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return task;
        }

        public Tasks UpdateTaskStatusIfExpired(int id)
        {
            Tasks task = GetTask(id);

            if (task is not null && task.Deadline < DateTime.Now && task.Status != TaskStatus.Done)
            {
                task.Status = TaskStatus.Expired;

                UpdateTask(task);
            }

            return task;
        }
    }
}
