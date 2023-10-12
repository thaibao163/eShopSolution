using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ForgerPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}