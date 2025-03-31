using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("role-based")]
    public IActionResult GetUserByRole()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Testuser"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "mock"));

        HttpContext.User = user;

        if(user.IsInRole("Admin"))
        {
            return Ok(new { Message = "Access granted for Admin role."});
        }
        else
        {
            return Forbid();
        }
    }

    [HttpGet("claims-based")]
    public IActionResult GetUserByClaim()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim("Department", "IT")
        }, "mock"));

        HttpContext.User = user;

        var hasClaim = user.HasClaim( c => c.Type == "Department" && c.Value == "IT");

        if(hasClaim)
        {
            return Ok(new {Message = "Access granted for IT department."});
        }
        else
        {
            return Forbid();
        }
    }
}