using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using LabFinal;
using System;
using LabFinal.Models;

namespace LabFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Console.WriteLine("Startup constructor called.");
        }
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ConfigureServices called.");
            services.AddControllersWithViews();

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the desired expiration time
                options.LoginPath = "/Identity/Login";
                options.LogoutPath = "/Identity/Logout";
                options.AccessDeniedPath = "/Identity/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "LabFinal";
                options.LoginPath = "/Identity/Login"; // Set the login path
                options.AccessDeniedPath = "/Identity/AccessDenied"; // Set the access denied path
            });


            services.AddAuthorization(options =>
            {
                // Your authorization policies go here
            });
        }


        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            Console.WriteLine("Configure called.");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // UseAuthentication should come before UseAuthorization
            app.UseAuthentication();
            app.UseAuthorization();

            ConfigureEndpoints(app);

            // Ensure the database is created and migrated
            dbContext.Database.Migrate();
        }


        private void ConfigureEndpoints(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Other routes
                endpoints.MapControllerRoute(
                    name: "selectUser",
                    pattern: "select-user",
                    defaults: new { controller = "Home", action = "SelectUser" });

                endpoints.MapControllerRoute(
                    name: "petDetail",
                    pattern: "pets/{id:int}",
                    defaults: new { controller = "Home", action = "Detail" });

                endpoints.MapControllerRoute(
                    name: "addToCart",
                    pattern: "add-to-cart/{id:int}",
                    defaults: new { controller = "Home", action = "AddToCart" });

                endpoints.MapControllerRoute(
                    name: "removeFromCart",
                    pattern: "remove-from-cart/{id:int}",
                    defaults: new { controller = "Home", action = "RemoveFromCart" });

                endpoints.MapControllerRoute(
                    name: "checkout",
                    pattern: "checkout",
                    defaults: new { controller = "Home", action = "Checkout" });

                endpoints.MapControllerRoute(
                    name: "payment",
                    pattern: "payment",
                    defaults: new { controller = "Home", action = "Payment" });

                endpoints.MapControllerRoute(
                    name: "confirmPayment",
                    pattern: "confirm-payment",
                    defaults: new { controller = "Home", action = "ConfirmPayment" });


                endpoints.MapControllerRoute(
                    name: "identity",
                    pattern: "Identity/{controller=Home}/{action=Index}/{id?}")
                    .RequireAuthorization(); 

                endpoints.MapRazorPages();

            });
        }

    }

}