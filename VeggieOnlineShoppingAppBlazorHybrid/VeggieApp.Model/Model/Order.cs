using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace VeggieApp.Model.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [ForeignKey("UserId")]
        //[JsonIgnore]
        public IdentityUser? User { get; set; }
        public string UserId { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem>? Items { get; set; }
    }
}
