using BookStoreAPI.Model;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Repository
{
    public interface IAccountRepository
    {
        public  Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SigninModel model);
    }
}
