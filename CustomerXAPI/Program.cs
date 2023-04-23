using IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Database");

// AutoMapper configuration
var mapper = MapperConfig.GetMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<CustomerXContext>(
        options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerX API", Version = "v1" });

    // Add authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };
    c.AddSecurityRequirement(securityRequirement);
});

ServiceConfiguration.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);
        c.DefaultModelsExpandDepth(-1);
        c.DefaultModelExpandDepth(-1);
        c.EnableFilter();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
    app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
}
else
{
    app.UseHttpsRedirection();
}

using (var scope =
  app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<CustomerXContext>())
    context.Database.Migrate();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();


app.Run();
