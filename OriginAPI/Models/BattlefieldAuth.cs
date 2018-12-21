namespace OriginAPI.Models
{
    public class BattlefieldAuth
    {
        public bool Authenticated { get; set; }
        public string Gamertag { get; set; }
        public int? Age { get; set; }
        public string Country { get; set; }
        public ulong? Pid { get; set; }
        public ulong? PidId { get; set; }
        public object Underage { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public Connectedplatform[] ConnectedPlatforms { get; set; }
        public Uniqueconnectedplatform[] UniqueConnectedPlatforms { get; set; }
    }

    public class Connectedplatform
    {
        public string GamerTag { get; set; }
        public string Platform { get; set; }
    }

    public class Uniqueconnectedplatform
    {
        public string GamerTag { get; set; }
        public string Platform { get; set; }
    }

}
