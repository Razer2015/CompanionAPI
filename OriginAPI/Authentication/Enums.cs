namespace Origin.Authentication
{
    public enum AccessType
    {
        Origin,
        Companion
    }

    public enum UserSearchStatus
    {
        SUCCESS = 1,
        NO_MATCHES = 2,
        PARTIAL_MATCHES = 3,
        TOO_MANY_RESULTS = 4
    }
}
