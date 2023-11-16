﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 40;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value>MaxPageSize)?MaxPageSize:value;
        }

        public string? Sort { get;set; }
    }
}