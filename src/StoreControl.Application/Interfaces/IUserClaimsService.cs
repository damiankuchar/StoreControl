namespace StoreControl.Application.Interfaces
{
    public interface IUserClaimsService
    {
        Dictionary<string, string> GetUserClaims();
        Dictionary<string, string> GetClaimsFromExpiredToken(string token);
    }
}
