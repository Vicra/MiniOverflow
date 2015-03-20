using System;
using System.ComponentModel.DataAnnotations;
using OverflowVictor.Web.CustomDataNotations;

namespace OverflowVictor.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        [Capital(ErrorMessage = "The password must be between 8 and 16 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [Vocal]
        [Number]
        [RepeatedLetters]
        [LettersAndNumber]
        [Capital(ErrorMessage = "The password must be between 8 and 16 characters")]
        public string ConfirmPassword { get; set; }

        


    }
}