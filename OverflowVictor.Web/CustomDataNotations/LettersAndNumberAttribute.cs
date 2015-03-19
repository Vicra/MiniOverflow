using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class LettersAndNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value.ToString();
            var result = text.All(Char.IsLetterOrDigit);
            return !result ? new ValidationResult("Can only contain Letters and Numbers") : ValidationResult.Success;
        }
    }
}