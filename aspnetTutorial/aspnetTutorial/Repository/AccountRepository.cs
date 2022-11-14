using aspnetTutorial.Data;
using aspnetTutorial.Models;
using aspnetTutorial.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager,
            IEmailService emailService,SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> CreateUser(UserModel user)
        {
            var newUser = new User
            {
                Email = user.Email,
                UserName = user.Email,
                LastName = user.LastName,
                Name = user.Name,
                isNew = true,

            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(newUser);
            }
            return result;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task GenerateEmailConfirmationTokenAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmation(user, token);
            }
        }
        public async Task<IdentityResult> ConfirmEmail(string uid,string token)
        {
           return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }
        private async Task SendEmailConfirmation(User user,string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
               {
                   new KeyValuePair<string,string>("{{UserName}}",user.Name),
                   new KeyValuePair<string,string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))
               }
            };
            await _emailService.SendConfirmationEmail(options);
        }
        public async Task<SignInResult> PasswordSignIn(UserSignInModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<IdentityResult> updateUser(User user)
        {

            return await _userManager.UpdateAsync(user);
        }
    }
}
