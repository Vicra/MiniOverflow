using System.ComponentModel.DataAnnotations;

namespace OverflowVictor.Web.Models
{
    public class AccountLoginModel
    {
        public AccountLoginModel()
        {
            CaptchaActive = false;
        }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool CaptchaActive { get; set; }
        public int LoginAttempts { get; set; }

    }
}