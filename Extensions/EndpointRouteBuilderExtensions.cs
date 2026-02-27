
using RangoAgil.API.EndpointHandlers;
namespace RangoAgil.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
    {
        var rangosEndpoints = app.MapGroup("/rangos");
        var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");



        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync);

        rangosComIDEndpoints.MapGet("", RangosHandlers.GetRangoByIdAsync).WithName("GetRangos");

        rangosEndpoints.MapPost("", RangosHandlers.RangoPostAsync);

        rangosComIDEndpoints.MapPut("", RangosHandlers.RangoPutAsync);

        rangosComIDEndpoints.MapDelete("", RangosHandlers.RangoDeleteAsync);
    }

    public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder app)
    {
        var ingredientesEndpoints = app.MapGroup("rangos/{rangoId:int}/ingredientes");

        ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsyn);
        ingredientesEndpoints.MapPost("", (int rangoId) =>
        {
            throw new NotImplementedException();
        });
    }
}
