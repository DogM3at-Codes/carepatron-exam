using api.Models;

namespace api.Events
{
    public class ClientEventArgs : EventArgs
    {
        public ClientEvent ClientEvent { get; set; }

        public ClientEventArgs(ClientEvent clientEvent)
        {
            this.ClientEvent = clientEvent;
        }

    }
}
