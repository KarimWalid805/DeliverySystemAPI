using System.ComponentModel.DataAnnotations;

namespace DeliverySystemAPI.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        [Required]
        public int OrdersId { get; set; }  

        public int? DriverId { get; set; } 

        public string? customersName { get; set; }
        public string? customersAddress { get; set; }

        public DateTime DeliveryDate { get; set; }

       
        public Order? Order { get; set; }
        public Driver? Driver { get; set; }
    }
}
