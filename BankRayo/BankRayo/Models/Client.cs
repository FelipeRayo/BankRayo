﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankRayo.Models
{
    [Serializable]
    public class Client : Person
    {
        public int ClientId { get; set; }

        public string Password { get; set; }

        public bool State { get; set; }
    }
}
