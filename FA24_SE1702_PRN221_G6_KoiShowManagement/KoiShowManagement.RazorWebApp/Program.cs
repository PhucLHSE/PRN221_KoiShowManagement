using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Service;

namespace KoiShowManagement.RazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Add FA24_SE1702_PRN221_G6_KoiShowManagementContext
            //builder.Services.AddScoped<FA24_SE1702_PRN221_G6_KoiShowManagementContext>();
            //Dependency Injection
            builder.Services.AddScoped<AnimalService>();
            builder.Services.AddScoped<AnimalVarietyService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
