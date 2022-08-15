using Steeltoe.Discovery.Client;
using Settings;
using Service;
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

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseCors();
app.Run();
