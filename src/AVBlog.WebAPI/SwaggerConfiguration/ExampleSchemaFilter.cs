using AVBlog.Application.ViewModels.Samples;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AVBlog.WebAPI.SwaggerConfiguration
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(VimeoVideoViewModel))
            {
                schema.Example = new OpenApiObject()
                {
                    ["id"] = new OpenApiString("The video id, use 00000000-0000-0000-0000-000000000000 for new video creation"),
                    ["vimeoId"] = new OpenApiInteger(1009233),
                    ["title"] = new OpenApiString("Video title"),
                    ["videoType"] = new OpenApiInteger(0),
                    ["publishedDate"] = new OpenApiString("15-01-2024"),
                    ["description"] = new OpenApiString("Video description"),
                    ["presenter"] = new OpenApiString("Name of presenter")
                };
            }
        }
    }
}
