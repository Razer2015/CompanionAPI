using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanionAPI.Models
{
    public class DetailedStatsRequestModel
    {
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("personaId")]
        public string PersonaId { get; set; }

        public DetailedStatsRequestModel(string game, string personaId) {
            Game = game;
            PersonaId = personaId;
        }
    }

    public class DetailedStatsResponseModel
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
        /// <summary>
        /// Wut is dis and where is it used?_?
        /// </summary>
        [JsonProperty("freemiumRank")]
        public object FreemiumRank { get; set; }
        /// <summary>
        /// Wut is dis and where is it used?_?
        /// </summary>
        [JsonProperty("completion")]
        public object Completion { get; set; }
        /// <summary>
        /// Wut is dis and where is it used?_?
        /// </summary>
        [JsonProperty("highlights")]
        public object HighLights { get; set; }
        /// <summary>
        /// Wut is dis and where is it used?_?
        /// </summary>
        [JsonProperty("highlightsByType")]
        public object HighLightsByType { get; set; }
        /// <summary>
        /// Wut is dis and where is it used?_?
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
