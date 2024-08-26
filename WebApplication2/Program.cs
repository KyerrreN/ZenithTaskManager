using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            });

            builder.Services.AddMvc().AddRazorRuntimeCompilation();

            builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            builder.Services.AddScoped<IBossRepository, SQLBossRepository>();
            builder.Services.AddScoped<IDepartmentRepository, SQLDepartmentRepository>();
            builder.Services.AddScoped<ITaskRepository, SQLTaskRepository>();

            // Building an application
            var app = builder.Build();

            // Pipeline, which consists of middleware components
            app.Use(async (context, func) =>
            {
                try
                {
                    await func();
                }
                catch (Exception e)
                {
                    context.Response.Clear();

                    if (e is BadHttpRequestException badHttpRequestException)
                    {
                        context.Response.StatusCode = badHttpRequestException.StatusCode;
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                    }
                }
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Tasks}/{id?}");

            app.Run();
        }
    }
}
