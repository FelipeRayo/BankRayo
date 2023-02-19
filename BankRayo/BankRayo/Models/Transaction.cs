using System.ComponentModel.DataAnnotations;

namespace BankRayo.Models
{
    [Serializable]
    public class Transactions
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }

        public float Vallue { get; set; }

        public float Balance { get; set; }
    }
}
