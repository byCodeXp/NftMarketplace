namespace Domain;

public record Env
{
    public record Roles
    {
        public const string User = "User";
    }

    public record Identity
    {
        public record Claims
        {
            public const string Id = "id";
            public const string Roles = "roles";
        }

        public record TokenExpirationTime
        {
            public static TimeSpan OneDay = TimeSpan.FromDays(1);
            public static TimeSpan SevenDays = TimeSpan.FromDays(7);
        }
    }
}
