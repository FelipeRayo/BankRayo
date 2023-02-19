﻿using System.ComponentModel.DataAnnotations;

namespace BankRayo.Models
{
    [Serializable]
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string NumberPhone { get; set; }
    }
}
