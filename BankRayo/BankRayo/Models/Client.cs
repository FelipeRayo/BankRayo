using System.ComponentModel.DataAnnotations;

namespace BankRayo.Models
{
    [Serializable]
    public class Client : Person
    {
        [Key]
        public int ClientId { get; set; }

        public int Id { get; set; }

        public string Password { get; set; }

        public bool State { get; set; }
    }
}
