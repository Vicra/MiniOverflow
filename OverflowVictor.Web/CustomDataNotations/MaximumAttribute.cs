using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class MaximumAttribute:ValidationAttribute
    {
        private int _max;
        public MaximumAttribute(int max)
        {
            this._max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value.ToString();
            if(text.Length>_max)
                return new ValidationResult("Must contain less than "+_max);
            return ValidationResult.Success;
        }
    }
}