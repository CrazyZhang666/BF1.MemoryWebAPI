namespace BF1.MemoryWebAPI.Features.Utils;

public class PlayerUtil
{
    /// <summary>
    /// 获取玩家ID或队标
    /// </summary>
    public static string GetPlayerTargetName(string originalName, bool isClan)
    {
        if (string.IsNullOrEmpty(originalName))
            return "";

        int index = originalName.IndexOf("]");

        string clan;
        string name;

        if (index != -1)
        {
            clan = originalName.Substring(1, index - 1);
            name = originalName.Substring(index + 1);
        }
        else
        {
            clan = "";
            name = originalName;
        }

        if (isClan)
            return clan;
        else
            return name;
    }

    /// <summary>
    /// 获取小队的中文名称
    /// </summary>
    /// <param name="squadID"></param>
    /// <returns></returns>
    public static string GetSquadChsName(int squadID)
    {
        switch (squadID)
        {
            case 0:
                return "无";
            case 1:
                return "苹果";
            case 2:
                return "奶油";
            case 3:
                return "查理";
            case 4:
                return "达夫";
            case 5:
                return "爱德华";
            case 6:
                return "弗莱迪";
            case 7:
                return "乔治";
            case 8:
                return "哈利";
            case 9:
                return "墨水";
            case 10:
                return "强尼";
            case 11:
                return "国王";
            case 12:
                return "伦敦";
            case 13:
                return "猿猴";
            case 14:
                return "疯子";
            case 15:
                return "橘子";
            default:
                return squadID.ToString();
        }
    }

    /// <summary>
    /// 计算玩家KD比
    /// </summary>
    /// <param name="kill">玩家击杀数</param>
    /// <param name="dead">玩家死亡数</param>
    /// <returns>返回玩家KD比（小数float）<returns>
    public static float GetPlayerKD(int kill, int dead)
    {
        if (kill == 0 && dead >= 0)
            return 0.0f;
        else if (kill > 0 && dead == 0)
            return kill;
        else if (kill > 0 && dead > 0)
            return (float)kill / dead;
        else
            return (float)kill / dead;
    }

    /// <summary>
    /// 计算玩家KPM比
    /// </summary>
    /// <param name="kill"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public static float GetPlayerKPM(int kill, float minute)
    {
        if (minute != 0.0f)
            return kill / minute;
        else
            return 0.0f;
    }

    /// <summary>
    /// 小数类型的时间秒，转为mm:ss格式
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string SecondsToMMSS(float time)
    {
        try
        {
            if (time >= 0 && time <= 36000)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);

                DateTime dateTime = DateTime.Parse(timeSpan.ToString());

                return $"{dateTime:mm:ss}";
            }
            else
            {
                return "00:00";
            }
        }
        catch (Exception) { return "00:00"; }
    }

    /// <summary>
    /// 小数类型的时间秒，转为mm格式（分钟）
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static int SecondsToMM(float time)
    {
        try
        {
            if (time >= 0 && time <= 36000)
            {
                int a = (int)(time / 60);

                if (a != 0)
                    return a;
                else
                    return 0;
            }
            else
                return 0;
        }
        catch (Exception) { return 0; }
    }
}
