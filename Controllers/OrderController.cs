using DeliverySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly onlineDeliveryDbContext _context;
    public OrderController(onlineDeliveryDbContext context)
    {
        _context = context;
    }

    // GET all orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Customer)
            .Select(o => new OrderDTO
            {
                OrdersId = o.OrdersId,
                Address = o.Address,
                OrderDate = o.OrderDate,
                ProductsList = o.ProductsList,
                Customer = new CustomerDTO
                {
                    CustomerId = o.Customer.CustomerId,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    Address = o.Customer.Address
                }
            })
            .ToListAsync();

        return Ok(orders);
    }

    // GET a single order
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Where(o => o.OrdersId == id)
            .Select(o => new OrderDTO
            {
                OrdersId = o.OrdersId,
                Address = o.Address,
                OrderDate = o.OrderDate,
                ProductsList = o.ProductsList,
                Customer = new CustomerDTO
                {
                    CustomerId = o.Customer.CustomerId,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    Address = o.Customer.Address
                }
            })
            .FirstOrDefaultAsync();

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // POST for driver form
    [HttpPost("driverForm")]
    public async Task<ActionResult<OrderDTO>> CreateDriverOrder(OrderCreateDTO dto)
    {
        try
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                Address = dto.Address,
                OrderDate = dto.OrderDate,
                ProductsList = dto.ProductsList
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrdersId }, new OrderDTO
            {
                OrdersId = order.OrdersId,
                Address = order.Address,
                OrderDate = order.OrderDate,
                ProductsList = order.ProductsList
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST for customer form
    [HttpPost("customerForm")]
    public async Task<ActionResult<OrderDTO>> CreateCustomerOrder(OrderCreateDTO dto)
    {
        try
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                Address = dto.Address,
                OrderDate = dto.OrderDate,
                ProductsList = dto.ProductsList
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrdersId }, new OrderDTO
            {
                OrdersId = order.OrdersId,
                Address = order.Address,
                OrderDate = order.OrderDate,
                ProductsList = order.ProductsList
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    public class OrderCreateDTO
    {
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ProductsList { get; set; }
    }

    public class OrderDTO
    {
        public int OrdersId { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductsList { get; set; }
        public CustomerDTO Customer { get; set; }
    }

    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
