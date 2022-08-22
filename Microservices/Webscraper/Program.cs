using Steeltoe.Discovery.Client;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery;
using Service;
using Settings;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.Configure<CardDatabaseSettings>(builder.Configuration.GetSection("CardDatabase"));
builder.Services.Configure<DeckDatabaseSettings>(builder.Configuration.GetSection("DeckDatabase"));
builder.Services.AddSingleton<CardService>();
builder.Services.AddSingleton<DeckService>();
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

app.Run();
