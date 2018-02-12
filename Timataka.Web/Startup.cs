using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timataka.Core.Data;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models;
using Timataka.Core.Models.Entities;
using Timataka.Core.Services;
using Timataka.Web.Services;

namespace Timataka.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // (timataka)
            // This is where password rules are modified
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // (timataka)
            // This is where cookies settings are modified
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            // DI for Repositores
            services.AddTransient<ISportsRepository, SportsRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                //CreateSuperAdmin(serviceProvider);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // (timataka)
        // Here Superadmin is created if hef does not already exist
        // There is only one superAdmin in this application
        private static void CreateSuperAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            const string superAdminEmail = "super@admin.com";

            var hasSuperAdminRole = roleManager.RoleExistsAsync("Superadmin");
            hasSuperAdminRole.Wait();

            if (!hasSuperAdminRole.Result)
            {
                Task roleResult = roleManager.CreateAsync(new IdentityRole("Superadmin"));
                roleResult.Wait();
            }

            var superAdmin = userManager.FindByEmailAsync(superAdminEmail);
            superAdmin.Wait();

            if (superAdmin.Result != null) return;

            var newSuperAdmin = new ApplicationUser
            {
                Email = superAdminEmail,
                UserName = superAdminEmail
            };

            var newSuperAdminResult = userManager.CreateAsync(newSuperAdmin, "Super#123");
            newSuperAdminResult.Wait();

            if (!newSuperAdminResult.Result.Succeeded) return;

            var newUserRole = userManager.AddToRoleAsync(newSuperAdmin, "Superadmin");
            newUserRole.Wait();
        }
    }
}
