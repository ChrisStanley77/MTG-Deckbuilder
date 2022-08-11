var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
search s = new search();

app.MapGet("/", () => s.getCards());

app.Run();
