using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AccountDB>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("docker_db1")));
builder.Services.AddDiscoveryClient(builder.Configuration);
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

app.MapGet("/", () => "To access the account api please enter /account/help for more information ");

app.Run();
