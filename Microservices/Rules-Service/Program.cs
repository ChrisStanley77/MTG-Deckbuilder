using Microsoft.EntityFrameworkCore;
// using Steeltoe.Discovery.Client;
// using Steeltoe.Common.Discovery;
// using Steeltoe.Discovery.Eureka;
// using Steeltoe.Discovery;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
var app = builder.Build();
app.MapControllers();
app.UseCors();
app.MapGet("/", () => "Hello World!");

app.Run();
