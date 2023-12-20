namespace ReservationApp.Entities
{
    public class Service
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal CostPerDay { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
