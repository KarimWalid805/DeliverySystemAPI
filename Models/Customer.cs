using System.Text.Json.Serialization;

namespace DeliverySystemAPI.Models;
public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }

    public ICollection<Order> Orders { get; set; }
}