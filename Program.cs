using MyBlog.Data;

namespace MyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseStaticFiles();
            
            app.MapControllerRoute("default", "{controller=Posts}/{action=Index}");
            
            
            app.Run();
        }
    }
}
