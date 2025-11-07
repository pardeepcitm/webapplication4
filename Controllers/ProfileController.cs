using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    [HttpGet("me")]
    public IActionResult GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.Identity?.Name;
        var email = User.FindFirstValue(ClaimTypes.Email);

        return Ok(new 
        { 
            UserId = userId, 
            Username = username, 
            Email = email 
        });
    }
}