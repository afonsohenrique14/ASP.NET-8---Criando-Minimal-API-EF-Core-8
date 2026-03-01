using System;
using Microsoft.OpenApi;
using RangoAgil.API.Metadatas;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RangoAgil.API.OperationFilters;

public sealed class DeprecatedInSwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
       
        var hasMetadata =
            context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<DeprecatedInSwaggerMetadata>()
                .Any();

        if (hasMetadata)
        {
            operation.Deprecated = true;
        }

    }
}
