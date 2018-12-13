using Newtonsoft.Json;

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

        // TODO: Fill in the rest

        [JsonProperty("detailedStatType")]
        public string DetailedStatType { get; set; }
    }

    public class BasicStats
    {
        [JsonProperty("timePlayed")]
        public ulong? TimePlayed { get; set; }
        [JsonProperty("wins")]
        public int? Wins { get; set; }
        [JsonProperty("losses")]
        public int? Losses { get; set; }
        [JsonProperty("kills")]
        public int? Kills { get; set; }
        [JsonProperty("deaths")]
        public int? Deaths { get; set; }
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
}
