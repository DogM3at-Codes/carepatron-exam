using api.Models;
using api.Repositories;
using System.Net.Mail;

namespace api.Services
{
    public sealed class ClientService : IClientService
    {
        private IClientRepository _clientRepository { get; set; }
        private IEmailService _emailService { get; set; }


        public ClientService(IClientRepository clientRepository, IEmailService emailService)
        {
            _clientRepository = clientRepository;
            _emailService = emailService;
        }

        public async Task<IResult> Create(Client client)
        {
            if (client == null)
            {
                return Results.BadRequest();
            }

            try
            {
                ValidateClientParameter(client);

                await _clientRepository.Create(client);
                await _emailService.SendEmail(client);
                return Results.Created($"/clients/{client.Id}", client);
            }
            catch (ArgumentException ae)
            {
                return Results.BadRequest(ae.Message);
            }
        }

        public Task<Client[]> Get()
        {
            return _clientRepository.Get();
        }

        public async Task<IResult> SearchClient(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Results.NotFound();
            }

            try
            {
                var clientList = await _clientRepository.SearchClient(name);

                return clientList.Length > 0 ? Results.Ok(clientList) : Results.NotFound();
            }
            catch (Exception e)
            {
                return Results.NotFound(e.Message);
            }
        }

        public async Task<IResult> Update(string id, Client client)
        {
            if (client == null)
            {
                return Results.BadRequest();
            }

            if (String.IsNullOrEmpty(id))
            {
                return Results.BadRequest();
            }

            try
            {
                ValidateClientParameter(client);

                if (await _clientRepository.ValidateEmailIfUpdated(id, client.Email))
                {
                    await _emailService.SendEmail(client);
                }

                await _clientRepository.Update(id, client);

                return Results.Ok($"Client {id} successfully updated");
            }
            catch (ArgumentException ae)
            {
                return Results.BadRequest(ae.Message);
            }
        }

        private static void ValidateClientParameter(Client client)
        {
            if (client.Id == null)
            {
                throw new ArgumentException("Id is required.");
            }

            if (client.FirstName == null)
            {
                throw new ArgumentException("FirstName is required.");
            }

            if (client.LastName == null)
            {
                throw new ArgumentException("Last Name is required.");
            }

            if (client.Email == null)
            {
                throw new ArgumentException("Email Address is required.");
            }

            if (!IsValidEmail(client.Email))
            {
                throw new ArgumentException("Invalid email address.");
            }

            if (client.PhoneNumber == null)
            {
                throw new ArgumentException("FirstName is required.");
            }
        }

        private static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
