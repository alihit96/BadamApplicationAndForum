using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using BadamApplicationAndForum.Helpers;
using BadamApplicationAndForum.Helpers.Permission;
using BadamApplicationAndForum.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pushe.co;
using System;
using System.Text;

namespace BadamApplicationAndForum
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

            var mvcBuilder = services.AddControllersWithViews();

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif
            services.AddDbContext<ApplicationDatabaseContext>(p=>p.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<PanelUser, IdentityRole>(p =>
            {
                p.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
                .AddEntityFrameworkStores<ApplicationDatabaseContext>()
                .AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityError>();

            services.AddAuthentication()
                .AddCookie(cfg=>cfg.SlidingExpiration= true)
                .AddJwtBearer(p =>
            {
                p.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["JwtConfig:issuer"],
                    ValidAudience = Configuration["JwtConfig:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                cfg.LoginPath = "/Admin/User/Login";
                cfg.AccessDeniedPath = "/Admin/User/Login";
            });

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddScoped<IForum, ForumService>();
            services.AddScoped<IPost, PostService>();
            services.AddScoped<IReply, ReplyService>();
            services.AddScoped<IProfessor, ProfessorService>();
            services.AddScoped<ISlider, SliderService>();
            services.AddScoped<INews, NewsService>();
            services.AddScoped<IApplicationUser, ApplicationUserService>();
            services.AddScoped<ISaveLog, SaveLogService>();
            services.AddScoped<IAlarm, AlarmService>();
            services.AddScoped<IDirectMessage, DirectMessageService>();
            services.AddScoped<IStaff, StaffService>();
            services.AddScoped<IRegulation, RegulationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddPushe(options =>
            {
                options.AccessToken = "83b0a84a8b10afdb34b6129cb738e6852323ff37";
            });

            services.AddControllersWithViews()
              .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Admin}/{controller=User}/{action=Index}/{id?}");

                
            });

        }
    }
}
