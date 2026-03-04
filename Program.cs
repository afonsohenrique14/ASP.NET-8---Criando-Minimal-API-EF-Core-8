
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Extensions;
using RangoAgil.API.OperationFilters;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDBConStr"])
);

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<RangoDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminFromBrazil", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("country", "Brazil")
    )
    .AddPolicy("RequirManagerFromBrazil", policy =>
    
        policy
            .RequireRole("Manager")
            .RequireClaim("country", "Brazil")
    );

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{   
    options.AddSecurityDefinition("TokenAuthRango",
        new OpenApiSecurityScheme
        {
            Name = "Autorization",
            Description = "Token baseado em autenticação e autorização",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        }
    );


    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        // chave = referência ao scheme definido acima
        [new OpenApiSecuritySchemeReference("TokenAuthRango", document)] = new List<string>()
        // ou = [] (C# 12) / Array.Empty<string>() dependendo do tipo esperado
    });


    options.OperationFilter<DeprecatedInSwaggerOperationFilter>();    

});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();
app.RegisterIdentityEndpoint();

app.Run();
