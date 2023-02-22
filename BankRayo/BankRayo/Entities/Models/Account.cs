using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRayo.Entities.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Number { get; set; }

        public string Type { get; set; }

        public decimal InitialBalance { get; set; }

        public bool State { get; set; }

        public int IdClient { get; set; }
    }
}
