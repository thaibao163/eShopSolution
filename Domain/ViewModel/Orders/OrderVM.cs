using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Orders
{
    public class OrderVM : Base
    {
        public string NameUser { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
