using BF1.MemoryWebAPI.Features;
using BF1.MemoryWebAPI.Common.Utils;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// ����̨���ڱ���
Console.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo
     + " - ������ʱ�� : " + CoreUtil.ClientBuildTime; ;

// ��·��
app.MapGet("/getPlayerList", () =>
{
    return API.GetPlayerList();
});

app.Run();
