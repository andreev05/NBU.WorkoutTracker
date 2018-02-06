using System.Threading.Tasks;

namespace NBU.WorkoutTracker.Core.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
