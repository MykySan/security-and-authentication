using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/secure")]
public class SecureController : ControllerBase
{
    [HttpGet("user")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult GetForUser()
    {
        return Ok("Hello, authorized user!");
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetForAdmin()
    {
        return Ok("Hello, Admin!");
    }
}
