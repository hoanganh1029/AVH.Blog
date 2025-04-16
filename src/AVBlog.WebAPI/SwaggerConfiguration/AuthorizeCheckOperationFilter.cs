using AVBlog.Infrastructure.Constants;
using AVBlog.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AVBlog.WebAPI.SwaggerConfiguration
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
              (context.MethodInfo.DeclaringType != null &&
              context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any())
              || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security =
                [
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = SwaggerConstant.SecurityDefinitionName
                                },
                                Type = SecuritySchemeType.OAuth2,
                                Description = SwaggerConstant.SecurityDefinitionDescription,
                                Name = "Authorization",
                                In = ParameterLocation.Header,
                            }
                        ] = [APIScope.Read, APIScope.Write]
                    }
                ];

            }
        }
    }
}
