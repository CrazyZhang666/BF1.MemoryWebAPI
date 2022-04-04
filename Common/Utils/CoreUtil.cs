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

    /// <summary>
    /// 获取本地IP地址
    /// </summary>
    /// <returns></returns>
    public static string GetLocalIP()
    {
        string HostName = Dns.GetHostName();    // 得到主机名
        IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
        for (int i = 0; i < IpEntry.AddressList.Length; i++)
        {
            // 从IP地址列表中筛选出IPv4类型的IP地址
            // AddressFamily.InterNetwork表示此IP为IPv4,
            // AddressFamily.InterNetworkV6表示此地址为IPv6类型
            if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                return IpEntry.AddressList[i].ToString();
            }
        }

        return "未找到本地IP";
    }
}
