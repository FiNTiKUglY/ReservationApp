namespace ReservationApp.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual List<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
