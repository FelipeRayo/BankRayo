using System.ComponentModel.DataAnnotations;

namespace BankRayo.Models
{
    [Serializable]
    public class Account
    {
        [Key]
        public int Number { get; set; }

        public string Type { get; set; }

        public float InitialBalance { get; set; }

        public bool State { get; set; }
    }
}
