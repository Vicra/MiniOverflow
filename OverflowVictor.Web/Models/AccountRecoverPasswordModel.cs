using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.Models
{
    public class AccountRecoverPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}