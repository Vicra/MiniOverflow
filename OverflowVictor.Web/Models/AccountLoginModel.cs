using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.Models
{
    public class AccountLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}