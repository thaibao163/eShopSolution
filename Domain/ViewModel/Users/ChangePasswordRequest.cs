using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.Users
{
    public class ChangePasswordRequest
    {
        [Display(Name = "CurrentPassword")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}