namespace BankRayo.Entities.BusinessEntities
{
    [Serializable]
    public class Client : Person
    {
        public int ClientId { get; set; }

        public string Password { get; set; }

        public bool State { get; set; }
    }
}
