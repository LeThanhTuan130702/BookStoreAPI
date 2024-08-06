using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model
{
    public class SignUpModel
    {
        [Required]
        public string FirsName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
