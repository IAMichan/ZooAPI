using ZooAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ZooDbContext>();

// Add Razor Pages services
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthorization();

// Add this line to enable routing
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // This line maps the controllers
    endpoints.MapControllers();

});

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

    await next();

    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

app.Run();