using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AirDrop.EndPoint.CustomDocumentFilters;

public class ExcludeAreaControllersDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var areasToExclude = new[] { "Admin" };
        var pathsToRemove = swaggerDoc.Paths
            .Where(pathItem =>
            {
                var matchingApiDescription = context.ApiDescriptions.FirstOrDefault(api =>
                    api.RelativePath != null &&
                    pathItem.Key.Trim('/').StartsWith(api.RelativePath.Trim('/'), System.StringComparison.OrdinalIgnoreCase) &&
                    areasToExclude.Any(area => api.ActionDescriptor.RouteValues["area"]?.Equals(area, System.StringComparison.OrdinalIgnoreCase) == true));

                return matchingApiDescription != null;
            })
            .ToList();

        foreach (var path in pathsToRemove)
        {
            swaggerDoc.Paths.Remove(path.Key);
        }
    }
}