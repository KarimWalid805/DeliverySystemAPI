using DeliverySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly onlineDeliveryDbContext _context;
    public DeliveryController(onlineDeliveryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Delivery>>> GetDelivery()
    {
        return await _context.Deliveries.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Delivery>> CreateDelivery(Delivery delivery)
    {
        _context.Deliveries.Add(delivery);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDelivery), new { id = delivery.DeliveryId }, delivery);
    }
}
