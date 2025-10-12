using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystemAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; } = null!;
    }
}
