using DeliverySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly onlineDeliveryDbContext _context;
    public DriversController(onlineDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
    {
        return await _context.Drivers.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Driver>> CreateDriver(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDrivers), new { id = driver.DriverId }, driver);
    }
}
