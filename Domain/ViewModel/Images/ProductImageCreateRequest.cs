using Microsoft.AspNetCore.Http;

namespace Domain.ViewModel.Images
{
    public class ProductImageCreateRequest /*: Base*/
    {
        public IFormFile ImageFile { get; set; }
    }
}