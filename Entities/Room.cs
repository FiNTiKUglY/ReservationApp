namespace ReservationApp.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public int AdultAmount { get; set; }

        public int KidAmount { get; set; }

        public decimal CostPerDay { get; set; }

        public string Description { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
