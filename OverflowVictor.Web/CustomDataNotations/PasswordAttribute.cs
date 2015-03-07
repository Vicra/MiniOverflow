using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class PasswordAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var stringValue = value.ToString();
            var string2 = stringValue.ToLower();
            if (stringValue.Equals(string2))
                return false;
            return true;
        }
    }
}