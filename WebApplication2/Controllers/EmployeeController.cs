using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository empRep;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITaskRepository taskRep;

        public EmployeeController(IEmployeeRepository empRep,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  ITaskRepository taskRep)
        {
            this.empRep = empRep;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.taskRep = taskRep;
        }

        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            EmployeeViewModel model = new();
            IdentityUser signedInUser = await userManager.GetUserAsync(User);

            model.SignedInEmployee = empRep.GetEmployeeByUserId(signedInUser.Id);

            if (model.SignedInEmployee is not null)
            {
                model.Tasks = taskRep.GetTasksAssignedToEmployeeAndNotDone(model.SignedInEmployee.Id);

                if (model.Tasks is not null && model.Tasks.Count > 0)
                {
                    foreach (var task in model.Tasks)
                    {
                        taskRep.UpdateTaskStatusIfExpired(task.Id);
                    }
                }
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult MarkTaskAsDone(int id)
        {
            Tasks taskToMark = taskRep.GetTask(id);

            if (taskToMark is not null)
            {
                taskToMark.Status = Models.TaskStatus.Done;
                taskToMark.CompletionDate = DateTime.Now;

                taskRep.UpdateTask(taskToMark);
            }

            return RedirectToAction("Tasks");
        }
    }
}
