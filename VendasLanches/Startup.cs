using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using VendasLanches.Areas.Admin.Services;
using VendasLanches.Context;
using VendasLanches.Models;
using VendasLanches.Models.Configurations;
using VendasLanches.Repositories;
using VendasLanches.Repositories.Interfaces;
using VendasLanches.Services;

namespace VendasLanches;

public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
        );

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options => {
            // Password policy
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
            //options.Password.RequiredUniqueChars = 1;
        });

        services.AddTransient<ISnackRepository, SnackRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddScoped<ISeedRolerInitial, SeedRolerInitial>();
        services.AddScoped<SellersReportService>();
        services.Configure<ImagesConfiguration>(Configuration.
            GetSection("ImagesFolderConfiguration"));

        services.AddAuthorization(options => {
            options.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); });
        });
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(ct => Cart.GetCart(ct));
        
        services.AddControllersWithViews();

        services.AddPaging(options => {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });

        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        ISeedRolerInitial seedRoler) {

        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        } else {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        seedRoler.SeedRoles();
        seedRoler.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {

            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );

            endpoints.MapControllerRoute(
                name: "snackCategory",
                pattern: "Snack/{action}/{category?}",
                defaults: new { Controller = "Snack", Action = "List" }
            );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}