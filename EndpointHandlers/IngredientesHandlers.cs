using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Models;

namespace RangoAgil.API.EndpointHandlers;

public static class IngredientesHandlers
{
    public static  async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>> GetIngredientesAsyn(
        RangoDbContext rangoDbContext,
        IMapper mapper,
        int rangoId) 
    {

        var EntityIngredientes = await rangoDbContext.Rangos
                                    .Include(r => r.Ingredientes)
                                    .FirstOrDefaultAsync(r => r.Id == rangoId);

        

        return EntityIngredientes is null? TypedResults.NoContent(): TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>> (EntityIngredientes?.Ingredientes));
    }
}
