using Microsoft.EntityFrameworkCore;
using LoanManagementSystem02.Data;

namespace LoanManagementSystem02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Register DbContext (Dependency Injection)
            builder.Services.AddDbContext<LoanManagementSystem02Context>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("LoanManagementSystem02Context")
                    ?? throw new InvalidOperationException("Connection string not found.")
                ));

            // ✅ Add Controllers
            builder.Services.AddControllers();

            // ✅ Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ✅ Middleware pipeline
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