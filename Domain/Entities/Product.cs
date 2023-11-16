using Domain.Common;
using Domain.ViewModel.Categorys;
using Domain.ViewModel.Images;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Image Images { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public Capacity Capacity { get; set; }

        public int CapacityID { get; set; }

        public Color Color { get; set; }

        public int ColorID { get; set; }
    }
}