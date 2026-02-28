using System;
using MiniValidation;
using RangoAgil.API.Models;

namespace RangoAgil.API.EndpointFilters;

public class ValidateAnnotationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var rangoForCreationDTO = context.GetArgument<RangoForCreationDTO>(2);

        if(!MiniValidator.TryValidate(rangoForCreationDTO, out var validationErros))
        {
            return TypedResults.ValidationProblem(validationErros);
        }

        return await next(context);
    }
}
