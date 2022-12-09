using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public long FileSize { get; set; }

        public Product Product { get; set; }
    }
}
