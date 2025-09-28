using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly onlineDeliveryDbContext _context;

    public AuthController(onlineDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpPost("customer")]
    public async Task<IActionResult> CustomerLogin([FromBody] LoginDto login)
    {
        var user = await _context.customeraccount
            .FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        return Ok(new
        {
            user.Id,
            user.Username,
            user.Password,
        });
    }

    [HttpPost("driver")]
    public async Task<IActionResult> DriverLogin([FromBody] LoginDto login)
    {
        var user = await _context.driveraccount
            .FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        return Ok(new
        {
            user.Id,
            user.Username,
            user.Password,
        });
    }

    [HttpPost("admin")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginDto login)
    {
        var user = await _context.adminaccount
            .FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        return Ok(new
        {
            user.Username
        });
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}