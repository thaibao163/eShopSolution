namespace Domain.ViewModel.Orders
{
    public class OrderDetailVM : Base
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}