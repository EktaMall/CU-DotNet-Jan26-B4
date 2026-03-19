using CarManagementMVCApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarManagementMVCApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options => 
                { options.AccessDeniedPath = "/Home/AccessDenied"; });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedRolesAndUsers(services);
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();


            // Role + User Seeder
            async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] roles = { "Admin", "Customer", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Admin
                var admin = new IdentityUser { UserName = "admin@test.com", Email = "admin@test.com" };
                if (await userManager.FindByEmailAsync(admin.Email) == null)
                {
                    await userManager.CreateAsync(admin, "Admin@123");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                // Customer
                var customer = new IdentityUser { UserName = "customer@test.com", Email = "customer@test.com" };
                if (await userManager.FindByEmailAsync(customer.Email) == null)
                {
                    await userManager.CreateAsync(customer, "Customer@123");
                    await userManager.AddToRoleAsync(customer, "Customer");
                }

                // User
                var user = new IdentityUser { UserName = "user@test.com", Email = "user@test.com" };
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    await userManager.CreateAsync(user, "User@123");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}