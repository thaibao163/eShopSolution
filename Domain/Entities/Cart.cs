﻿using Domain.Common;

namespace Domain.Entities
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; }

        public bool Status { get; set; }
    }
}