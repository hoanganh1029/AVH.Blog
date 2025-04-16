using AVBlog.Application.Extensions;
using AVBlog.Application.Mappers;
using AVBlog.Application.Queries.Base;
using AVBlog.Domain.Entities.Users;
using AVBlog.Infrastructure.Data;
using AVBlog.WebApp.Configurations;
using AVBlog.WebApp.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AVBlog.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<AVBlogQueryContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString(nameof(AVBlogQueryContext) ?? throw new InvalidOperationException($"Connection string {nameof(AVBlogQueryContext)} not found."))));

            services.AddDbContext<AVBlogCommandContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString(nameof(AVBlogCommandContext) ?? throw new InvalidOperationException($"Connection string {nameof(AVBlogCommandContext)} not found."))));

            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AVBlogCommandContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
            .AddInMemoryApiResources(IdentityConfig.ApiResources)
            .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
            .AddInMemoryClients(IdentityConfig.Clients)
            .AddAspNetIdentity<AppUser>();

            services.AddAutoMapper(typeof(AVBlogProfile));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IQueryHandlerBase<,>)));

            services.AddDependencyInjectionAutomatically();

            services.ConfigureApplicationCookie(options => options.ExpireTimeSpan = TimeSpan.FromHours(2));

            services.AddAuthentication();

            //Global filter
            //services.AddControllersWithViews(options => options.Filters.Add(typeof(LogActionAttribute)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Custom middleware
            app.UseErrorHandlingMiddleware();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "AdminArea",
                pattern: "{area:exists}/{controller=VideoAdmin}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Video}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.SeedData().Wait();

            app.Run();
        }
    }
}
