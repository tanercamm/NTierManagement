
using Microsoft.EntityFrameworkCore;
using NTierManagement.BLL.Interfaces;
using NTierManagement.BLL.Services;
using NTierManagement.DAL.Abstract;
using NTierManagement.DAL.Concrete;
using NTierManagement.Entity.Context;
using NTierManagement.Entity.Models;

namespace NTierManagement.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // SQL CONNECT�ON
            builder.Services.AddDbContext<ManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementDB") + ";TrustServerCertificate=True"));

            // Repository ve Servisler i�in Dependency Injection (Ba��ml�l�k tan�mlamas�)
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IBaseRepository<Company>, BaseRepository<Company>>();
            builder.Services.AddScoped<IBaseRepository<Department>, BaseRepository<Department>>();
            builder.Services.AddScoped<IBaseRepository<Person>, BaseRepository<Person>>();

            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
