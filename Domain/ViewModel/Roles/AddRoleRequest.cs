using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Roles
{
    public class AddRoleRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}