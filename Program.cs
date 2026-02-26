using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Entities;
using RangoAgil.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();   
}


app.MapGet("/", () => "Hello World!");

var rangosEndpoints = app.MapGroup("/rangos");
var rangosComIDEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");
var ingredientesEndpoints = rangosComIDEndpoints.MapGroup("/ingredientes");

rangosEndpoints.MapGet("", 
    async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> 
    (
        RangoDbContext rangoDbContext, 
        IMapper mapper,
        [FromQuery(Name ="name")]string? rangoNome) =>
    {
        var rangosEntity = await rangoDbContext.Rangos
                                    .Where(
                                        x=>
                                            rangoNome == null ||
                                            x.Nome.ToLower().Contains(rangoNome.ToLower())
                                        )
                                    .ToListAsync();

        if(rangosEntity.Count <= 0 || rangosEntity == null)
            return TypedResults.NoContent();

        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));
        
    });

rangosComIDEndpoints.MapGet("", async Task<Results< NotFound, Ok<RangoDTO>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId) =>
{

    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

    return EntityRango is null? TypedResults.NotFound(): TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));
    
}).WithName("GetRangos");

ingredientesEndpoints.MapGet("", async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId) =>
{

    var EntityIngredientes = await rangoDbContext.Rangos
                                .Include(r => r.Ingredientes)
                                .FirstOrDefaultAsync(r => r.Id == rangoId);

    

    return EntityIngredientes is null? TypedResults.NoContent(): TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>> (EntityIngredientes?.Ingredientes));
});

rangosEndpoints.MapPost("", async Task<CreatedAtRoute<RangoDTO>>
 (    
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForCreationDTO rangoForCreation
    ) =>  {
    
    var rangosEntity = mapper.Map<Rango>(rangoForCreation);

    rangoDbContext.Add(rangosEntity);
    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(rangosEntity);

    return TypedResults.CreatedAtRoute(rangoToReturn, "GetRangos", new {rangoId= rangoToReturn.Id});

});

rangosComIDEndpoints.MapPut("", async Task<Results<NotFound, Ok<RangoDTO>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    RangoForUpdateDTO rangoForUpdate,
    int rangoId
    ) =>
{

    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

    if(EntityRango == null)
        return TypedResults.NotFound();

    mapper.Map(rangoForUpdate, EntityRango);

    await rangoDbContext.SaveChangesAsync();

    var rangoToReturn = mapper.Map<RangoDTO>(EntityRango);

    return TypedResults.Ok(rangoToReturn);
});

rangosComIDEndpoints.MapDelete("", async Task<Results<NotFound, Ok>> (
    RangoDbContext rangoDbContext,
    int rangoId
    ) =>
{

    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==rangoId);

    if(EntityRango == null)
        return TypedResults.NotFound();

    rangoDbContext.Rangos.Remove(EntityRango);

    await rangoDbContext.SaveChangesAsync();

    return TypedResults.Ok();
});





app.Run();
