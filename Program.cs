using BF1.MemoryWebAPI.Features;
using BF1.MemoryWebAPI.Common.Utils;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Urls.Add($"http://{CoreUtil.GetLocalIP()}:2333");

// ����̨���ڱ���
Console.Title = CoreUtil.MainAppWindowName + CoreUtil.ClientVersionInfo
     + " - ������ʱ�� : " + CoreUtil.ClientBuildTime;

Console.WriteLine($"����IP��ַ��http://{CoreUtil.GetLocalIP()}:2333");
Console.WriteLine();

// ��·��
app.MapGet("/", () =>
{
    return "ս��1�ڴ�Web������ ��������...";
});

// ��ȡ����б�
app.MapGet("/getPlayerList", () =>
{
    return API.GetPlayerList();
});

app.Run();
