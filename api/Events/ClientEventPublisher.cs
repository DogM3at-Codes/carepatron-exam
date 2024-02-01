using api.Data;
using api.Models;

namespace api.Events
{
    public class ClientEventPublisher
    {
        public event EventHandler<ClientEventArgs>? RaiseClientEvent;

        public void NewClientEvent(ClientEvent client)
        {
            OnRaiseClientEvent(new ClientEventArgs(client));
        }

        protected virtual void OnRaiseClientEvent(ClientEventArgs e)
        {
            RaiseClientEvent!?.Invoke(this, e);
        }
    }
}
