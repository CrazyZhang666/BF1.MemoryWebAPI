using BF1.MemoryWebAPI.Features.Data;

namespace BF1.MemoryWebAPI.Features;

public class RespJson
{
    public string ServerName { get; set; }
    public long GameId { get; set; }
    public string ServerTime { get; set; }
    public ScoreInfo ScoreTeam1 { get; set; }
    public ScoreInfo ScoreTeam2 { get; set; }

    public List<PlayerData> PlayerListTeam0 { get; set; }
    public List<PlayerData> PlayerListTeam1 { get; set; }
    public List<PlayerData> PlayerListTeam2 { get; set; }

    public class ScoreInfo
    {
        public int Current { get; set; }
        public int FromeKill { get; set; }
        public int FromeFlag { get; set; }
    }
}
