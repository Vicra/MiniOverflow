using System.ComponentModel.DataAnnotations;
using OverflowVictor.Web.CustomDataNotations;

namespace OverflowVictor.Web.Models
{
    public class AccountRegisterModel 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Password(ErrorMessage = "Password must contain a capital letter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}