namespace Domain.ViewModel.Products
{
    public class ProductInformation : Base
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}