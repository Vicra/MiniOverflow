using System.ComponentModel.DataAnnotations;
using OverflowVictor.Web.CustomDataNotations;

namespace OverflowVictor.Web.Models
{
    public class AccountRegisterModel 
    {
        [Required]
        [Minimum(2)]
        [Maximum(50)]
        public string Name { get; set; }

        [Required]
        [Minimum(2)]
        [Maximum(50)]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Minimum(8)]
        [Maximum(16)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        [Capital(ErrorMessage = "Must contain a capital letter")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Minimum(8)]
        [Maximum(16)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        public string ConfirmPassword { get; set; }
    }
}