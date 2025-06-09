using Microsoft.EntityFrameworkCore;
using WebAPINET8.Database;

var builder = WebApplication.CreateBuilder(args);

// Configuración de conexión a base de datos
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                          builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
Console.WriteLine($"Connection string: {connectionString}");


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ejemplo de API v1");
        c.RoutePrefix = "swagger";
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapWhen(context => context.Request.Path.StartsWithSegments("/ui"), subApp =>
{
    subApp.Use(async (context, next) =>
    {
        context.Request.Path = context.Request.Path.Value!.Replace("/ui", "");
        await next();
    });

    subApp.UseDefaultFiles();
    subApp.UseStaticFiles();  
});

app.Run();

