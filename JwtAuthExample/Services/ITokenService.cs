public interface ITokenService
{
    string GenerateAccessToken(string username, string role);
    string GenerateRefreshToken();
}
