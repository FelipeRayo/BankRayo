namespace BankRayo.Entities.BusinessEntities
{
    [Serializable]
    public class Person
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string NumberPhone { get; set; }

    }
}
