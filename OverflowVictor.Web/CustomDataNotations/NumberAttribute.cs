using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class NumberAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var text = value.ToString();
                var hasNumber = text.Any(char.IsDigit);
                return hasNumber ? ValidationResult.Success : new ValidationResult("Must contain a number");
            }
            return new ValidationResult("required fiels");
        }
    }
}