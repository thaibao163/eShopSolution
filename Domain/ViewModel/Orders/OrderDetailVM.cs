namespace Domain.ViewModel.Orders
{
    public class OrderDetailVM : Base
    {
        public string UserName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}