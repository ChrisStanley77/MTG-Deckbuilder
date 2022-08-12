using Steeltoe.Discovery.Client;
using Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.Configure<CardDatabaseSettings>(builder.Configuration.GetSection("CardDatabase"));
builder.Services.Configure<DeckDatabaseSettings>(builder.Configuration.GetSection("DeckDatabase"));
builder.Services.AddSingleton<Service.CardService>();
builder.Services.AddSingleton<ServiceDeck.DeckService>();
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
