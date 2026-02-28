
using RangoAgil.API.EndpointFilters;
using RangoAgil.API.EndpointHandlers;
namespace RangoAgil.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
    {
        var rangosEndpoints = app.MapGroup("/rangos");
        var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
        var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
            .AddEndpointFilter(new RangosIsLockedFilter(8))
            .AddEndpointFilter(new RangosIsLockedFilter(10))
            .AddEndpointFilter<LogNotFoundResponseFilter>();


        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

        rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync).WithName("GetRangos");

        rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync)
        .AddEndpointFilter<ValidateAnnotationFilter>();

        rangosComIDEndpointsAndLockFilterEndpoints.MapPut("", RangosHandlers.RangoPutAsync);
        rangosComIDEndpointsAndLockFilterEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
            
    }

    public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder app)
    {
        var ingredientesEndpoints = app.MapGroup("rangos/{rangoId:int}/ingredientes");

        ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsyn);
        
    }
}
