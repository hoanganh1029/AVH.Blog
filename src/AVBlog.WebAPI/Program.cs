
using AVBlog.Application.Extensions;
using AVBlog.Application.Mappers;
using AVBlog.Application.Queries.Base;
using AVBlog.Domain.Entities.Users;
using AVBlog.Infrastructure.Constants;
using AVBlog.Infrastructure.Data;
using AVBlog.WebAPI.Constants;
using AVBlog.WebAPI.Converters;
using AVBlog.WebAPI.SwaggerConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AVBlog.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var identityUrl = "https://localhost:7000";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
             {
                 options.Authority = identityUrl;
                 options.TokenValidationParameters.ValidateAudience = false;
             });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiBlogPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", APIScope.Read, APIScope.Write);
                });
            });

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

            //services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AV Blog API",
                    Version = "v1",
                    Description = "Use ASP.NET Core Web API to perform basic CRUD operations"
                });
                options.AddSecurityDefinition(SwaggerConstant.SecurityDefinitionName, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = SwaggerConstant.SecurityDefinitionDescription,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {{APIScope.Read, "Read Blog"}, {APIScope.Write, "Update Blog"}},
                        }
                    },
                });
                options.SchemaFilter<ExampleSchemaFilter>();
                // Set the comments path for the Swagger JSON and UI base on comment of function in controller.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<AVBlogQueryContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString(nameof(AVBlogQueryContext) ?? throw new InvalidOperationException($"Connection string {nameof(AVBlogQueryContext)} not found."))));

            services.AddDbContext<AVBlogCommandContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString(nameof(AVBlogCommandContext) ?? throw new InvalidOperationException($"Connection string {nameof(AVBlogCommandContext)} not found."))));

            services.AddIdentityCore<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AVBlogCommandContext>();

            services.AddAutoMapper(typeof(AVBlogProfile));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(IQueryHandlerBase<,>)));

            services.AddDependencyInjectionAutomatically();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Version 1.0");
                options.OAuthClientId("WebAPIClient");
                options.OAuthAppName("APIBlog");
                options.OAuthScopeSeparator(" ");
                options.OAuthUsePkce();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
