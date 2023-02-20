using System.ComponentModel.DataAnnotations;

namespace BankRayo.Models
{
    [Serializable]
    public class Account
    {
        [Key]
        public int Number { get; set; }

        [Required]
        public string Type { get; set; }

        public decimal InitialBalance { get; set; }

        public bool State { get; set; }

        public int IdClient { get; set; }
    }
}
