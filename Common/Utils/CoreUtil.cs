namespace BF1.MemoryWebAPI.Common.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 目标进程
    /// </summary>
    public const string TargetAppName = "bf1";    // 战地1
    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "战地1内存Web服务器 v";

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static Version ClientVersionInfo = Assembly.GetExecutingAssembly().GetName().Version;

    /// <summary>
    /// 程序客户端最后编译时间
    /// </summary>
    public static string ClientBuildTime = File.GetLastWriteTime(Process.GetCurrentProcess().MainModule.FileName).ToString();
}
