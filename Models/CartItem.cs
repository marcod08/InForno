namespace InForno.Models
{
    public class CartItem
    {
        public int PizzaId { get; set; }
        public int DrinkId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
