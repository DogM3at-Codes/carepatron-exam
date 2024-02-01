using api.Data;
using api.Models;

namespace api.Events
{
    public class ClientEventSubscriber
    {
        private readonly ClientEvent _clientEvent;

        public ClientEventSubscriber(ClientEvent clientEvent, ClientEventPublisher clientEventPublisher)
        {
            _clientEvent = clientEvent;

            clientEventPublisher.RaiseClientEvent += StoreNewClientInformationEvent;
        }

        private void StoreNewClientInformationEvent(object sender, ClientEventArgs e)
        {
            ClientDataEvent.ClientEventList.Add(e.ClientEvent);
        }
    }
}
