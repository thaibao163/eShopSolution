namespace Domain.ViewModel.Cart
{
    public class CartVM
    {
        public int CartDetailId { get; set; }
        public int ProductId { get; set; }
        public int CarId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal ToTal { get; set; }
    }
}