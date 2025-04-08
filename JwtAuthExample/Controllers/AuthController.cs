using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string, (string Password, string Role)> Users = new()
    {
        { "user1", ("password1", "User") },
        { "admin", ("password2", "Admin") }
    };

    private readonly ITokenService _tokenService;

    private static readonly Dictionary<string, string> RefreshTokens = new();

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if(string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Username and password are required");
        }
        
        if (!Users.TryGetValue(request.Username!, out var user) || user.Password != request.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        var accessToken = _tokenService.GenerateAccessToken(request.Username, user.Role);
        var refreshToken = _tokenService.GenerateRefreshToken();

        RefreshTokens[refreshToken] = request.Username;

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshTokenRequest request)
    {
        if(string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest("Refresh token is required");
        }
        
        if (request.RefreshToken == null || !RefreshTokens.TryGetValue(request.RefreshToken, out var username))
        {
            return Unauthorized("Invalid refresh token");
        }

        RefreshTokens.Remove(request.RefreshToken);

        var role = Users[username].Role;
        var newAccessToken = _tokenService.GenerateAccessToken(username, role);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        RefreshTokens[newRefreshToken] = username;

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }
}
