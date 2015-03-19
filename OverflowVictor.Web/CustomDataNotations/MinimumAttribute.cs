using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class MinimumAttribute : ValidationAttribute
    {
        int _min;
        public MinimumAttribute (int min)
        {
            this._min = min;
        }
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var text = value.ToString();
            if (text.Length < _min)
            {
                return new ValidationResult("Must contain more than " + _min);
            }
            return ValidationResult.Success;
        }

    }
}