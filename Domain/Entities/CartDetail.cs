using Domain.Common;

namespace Domain.Entities
{
    public class CartDetail : BaseEntity
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}