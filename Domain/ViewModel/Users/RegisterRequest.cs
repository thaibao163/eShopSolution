using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class RegisterRequest
    {
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}