using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class RepeatedLettersAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value.ToString().ToCharArray();
            for (var i = 1; i < text.Length; i++)
            {
                if(text[i-1] == text[i])
                    return new ValidationResult("Cannot contain repeted letters");
            }
            return ValidationResult.Success;
        }
    }
}