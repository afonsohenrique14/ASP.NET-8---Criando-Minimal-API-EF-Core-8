
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Models;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.Entities;

namespace RangoAgil.API.EndpointHandlers;

public static class RangosHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> GetRangosAsync
    (
        RangoDbContext rangoDbContext, 
        IMapper mapper,
        ILogger<RangoDTO> logger,
        [FromQuery(Name ="name")]string? rangoNome
    )
    {
        var rangosEntity = await rangoDbContext.Rangos
                                    .Where(
                                        x=>
                                            rangoNome == null ||
                                            x.Nome.ToLower().Contains(rangoNome.ToLower())
                                        )
                                    .ToListAsync();

        if(rangosEntity.Count <= 0 || rangosEntity == null)
        {
            logger.LogInformation($"Rango não encontrado. Parâmetro: {rangoNome}");
            return TypedResults.NoContent();
        }

        logger.LogInformation("Retornando o Rango encontrado");
        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
        
    }

    public static async Task<Results< NotFound, Ok<RangoDTO>>> GetRangoByIdAsync(
            RangoDbContext rangoDbContext,
            IMapper mapper,
            int rangoId)
    {

            var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

            return EntityRango is null? TypedResults.NotFound(): TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));
            
    }



    public static async Task<CreatedAtRoute<RangoDTO>> RangoPostAsync
    (    
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForCreationDTO rangoForCreation
        ) {
        
        var rangosEntity = mapper.Map<Rango>(rangoForCreation);

        rangoDbContext.Add(rangosEntity);
        await rangoDbContext.SaveChangesAsync();

        var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

        return TypedResults.CreatedAtRoute(rangoToReturn, "GetRangos", new {rangoId= rangoToReturn.Id});

    }

    public static async Task<Results<NotFound, Ok<RangoDTO>>> RangoPutAsync (
        RangoDbContext rangoDbContext,
        IMapper mapper,
        RangoForUpdateDTO rangoForUpdate,
        int rangoId
        )
    {

        var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

        if(EntityRango == null)
            return TypedResults.NotFound();

        mapper.Map(rangoForUpdate, EntityRango);

        await rangoDbContext.SaveChangesAsync();

        var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

        return TypedResults.Ok(rangoToReturn);
    }

    public static async Task<Results<NotFound, Ok>> RangoDeleteAsync (
        RangoDbContext rangoDbContext,
        int rangoId
        ) 
    {

        var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

        if(EntityRango == null)
            return TypedResults.NotFound();

        rangoDbContext.Rangos.Remove(EntityRango);

        await rangoDbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }


}
