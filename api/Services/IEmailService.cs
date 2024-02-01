using api.Models;

namespace api.Services
{
    public interface IEmailService
    {
        Task<Task> SendEmail(Client client);
    }
}
