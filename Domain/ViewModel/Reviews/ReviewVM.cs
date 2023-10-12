namespace Domain.ViewModel.Reviews
{
    public class ReviewVM : Base
    {
        public string FullName { get; set; }
        public string ProductName { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}