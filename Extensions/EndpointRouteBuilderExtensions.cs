
using RangoAgil.API.EndpointFilters;
using RangoAgil.API.EndpointHandlers;
using RangoAgil.API.Metadatas;
namespace RangoAgil.API.Extensions;



public static class EndpointRouteBuilderExtensions
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder app)
    {
        
        app.MapGet("/pratos/{pratoId:int}", (int pratoId)=> $"O prato {pratoId} é delicioso.")
        .WithMetadata( new DeprecatedInSwaggerMetadata())
        .AddOpenApiOperationTransformer((opperation, context, ct)=>{
            opperation.Deprecated = true;
            return Task.CompletedTask;
        })
        .WithSummary("Este endpoint está deprecated e será descontinuado na versão 2 desta API.")
        .WithDescription("Por favor utilize a outra rota desta API sendo ela /rangos/{rangoId} para evitar maiores transtornos.")
        ;

        var rangosEndpoints = app.MapGroup("/rangos");

        var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
        var rangosComIDEndpointsAndLockFilterEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}")
            .AddEndpointFilter(new RangosIsLockedFilter(8))
            .AddEndpointFilter(new RangosIsLockedFilter(10))
            .AddEndpointFilter<LogNotFoundResponseFilter>();


        rangosEndpoints.MapGet("", RangosHandlers.GetRangosAsync)
        .WithSummary("Está rota retornará uma lista com todos os rangos.");

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
