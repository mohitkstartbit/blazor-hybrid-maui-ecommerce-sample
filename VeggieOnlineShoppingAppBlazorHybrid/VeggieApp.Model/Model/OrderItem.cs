using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VeggieApp.Model.Model
{
    public class OrderItem
    {
        [Key]
        public int Itemid { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order? Orders { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("product_id")]
        [JsonIgnore]
        public Product.Product? Products { get; set; }
        public int product_id { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public decimal list_price { get; set; }
    }
}
