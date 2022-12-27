namespace Domain.ViewModel.Images
{
    public class ProductImageViewModel :Base
    {
        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public long FileSize { get; set; }
    }
}