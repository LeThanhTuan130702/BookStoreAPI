using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> usermanger;
        private readonly SignInManager<ApplicationUsers> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager,
            SignInManager<ApplicationUsers> signInManager,
            IConfiguration configuration,RoleManager<IdentityRole> 
            roleManager) { 
            this.usermanger= userManager;
            this.signInManager= signInManager;
            this.configuration= configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(SigninModel model)
        {
            var user=await usermanger.FindByEmailAsync(model.Email);
            var passwordValid = await usermanger.CheckPasswordAsync(user,model.password);

            if (user == null||!passwordValid)
            {
                return string.Empty;
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.password,false,false);
            if (!result.Succeeded) { 
             return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var userRoles = await usermanger.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }    

            var authenKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey,SecurityAlgorithms.HmacSha256)


                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUsers
            {
                FirsName= model.FirsName,
                LastName= model.LastName,
                Email= model.Email,
                UserName=model.Email
            };
            var result= await usermanger.CreateAsync(user,model.password);
            if (result.Succeeded)
            {
                //check
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                } 
                await usermanger.AddToRoleAsync(user, AppRole.Customer);
                    
            }
            return result;
        }
    }
}
