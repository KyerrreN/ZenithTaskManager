using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // Dependency Injected Services
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmployeeRepository empRep;
        private readonly IBossRepository bossRep;
        private readonly IDepartmentRepository depRep;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnv;

        // Constructor to inject services
        public AdminController(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager,
                               IEmployeeRepository empRep,
                               IBossRepository bossRep,
                               IDepartmentRepository depRep,
                               RoleManager<IdentityRole> roleManager,
                               Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnv)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.empRep = empRep;
            this.bossRep = bossRep;
            this.depRep = depRep;
            this.roleManager = roleManager;
        }

        // Action methods
        [HttpGet]
        public ViewResult AddEmployee()
        {
            ModelState.Clear();

            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsLoginTaken(string login)
        {
            var user = await userManager.FindByNameAsync(login);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json("Ошибка: данный логин уже используется");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Login };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.Hierarchy == Hierarchy.Employee)
                    {
                        await userManager.AddToRoleAsync(user, "employee");

                        Employee empToAdd = new Employee(
                        model.Name,
                        model.Surname,
                        model.Patronymic,
                        null,
                        null,
                        null,
                        user.Id);

                        empRep.Add(empToAdd);
                    }
                    
                    if (model.Hierarchy == Hierarchy.Boss)
                    {
                        await userManager.AddToRoleAsync(user, "boss");

                        Boss bossToAdd = new Boss()
                        {
                            Name = model.Name,
                            Surname = model.Surname,
                            Patronymic = model.Patronymic,
                            UserId = user.Id
                        };

                        bossRep.AddBoss(bossToAdd);
                    }
                    

                    return RedirectToAction("AddEmployee", "admin");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AssignEmployee()
        {
            AssignEmployeeViewModel model = new AssignEmployeeViewModel();
            model.UnassignedEmployees = empRep.GetUnassignedEmployees();
            model.Departments = depRep.GetAllDepartment();

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignEmployee(AssignEmployeeViewModel model)
        {
            model.UnassignedEmployees = empRep.GetUnassignedEmployees();
            model.Departments = depRep.GetAllDepartment();

            if (ModelState.IsValid)
            {
                empRep.AssignEmployeeToDepartment(model.Surname, model.DepartmentName, model.Position);

                return RedirectToAction("addemployee");
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult AddDepartment()
        {
            AddDepartmentViewModel model = new AddDepartmentViewModel()
            {
                BossList = bossRep.GetUnassignedBosses()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddDepartment(AddDepartmentViewModel model)
        {
            model.BossList = bossRep.GetUnassignedBosses();

            if (ModelState.IsValid)
            {
                depRep.AddDepartment(
                    new Department()
                    {
                        Name = model.Name
                    });

                string[] credentials = model.DepartmentHead.Split(' ');
                
                string name = credentials[1];
                string surname = credentials[0];
                string? patronymic = null;

                if (credentials.Length > 2)
                {
                    patronymic = credentials[2];
                }

                Boss bossToAssign = bossRep.GetBoss(name, surname, patronymic);

                bossRep.AssignBossToDepartment(bossToAssign, model.Name);

                return RedirectToAction("addemployee");
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult ReadDepartment()
        {
            List<ReadDepartmentViewModel> model = new List<ReadDepartmentViewModel>();
            List<Department> deps = depRep.GetAllDepartment();

            if (deps is not null && deps.Count > 0)
            {
                foreach (var dep in deps)
                {
                    Boss bossViewModel = bossRep.GetBossByDepartment(dep.Id);

                    if (bossViewModel is not null)
                    {
                        List<Employee> employeesList = empRep.GetEmployeesAssignedToSpecificDepartment(bossViewModel.Id);

                        model.Add(new ReadDepartmentViewModel()
                        {
                            SingleDepartment = dep,
                            SingleBoss = bossViewModel,
                            Employees = employeesList
                        });
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Employee empToDelete = empRep.GetEmployee(id);
            IdentityUser user = await userManager.FindByIdAsync(empToDelete.UserId);

            await userManager.DeleteAsync(user);
            empRep.Delete(id);

            return RedirectToAction("ReadDepartment");
        }

        [HttpPost]
        public IActionResult UnassignEmployee(int id)
        {
            empRep.UnassignEmployee(id);

            return RedirectToAction("ReadDepartment");
        }

        [HttpGet]
        public IActionResult ReadUnassignedEmployees()
        {
            UnassignedEmployeeViewModel model = new()
            {
                UnassignedBosses = bossRep.GetUnassignedBosses(),
                UnassignedEmployees = empRep.GetUnassignedEmployees()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Boss bossInThisDepartment = bossRep.GetBossByDepartment(id);

            // Открепляем работников от этого босса
            List<Employee> empsInThisDepartment = empRep.GetEmployeesAssignedToSpecificDepartment(bossInThisDepartment.Id);

            if (empsInThisDepartment is not null && empsInThisDepartment.Count > 0)
            {
                foreach (var emp in empsInThisDepartment)
                {
                    empRep.UnassignEmployee(emp.Id);
                }
            }

            // Удаляем босса, связанного с этим отделом
            IdentityUser bossToDelete = await userManager.FindByIdAsync(bossInThisDepartment.UserId);
            await userManager.DeleteAsync(bossToDelete);
            bossRep.DeleteBoss(bossInThisDepartment.Id);

            // Удаляем отдел
            depRep.DeleteDepartment(id);

            return RedirectToAction("ReadDepartment");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBoss(int id)
        {
            Boss bossToDelete = bossRep.GetBoss(id);
            IdentityUser user = await userManager.FindByIdAsync(bossToDelete.UserId);

            await userManager.DeleteAsync(user);
            bossRep.DeleteBoss(id);

            return RedirectToAction("readunassignedemployees");
        }
    }
}
