namespace Domain.ViewModel.Products
{
    public class ProductVM : Base
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}