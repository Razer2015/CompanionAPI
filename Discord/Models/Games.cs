namespace Discord.Models
{
    public enum Games
    {
        BF4,
        BF1,
        BFV
    }

    public static class EnumExtensions
    {
        public static string GetCodeName(this Games game) {
            switch (game) {
                case Games.BF4:
                    return "bf4";
                case Games.BF1:
                    return "tunguska";
                case Games.BFV:
                    return "casablanca";
                default:
                    return "";
            }
        }
    }
}
