using Microsoft.AspNetCore.Http;

namespace Domain.ViewModel.Images
{
    public class ProductCreateRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}