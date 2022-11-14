using aspnetTutorial.Models;
using System.Threading.Tasks;

namespace aspnetTutorial.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendConfirmationEmail(UserEmailOptions userEmailOptions);
    }
}