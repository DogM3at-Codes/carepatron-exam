using api.Models;

namespace api.Services
{
    public interface IClientService
    {
        Task<Client[]> Get();
        Task<IResult> SearchClient(string name);
        Task<IResult> Create(Client client);
        Task<IResult> Update(string id, Client client);
    }
}
