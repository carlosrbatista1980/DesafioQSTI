using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioQSTI.Data;
using DesafioQSTI.Data.Seed;
using DesafioQSTI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DesafioQSTI
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
            //services.AddControllersWithViews();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // --- <Cookie policy configuration>
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // --- </Cookie policy configuration>

            // --- <Main context>
            //services.AddDbContext<MySqlDbContext>(opt => { opt.UseSqlServer(Configuration.GetConnectionString("MySql")); });
            services.AddDbContext<MySqlDbContext>(opt => { opt.UseMySql(Configuration.GetConnectionString("MySql")); });
            // --- </Main context>

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<MySqlDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Desafio"],
                    ValidIssuer = Configuration["Jwt:Desafio"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"])),
                };
            });


            services.AddScoped<ServicoCliente>();
            services.AddScoped<Seed>();

            // --- <Registration Services>
            //services.AddScoped<ServiceInitialize>();

            // --- </Registration Services>

            // --- <Add singleton for future implementation>

            // --- </Add singleton for future implementation>

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(opt =>
            {
                opt.Cookie.Name = "Cliente.Session";
                opt.IdleTimeout = TimeSpan.FromMinutes(10);
                opt.Cookie.IsEssential = true;
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seed.SeedData();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //app.UseMvc(route =>
            //{
            //    route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); 
            //});
            
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Index}/{id?}");
            });
        }
    }
}
