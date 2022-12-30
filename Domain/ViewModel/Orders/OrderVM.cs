namespace Domain.ViewModel.Orders
{
    public class OrderVM : Base
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}