using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "boss")]
    public class BossController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBossRepository bossRep;
        private readonly IEmployeeRepository empRep;
        private readonly IDepartmentRepository depRep;
        private readonly ITaskRepository taskRep;

        public BossController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IBossRepository bossRep,
                              IEmployeeRepository empRep,
                              IDepartmentRepository depRep,
                              ITaskRepository taskRep)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.bossRep = bossRep;
            this.empRep = empRep;
            this.depRep = depRep;
            this.taskRep = taskRep;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            BossManageViewModel model = new BossManageViewModel();
            IdentityUser signedInUser = await userManager.GetUserAsync(User);

            model.SignedInBoss = bossRep.GetSignedInBoss(signedInUser.Id);

            if (model.SignedInBoss.DepartmentId is not null)
            {
                model.Department = depRep.GetDepartment((int)model.SignedInBoss.DepartmentId);
                model.Employees = empRep.GetEmployeesAssignedToSpecificDepartment(model.SignedInBoss.Id);
                model.Tasks = taskRep.GetTasksAssignedToDepartment(model.Employees);
                
                if (model.Employees is not null && model.Employees.Count > 0)
                {
                    // Согласно конвенции, вся эта валидация должна быть
                    // вынесена в отдельный класс модели, но у меня
                    // нет на это времени + оно работает
                    for (int i = 0; i < model.Employees.Count; i++)
                    {
                        if (taskRep.IsHaveTasks(model.Employees[i].Id))
                        {
                            List<Models.Tasks> assignedTasks = taskRep.GetTasksAssignToEmployee(model.Employees[i].Id);

                            foreach (var task in assignedTasks)
                            {
                                taskRep.UpdateTaskStatusIfExpired(task.Id);
                            }

                            model.TasksToEmployee.Add(model.Employees[i], assignedTasks);
                        }
                    }
                }
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult TryAssignTask(int employeeId)
        {
            Employee empToAssignTask = empRep.GetEmployee(employeeId);

            if (empToAssignTask is not null)
            {
                return RedirectToAction("assigntask", new { id = employeeId });
            }

            throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult AssignTask(int id)
        {
            AssignTaskViewModel model = new AssignTaskViewModel();

            Employee empToAssignTask = empRep.GetEmployee(id);
            model.Emp = empToAssignTask;

            return View(model);
        }
        [HttpPost]
        public IActionResult AssignTask(int id, AssignTaskViewModel model)
        {
            Employee empToAssignTask = empRep.GetEmployee(id);
            model.Emp = empToAssignTask;

            taskRep.AddTask(new Models.Tasks
            {
                Title = model.Title,
                Description = model.Description,
                AssignDate = DateTime.Now,
                Deadline = model.Deadline,
                Status = Models.TaskStatus.InProgress,
                EmployeeId = empToAssignTask.Id,
                BossId = (int)empToAssignTask.BossId
            });

            return View(model);
        }
        [HttpPost]
        public IActionResult MarkTaskAsDone(int id)
        {
            Tasks task = taskRep.GetTask(id);

            if (task is not null)
            {
                task.Status = Models.TaskStatus.Done;
                task.CompletionDate = DateTime.Now;

                taskRep.UpdateTask(task);
            }

            return RedirectToAction("manage");
        }
        [HttpGet]
        public async Task<IActionResult> History()
        {
            BossHistoryViewModel model = new BossHistoryViewModel();
            IdentityUser signedInUser = await userManager.GetUserAsync(User);
            Boss currentBoss = bossRep.GetSignedInBoss(signedInUser.Id);

            model.HistoryOfTasks = taskRep.GetTasksAssignedFromBoss(currentBoss.Id);

            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteTaskFromHistory(int id)
        {
            taskRep.DeleteTask(id);

            return RedirectToAction("history");
        }
    }
}
