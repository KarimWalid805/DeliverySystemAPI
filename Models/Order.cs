using System.Text.Json.Serialization;

namespace DeliverySystemAPI.Models
{
    public class Order
    {

        public int OrdersId { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductsList { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        

    }
}
