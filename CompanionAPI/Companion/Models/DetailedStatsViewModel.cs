using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanionAPI.Models
{
    public class DetailedStatsViewModel : StatsViewModel
    {
        [JsonProperty("basicStats")]
        public BasicStats BasicStats { get; set; }
        [JsonProperty("favoriteClass")]
        public string FavoriteClass { get; set; }
        [JsonProperty("kitStats")]
        public KitStat[] KitStats { get; set; }
        [JsonProperty("awardScore")]
        public ulong? AwardScore { get; set; }
        [JsonProperty("bonusScore")]
        public ulong? BonusScore { get; set; }
        [JsonProperty("squadScore")]
        public ulong? SquadScore { get; set; }
        [JsonProperty("avengerKills")]
        public ulong? AvengerKills { get; set; }
        [JsonProperty("saviorKills")]
        public ulong? SaviorKills { get; set; }
        [JsonProperty("highestKillStreak")]
        public ulong? HighestKillStreak { get; set; }
        [JsonProperty("dogtagsTaken")]
        public ulong? DogtagsTaken { get; set; }
        [JsonProperty("roundsPlayed")]
        public ulong? RoundsPlayed { get; set; }
        [JsonProperty("flagsCaptured")]
        public ulong? FlagsCaptured { get; set; }
        [JsonProperty("flagsDefended")]
        public ulong? FlagsDefended { get; set; }
        [JsonProperty("accuracyRatio")]
        public double? AccuracyRatio { get; set; }
        [JsonProperty("headShots")]
        public ulong? HeadShots { get; set; }
        [JsonProperty("longestHeadShot")]
        public double? LongestHeadShot { get; set; }
        [JsonProperty("nemesisKills")]
        public ulong? NemesisKills { get; set; }
        [JsonProperty("nemesisKillStreak")]
        public ulong? NemesisKillStreak { get; set; }
        [JsonProperty("revives")]
        public ulong? Revives { get; set; }
        [JsonProperty("heals")]
        public ulong? Heals { get; set; }
        [JsonProperty("repairs")]
        public ulong? Repairs { get; set; }
        [JsonProperty("suppressionAssist")]
        public ulong? SuppressionAssist { get; set; }
        [JsonProperty("kdr")]
        public double? KDR { get; set; }
        [JsonProperty("killAssists")]
        public ulong? KillAssists { get; set; }
        [JsonProperty("detailedStatType")]
        public string DetailedStatType { get; set; }
        [JsonProperty("gameModeStats")]
        public GameModeStats[] GameModeStats { get; set; }
        [JsonProperty("vehicleStats")]
        public VehicleStats[] VehicleStats { get; set; }

        #region BFV only
        [JsonProperty("tidesOfWarInfo")]
        public RankInfo TidesOfWarInfo { get; set; }
        [JsonProperty("draws")]
        public int? Draws { get; set; }
        [JsonProperty("collectibles")]
        public List<string> Collectibles { get; set; }
        // TODO: Royale tag is missing
        #endregion

        [JsonIgnore]
        public string EmblemUrl { get; set; }
    }
}
