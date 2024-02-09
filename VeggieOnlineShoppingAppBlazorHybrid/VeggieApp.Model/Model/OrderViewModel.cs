namespace VeggieApp.Model.Model
{
    public class OrderViewModel
    {
        public Order Orders { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
