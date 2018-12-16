using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanionAPI.Models
{
    public class StatsViewModel
    {
    }

    public class GameStats
    {
        [JsonProperty("bf4")]
        public BasicStats BF4 { get; set; }
        [JsonProperty("tunguska")]
        public BasicStats BF1 { get; set; }
        [JsonProperty("casablanca")]
        public BasicStats BFV { get; set; }
    }

    public class BasicStats
    {
        [JsonProperty("timePlayed")]
        public ulong? TimePlayed { get; set; }
        [JsonProperty("wins")]
        public ulong? Wins { get; set; }
        [JsonProperty("losses")]
        public ulong? Losses { get; set; }
        [JsonProperty("kills")]
        public ulong? Kills { get; set; }
        [JsonProperty("deaths")]
        public ulong? Deaths { get; set; }
        [JsonProperty("kpm")]
        public double? KPM { get; set; }
        [JsonProperty("spm")]
        public double? SPM { get; set; }
        [JsonProperty("skill")]
        public double? Skill { get; set; }
        [JsonProperty("soldierImageUrl")]
        public string SoldierImageUrl { get; set; }
        [JsonProperty("rank")]
        public Rank Rank { get; set; }
        [JsonProperty("rankProgress")]
        public RankProgress RankProgress { get; set; }
        [JsonProperty("freemiumRank")]
        public FreemiumRank FreemiumRank { get; set; }
        [JsonProperty("completion")]
        public List<Completion> Completion { get; set; }
        /// <summary>
        /// TODO: Found from Career
        /// </summary>
        [JsonProperty("highlights")]
        public object HighLights { get; set; }
        /// <summary>
        /// TODO: Found from Career
        /// </summary>
        [JsonProperty("highlightsByType")]
        public object HighLightsByType { get; set; }
        /// <summary>
        /// TODO: Found from Career
        /// </summary>
        [JsonProperty("equippedDogtags")]
        public object EquippedDogtags { get; set; }
    }

    public class Rank
    {
        [JsonProperty("number")]
        public int? Number { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }

    public class RankProgress
    {
        [JsonProperty("current")]
        public ulong? Current { get; set; }
        [JsonProperty("total")]
        public ulong? Total { get; set; }
    }

    public class FreemiumRank
    {
        [JsonProperty("rank")]
        public Rank Rank { get; set; }
        [JsonProperty("freemiumScore")]
        public ulong? FreemiumScore { get; set; }
        [JsonProperty("progress")]
        public RankProgress Progress { get; set; }
    }

    public class Completion
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("progress")]
        public RankProgress Progress { get; set; }
        [JsonProperty("rank")]
        public object Rank { get; set; }
    }

    public class KitStat
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("prettyName")]
        public string PrettyName { get; set; }
        [JsonProperty("kitType")]
        public object KitType { get; set; }
        [JsonProperty("score")]
        public ulong? Score { get; set; }
        [JsonProperty("kills")]
        public double? Kills { get; set; }
        [JsonProperty("secondsAs")]
        public ulong? SecondsAs { get; set; }
    }

    public class GameModeStats
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("prettyName")]
        public string PrettyName { get; set; }
        [JsonProperty("wins")]
        public ulong? Wins { get; set; }
        [JsonProperty("score")]
        public ulong? Score { get; set; }
        [JsonProperty("losses")]
        public ulong? Losses { get; set; }
        [JsonProperty("winLossRatio")]
        public ulong? WinLossRatio { get; set; }
    }

    public class VehicleStats
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("prettyName")]
        public string PrettyName { get; set; }
        [JsonProperty("killsAs")]
        public ulong? KillsAs { get; set; }
        [JsonProperty("vehiclesDestroyed")]
        public ulong? VehiclesDestroyed { get; set; }
        [JsonProperty("timeSpent")]
        public ulong? TimeSpent { get; set; }
    }

    public class RankInfo
    {
        [JsonProperty("currentRank")]
        public int? CurrentRank { get; set; }
        [JsonProperty("progression")]
        public Progression Progression { get; set; }
    }

    public class Progression
    {
        [JsonProperty("valueNeeded")]
        public double? ValueNeeded { get; set; }
        [JsonProperty("valueAcquired")]
        public double? ValueAcquired { get; set; }
        [JsonProperty("unlocked")]
        public bool Unlocked { get; set; }
    }

    public class TopClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        // TODO: Images tag is missing
        [JsonProperty("rank")]
        public RankInfo Rank { get; set; }
        // TODO: Archetypes tag is missing
        [JsonProperty("stats")]
        public Stats Stats { get; set; }
        [JsonProperty("timePlayed")]
        public ulong? TimePlayed { get; set; }
    }

    public class Stats
    {
        [JsonProperty("values")]
        public StatValues Values { get; set; }
    }

    public class StatValues
    {
        [JsonProperty("kills")]
        public ulong? Kills { get; set; }
        [JsonProperty("kd")]
        public double? KD { get; set; }
        [JsonProperty("deaths")]
        public ulong? Deaths { get; set; }
        [JsonProperty("accuracy")]
        public ulong? Accuracy { get; set; }
        [JsonProperty("rank")]
        public int? Rank { get; set; }
        [JsonProperty("seconds")]
        public ulong? Seconds { get; set; }
        [JsonProperty("score")]
        public ulong? Score { get; set; }
        [JsonProperty("revives")]
        public ulong? Revives { get; set; }
        [JsonProperty("hits")]
        public ulong? Hits { get; set; }
        [JsonProperty("shots")]
        public ulong? Shots { get; set; }
    }
}
