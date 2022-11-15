using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Reviews
{
    public class ReviewVM :Base
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
