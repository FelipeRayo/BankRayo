using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRayo.Entities.Models
{
   [Table("Person")]
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string NumberPhone { get; set; }

        public int ClientId { get; set; }

        public string Password { get; set; }

        public bool State { get; set; }

    }
}
