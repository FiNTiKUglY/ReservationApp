namespace ReservationApp.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public virtual Room Room { get; set; }

        public decimal Cost { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual List<Service> Services { get; set; } = new List<Service>();

        public virtual User User { get; set; }
    }
}
