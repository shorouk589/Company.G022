using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Repository;
using Company.G02.DAL.Data.Contexts;
using Company.G02.PL.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //Allow DI for DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Allow DI for EmployeeRepository

        
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            } /*,  ServiceLifetime.Singleton*/);//Allow DI for CompanyDbContext



            // Life Time
            // builder.Services.AddScoped();    //Create one instance per request / per request(all life time object will be same for one request) /(after that it will be Unreachable)
            // builder.Services.AddTransient(); //Create a new instance every time/per one operation (all life time object will be different)
            // builder.Services.AddSingleton(); //Create one instance per application /per App (all life time object will be same for all request)

            builder.Services.AddScoped<IScopedService , ScopedService>();//Per Request
            builder.Services.AddTransient<ITransientService,TransientService>(); // pre operation
            builder.Services.AddSingleton<ISingletonService,SingletonService>(); // per application


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
