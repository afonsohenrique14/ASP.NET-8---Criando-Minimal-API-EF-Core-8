using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
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


app.MapGet("/rangos", 
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

app.MapGet("/rango/{id:int}", async Task<Results< NotFound, Ok<RangoDTO>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int id) =>
{

    var EntityRango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x=> x.Id ==id);

    return EntityRango is null? TypedResults.NotFound(): TypedResults.Ok(mapper.Map<RangoDTO>(EntityRango));
    
});

app.MapGet("/rango/{rangoId:int}/ingredientes", async Task<Results<NoContent, Ok<IEnumerable<IngredienteDTO>>>> (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId) =>
{

    var EntityIngredientes = await rangoDbContext.Rangos
                                .Include(r => r.Ingredientes)
                                .FirstOrDefaultAsync(r => r.Id == rangoId);

    

    return EntityIngredientes is null? TypedResults.NoContent(): TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>> (EntityIngredientes?.Ingredientes));
});



app.Run();
