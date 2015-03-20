using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace OverflowVictor.Web.CustomDataNotations
{
    public class CapitalAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value!=null)
            {
                var stringValue = (string) value;
                var string2 = stringValue.ToLower();
                if (stringValue.Equals(string2))
                    return false;
                return true;
            }
            return false;
        }
    }
}