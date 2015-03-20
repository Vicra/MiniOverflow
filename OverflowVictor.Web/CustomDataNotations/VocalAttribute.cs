using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class VocalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var vocals = new char[] {'a', 'e', 'i', 'o', 'u'};
                var text = value.ToString().ToLower();
                foreach (var vocal in vocals)
                {
                    if (text.Contains(vocal))
                        return ValidationResult.Success;
                }
                return new ValidationResult("Must contain a vocal");
            }
            return new ValidationResult("required field");
        }
    }
}