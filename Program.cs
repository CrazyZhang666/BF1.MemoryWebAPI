using BF1.MemoryWebAPI.Features;
using BF1.MemoryWebAPI.Common.Utils;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Urls.Add($"http://{CoreUtil.GetLocalIP()}:2333");

// 控制台窗口标题
Console.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo
     + " - 最后编译时间 : " + CoreUtil.ClientBuildTime;

Console.WriteLine($"本地IP地址：http://{CoreUtil.GetLocalIP()}:2333");
Console.WriteLine();

// 主路由
app.MapGet("/", () =>
{
    return "战地1内存Web服务器 正在运行...";
});

// 获取玩家列表
app.MapGet("/getPlayerList", () =>
{
    return API.GetPlayerList();
});

app.Run();
