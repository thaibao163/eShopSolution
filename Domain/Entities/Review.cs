using Domain.Common;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public string Content { get; set; }
    }
}