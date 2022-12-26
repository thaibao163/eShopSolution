using Domain.Common;
using Domain.ViewModel.Images;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Gender { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SizeID { get; set; }

        public int ColorID { get; set; }

        public List<Image> Images { get; set; }
    }
}