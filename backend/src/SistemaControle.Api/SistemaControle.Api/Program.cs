using Microsoft.EntityFrameworkCore;
using SistemaControle.Infra.DI;
using SistemaControle.Infra.EF.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddServiceCollection(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Controle V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

{
    var scope  = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<SistemaControleContext>();
    dbContext.Database.Migrate();
}

app.Run();