using DeliverySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly onlineDeliveryDbContext _context;

    public RegisterController(onlineDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpPost("customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerAccount account)
    {
        var exists = await _context.customeraccount.AnyAsync(u => u.Username == account.Username);
        if (exists)
            return BadRequest("Username already exists");

        _context.customeraccount.Add(account);
        await _context.SaveChangesAsync();
        return Ok(account);
    }

    [HttpPost("driver")]
    public async Task<IActionResult> RegisterDriver([FromBody] DriverAccount account)
    {
        var exists = await _context.driveraccount.AnyAsync(u => u.Username == account.Username);
        if (exists)
            return BadRequest("Username already exists");

        _context.driveraccount.Add(account);
        await _context.SaveChangesAsync();
        return Ok(account);
    }

}