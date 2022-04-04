using BF1.MemoryWebAPI.Features;
using BF1.MemoryWebAPI.Common.Utils;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// 控制台窗口标题
Console.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo
     + " - 最后编译时间 : " + CoreUtil.ClientBuildTime; ;

// 主路由
app.MapGet("/getPlayerList", () =>
{
    return API.GetPlayerList();
});

app.Run();
