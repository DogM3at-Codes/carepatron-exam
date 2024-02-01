using api.Models;

namespace api.Repositories
{
    public interface IClientRepository
    {
        Task<Client[]> Get();
        Task<Client[]> SearchClient(string name);
        Task Create(Client client);
        Task Update(string id, Client client);
        Task<bool> ValidateEmailIfUpdated(string id, string email);
    }
}
