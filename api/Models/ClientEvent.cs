namespace api.Models
{
    public class ClientEvent
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; } = "";
        public string ClientFirstName { get; set; } = "";
        public string ClientLastName { get; set; } = "";    
        public DateTime DateCreated { get; set; }
    }
}
