using Domain.Common;
using Domain.ViewModel.Images;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Gender { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public List<Image> Images { get; set; }
    }
}