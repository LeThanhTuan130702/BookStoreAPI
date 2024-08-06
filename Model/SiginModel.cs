using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model
{
    public class SigninModel
    {
        [Required,EmailAddress]
        public string Email { get; set;} = null!;
        [Required]
        public string password { get; set;} = null!;
    }
}
