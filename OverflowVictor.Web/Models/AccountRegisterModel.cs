using System.ComponentModel.DataAnnotations;
using OverflowVictor.Web.CustomDataNotations;

namespace OverflowVictor.Web.Models
{
    public class AccountRegisterModel 
    {
        [Required]
        [StringLength(50,ErrorMessage = "eroor",MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "eroor", MinimumLength = 2)]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "eroor", MinimumLength = 8)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        [Capital(ErrorMessage = "Must contain a capital letter")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "eroor", MinimumLength = 8)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        [Capital(ErrorMessage = "Must contain a capital letter")]
        public string ConfirmPassword { get; set; }
    }
}