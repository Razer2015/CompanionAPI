using Newtonsoft.Json;

namespace CompanionAPI.Models
{
    public class CareerViewModel : StatsViewModel
    {
        [JsonProperty("stats")]
        public BasicStats Stats { get; set; }
        [JsonProperty("gameStats")]
        public GameStats GameStats { get; set; }
        // TODO: highlights (seems empty though)
        [JsonProperty("emblem")]
        public string Emblem { get; set; }
    }
}
