using System.ComponentModel.DataAnnotations;

namespace BankRayo.Entities.Models
{
    [Serializable]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int NumberAccount { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public decimal Value { get; set; }

        public decimal Balance { get; set; }

        public bool State { get; set; }
    }
}
