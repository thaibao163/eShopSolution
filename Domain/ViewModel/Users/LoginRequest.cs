using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class LoginRequest
    {
        public string Login { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}