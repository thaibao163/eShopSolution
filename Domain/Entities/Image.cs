using Domain.Common;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public long FileSize { get; set; }

        public Product Product { get; set; }
    }
}