namespace StoreControl.Infrastructure.Authentication
{
    public  class JwtOptions
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
        public int ExpirationTime {  get; init; }
        public int RefreshTokenExpirationTime {  get; init; }
        public int ClockSkew { get; init; }
    }
}
