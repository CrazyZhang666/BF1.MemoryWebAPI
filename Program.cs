using BF1.MemoryWebAPI.Features;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/getPlayerList", () =>
{
    return API.GetPlayerList();
});

app.Run();
