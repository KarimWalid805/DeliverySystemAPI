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

    // GET all deliveries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
    {
        return await _context.Deliveries
            .Include(d => d.Order) // optional
            .ToListAsync();
    }

    // GET single delivery by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Delivery>> GetDelivery(int id)
    {
        var delivery = await _context.Deliveries
            .Include(d => d.Order) // optional
            .FirstOrDefaultAsync(d => d.DeliveryId == id);

        if (delivery == null)
            return NotFound();

        return delivery;
    }

    // POST: create delivery
    [HttpPost]
    public async Task<ActionResult<Delivery>> CreateDelivery([FromBody] Delivery delivery)
    {
        var newDelivery = new Delivery
        {
            OrdersId = delivery.OrdersId,
            DriverId = delivery.DriverId,
            customersName = delivery.customersName,
            customersAddress = delivery.customersAddress,
            DeliveryDate = delivery.DeliveryDate
        };

        _context.Deliveries.Add(newDelivery);
        await _context.SaveChangesAsync();

        // Return the single delivery
        return CreatedAtAction(nameof(GetDelivery), new { id = newDelivery.DeliveryId }, newDelivery);
    }
}
