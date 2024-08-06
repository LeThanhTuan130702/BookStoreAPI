using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Data
{
    public class ApplicationUsers:IdentityUser
    {
        public string FirsName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
