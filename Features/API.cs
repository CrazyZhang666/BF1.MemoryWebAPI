using BF1.MemoryWebAPI.Features.Core;
using BF1.MemoryWebAPI.Features.Data;
using BF1.MemoryWebAPI.Features.Utils;

namespace BF1.MemoryWebAPI.Features;

public static class API
{
    private const int MaxPlayer = 74;

    private static List<PlayerData> PlayerList_All = new();
    private static List<PlayerData> PlayerList_Team0 = new();
    private static List<PlayerData> PlayerList_Team1 = new();
    private static List<PlayerData> PlayerList_Team2 = new();

    public static object GetPlayerList()
    {
        try
        {
            if (Memory.Initialize())
            {
                PlayerList_All.Clear();
                PlayerList_Team0.Clear();
                PlayerList_Team1.Clear();
                PlayerList_Team2.Clear();

                var RespJson_Bf1 = new RespJson();

                RespJson_Bf1.ServerName = Memory.ReadString(Memory.GetBaseAddress() + Offsets.ServerName_Offset, Offsets.ServerName, 64);
                RespJson_Bf1.GameId = Memory.Read<long>(Memory.GetBaseAddress() + Offsets.GameId_Offset, Offsets.GameId);

                var _serverTime = Memory.Read<float>(Memory.GetBaseAddress() + Offsets.ServerTime_Offset, Offsets.ServerTime);
                RespJson_Bf1.ServerTime = PlayerUtil.SecondsToMMSS(_serverTime);

                var _sOffset0 = Memory.Read<long>(Memory.GetBaseAddress() + Offsets.ServerScore_Offset, Offsets.ServerScoreTeam);
                // 队伍1分数
                RespJson_Bf1.ScoreTeam1 = new RespJson.ScoreInfo()
                {
                    Current = Memory.Read<int>(_sOffset0 + 0x2B0),
                    FromeKill = Memory.Read<int>(_sOffset0 + 0x2B0 + 0x60),
                    FromeFlag = Memory.Read<int>(_sOffset0 + 0x2B0 + 0x100)
                };

                // 队伍2分数
                RespJson_Bf1.ScoreTeam2 = new RespJson.ScoreInfo()
                {
                    Current = Memory.Read<int>(_sOffset0 + 0x2B8),
                    FromeKill = Memory.Read<int>(_sOffset0 + 0x2B0 + 0x68),
                    FromeFlag = Memory.Read<int>(_sOffset0 + 0x2B0 + 0x108)
                };

                //////////////////////////////// 玩家数据 ////////////////////////////////

                for (int i = 0; i < MaxPlayer; i++)
                {
                    var _baseAddress = Player.GetPlayerById(i);
                    if (!Memory.IsValid(_baseAddress))
                        continue;

                    var _mark = Memory.Read<byte>(_baseAddress + 0x1D7C);
                    var _teamId = Memory.Read<int>(_baseAddress + 0x1C34);
                    var _spectator = Memory.Read<byte>(_baseAddress + 0x1C31);
                    var _personaId = Memory.Read<long>(_baseAddress + 0x38);
                    var _squadId = Memory.Read<int>(_baseAddress + 0x1E50);
                    var _name = Memory.ReadString(_baseAddress + 0x2156, 64);
                    if (string.IsNullOrEmpty(_name))
                        continue;

                    var _weaponSlot = new string[8] { "", "", "", "", "", "", "", "" };

                    var _pClientVehicleEntity = Memory.Read<long>(_baseAddress + 0x1D38);
                    if (Memory.IsValid(_pClientVehicleEntity))
                    {
                        var _pVehicleEntityData = Memory.Read<long>(_pClientVehicleEntity + 0x30);
                        _weaponSlot[0] = Memory.ReadString(Memory.Read<long>(_pVehicleEntityData + 0x2F8), 64);
                    }
                    else
                    {
                        var _pClientSoldierEntity = Memory.Read<long>(_baseAddress + 0x1D48);
                        var _pClientSoldierWeaponComponent = Memory.Read<long>(_pClientSoldierEntity + 0x698);
                        var _m_handler = Memory.Read<long>(_pClientSoldierWeaponComponent + 0x8A8);

                        for (int j = 0; j < 8; j++)
                        {
                            var offset0 = Memory.Read<long>(_m_handler + j * 0x8);

                            offset0 = Memory.Read<long>(offset0 + 0x4A30);
                            offset0 = Memory.Read<long>(offset0 + 0x20);
                            offset0 = Memory.Read<long>(offset0 + 0x38);
                            offset0 = Memory.Read<long>(offset0 + 0x20);

                            _weaponSlot[j] = Memory.ReadString(offset0, 64);
                        }
                    }

                    int index = PlayerList_All.FindIndex(val => val.Name == _name);
                    if (index == -1)
                    {
                        PlayerList_All.Add(new PlayerData()
                        {
                            Admin = "",
                            VIP = "",

                            Mark = _mark,
                            TeamId = _teamId,
                            Spectator = _spectator,
                            Clan = PlayerUtil.GetPlayerTargetName(_name, true),
                            Name = PlayerUtil.GetPlayerTargetName(_name, false),
                            PersonaId = _personaId,
                            SquadId = PlayerUtil.GetSquadChsName(_squadId),

                            Rank = 0,
                            Kill = 0,
                            Dead = 0,
                            Score = 0,

                            KD = "0.00",
                            KPM = "0.00",

                            WeaponS0 = PlayerUtil.GetWeaponChsName(_weaponSlot[0]),
                            WeaponS1 = PlayerUtil.GetWeaponChsName(_weaponSlot[1]),
                            WeaponS2 = PlayerUtil.GetWeaponChsName(_weaponSlot[2]),
                            WeaponS3 = PlayerUtil.GetWeaponChsName(_weaponSlot[3]),
                            WeaponS4 = PlayerUtil.GetWeaponChsName(_weaponSlot[4]),
                            WeaponS5 = PlayerUtil.GetWeaponChsName(_weaponSlot[5]),
                            WeaponS6 = PlayerUtil.GetWeaponChsName(_weaponSlot[6]),
                            WeaponS7 = PlayerUtil.GetWeaponChsName(_weaponSlot[7]),
                        });
                    }
                }

                //////////////////////////////// 得分板数据 ////////////////////////////////

                var _pClientScoreBA = Memory.Read<long>(Memory.GetBaseAddress() + 0x39EB8D8);
                _pClientScoreBA = Memory.Read<long>(_pClientScoreBA + 0x68);

                for (int i = 0; i < MaxPlayer; i++)
                {
                    _pClientScoreBA = Memory.Read<long>(_pClientScoreBA);
                    var _pClientScoreOffset = Memory.Read<long>(_pClientScoreBA + 0x10);
                    if (!Memory.IsValid(_pClientScoreBA))
                        continue;

                    var _mark = Memory.Read<byte>(_pClientScoreOffset + 0x300);
                    var _rank = Memory.Read<int>(_pClientScoreOffset + 0x304);
                    if (_rank == 0)
                        continue;
                    var _kill = Memory.Read<int>(_pClientScoreOffset + 0x308);
                    var _dead = Memory.Read<int>(_pClientScoreOffset + 0x30C);
                    var _score = Memory.Read<int>(_pClientScoreOffset + 0x314);

                    var index = PlayerList_All.FindIndex(val => val.Mark == _mark);
                    if (index != -1)
                    {
                        PlayerList_All[index].Rank = _rank;
                        PlayerList_All[index].Kill = _kill;
                        PlayerList_All[index].Dead = _dead;
                        PlayerList_All[index].Score = _score;
                        PlayerList_All[index].KD = PlayerUtil.GetPlayerKD(_kill, _dead).ToString("0.00");
                        PlayerList_All[index].KPM = PlayerUtil.GetPlayerKPM(_kill, PlayerUtil.SecondsToMM(_serverTime)).ToString("0.00");
                    }
                }

                //////////////////////////////// 队伍数据整理 ////////////////////////////////

                foreach (var item in PlayerList_All)
                {
                    if (item.TeamId == 0)
                    {
                        PlayerList_Team0.Add(item);
                    }
                    if (item.TeamId == 1)
                    {
                        PlayerList_Team1.Add(item);
                    }
                    else if (item.TeamId == 2)
                    {
                        PlayerList_Team2.Add(item);
                    }
                }

                PlayerList_Team0.Sort((a, b) => a.Name.CompareTo(b.Name));
                PlayerList_Team1.Sort((a, b) => b.Score.CompareTo(a.Score));
                PlayerList_Team2.Sort((a, b) => b.Score.CompareTo(a.Score));

                RespJson_Bf1.PlayerListTeam0 = PlayerList_Team0;
                RespJson_Bf1.PlayerListTeam1 = PlayerList_Team1;
                RespJson_Bf1.PlayerListTeam2 = PlayerList_Team2;

                Memory.CloseHandle();

                Results.StatusCode(200);
                return RespJson_Bf1;
            }
            else
            {
                Results.StatusCode(400);
                return "战地1内存模块初始化失败";
            }
        }
        catch (Exception ex)
        {
            Results.StatusCode(500);
            return $"异常：{ex.Message}";
        }
    }
}
