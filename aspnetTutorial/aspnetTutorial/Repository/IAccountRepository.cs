using aspnetTutorial.Data;
using aspnetTutorial.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace aspnetTutorial.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(UserModel user);
        Task Logout();
        Task<SignInResult> PasswordSignIn(UserSignInModel user);
        Task<IdentityResult> updateUser(User user);
        Task<IdentityResult> ConfirmEmail(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(User user);
        Task<User> GetUserByEmail(string email);
    }
}