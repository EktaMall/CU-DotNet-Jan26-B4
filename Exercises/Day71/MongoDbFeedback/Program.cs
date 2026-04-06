//using MongoDbFeedback;
using MongoDbFeedBack.Services;

namespace MongoDbFeedback
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<FeedBackService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ✅ Enable CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            // Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllers(); // ✅ Important for API

            app.MapRazorPages();

            app.Run();
        }
    }
}