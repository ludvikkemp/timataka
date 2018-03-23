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
        public IHostingEnvironment CurrentEnvicoment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment currentEnviroment)
        { 
            Configuration = configuration;
            CurrentEnvicoment = currentEnviroment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(CurrentEnvicoment.IsEnvironment("Testing"))
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            
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

            // DI for Repositories
            services.AddTransient<ISportRepository, SportRepository>();
            services.AddTransient<IDisciplineRepository, DisciplineRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICompetitionRepository, CompetitionRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IHeatRepository, HeatRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IClubRepository, ClubRepository>();

            // DI for Services
            services.AddTransient<ICompetitionService, CompetitionService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ISportService, SportService>();
            services.AddTransient<IDisciplineService, DisciplineService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IHeatService, HeatService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IClubService, ClubService>();

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
                CreateSuperAdmin(serviceProvider);
                CreateAdmin(serviceProvider);
                CreateRoles(serviceProvider);
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

        private static void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var hasSuperadminRole = roleManager.RoleExistsAsync("Superadmin");
            var hasAdminRole = roleManager.RoleExistsAsync("Admin");
            var hasUserRole = roleManager.RoleExistsAsync("User");

            hasSuperadminRole.Wait();
            hasAdminRole.Wait();
            hasUserRole.Wait();

            if (!hasSuperadminRole.Result)
            {
                Task roleResult = roleManager.CreateAsync(new IdentityRole("Superadmin"));
                roleResult.Wait();
            }
            if (!hasAdminRole.Result)
            {
                Task roleResult = roleManager.CreateAsync(new IdentityRole("Admin"));
                roleResult.Wait();
            }
            if (!hasUserRole.Result)
            {
                Task roleResult = roleManager.CreateAsync(new IdentityRole("User"));
                roleResult.Wait();
            }
        }

        // (timataka)
        // Here Superadmin is created if hef does not already exist
        // There is only one superAdmin in this application
        private static void CreateSuperAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            const string adminEmail = "superadmin@superadmin.com";

            var superAdmin = userManager.FindByEmailAsync(adminEmail);
            superAdmin.Wait();

            if (superAdmin.Result != null) return;
            var newSuperadmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Jón",
                LastName = "Jónsson",
                Ssn = "2110942369",
                Gender = "Male",
                Phone = "354-5671024",
                CountryId = 44,
                DateOfBirth = DateTime.Now,
                Deleted = false
            };

            var newSuperAdminResult = userManager.CreateAsync(newSuperadmin, "Super#123");
            newSuperAdminResult.Wait();

            if (!newSuperAdminResult.Result.Succeeded) return;
            var newUserRole = userManager.AddToRoleAsync(newSuperadmin, "Superadmin");
            newUserRole.Wait();
            var newUserRole2 = userManager.AddToRoleAsync(newSuperadmin, "Admin");
            newUserRole2.Wait();
            var newUserRole3 = userManager.AddToRoleAsync(newSuperadmin, "User");
            newUserRole3.Wait();
        }

        private static void CreateAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            const string adminEmail = "admin@admin.com";

            var superAdmin = userManager.FindByEmailAsync(adminEmail);
            superAdmin.Wait();

            if (superAdmin.Result != null) return;
            var newSuperadmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Karl",
                LastName = "Sigurðsson",
                Ssn = "1211912439",
                Gender = "Male",
                Phone = "354-5621326",
                CountryId = 44,
                DateOfBirth = DateTime.Now,
                Deleted = false
            };

            var newSuperAdminResult = userManager.CreateAsync(newSuperadmin, "Admin#123");
            newSuperAdminResult.Wait();

            if (!newSuperAdminResult.Result.Succeeded) return;
            var newUserRole2 = userManager.AddToRoleAsync(newSuperadmin, "Admin");
            newUserRole2.Wait();
            var newUserRole3 = userManager.AddToRoleAsync(newSuperadmin, "User");
            newUserRole3.Wait();
        }
    }
}
