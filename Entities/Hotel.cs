namespace ReservationApp.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public virtual Region Region { get; set; }

        public bool IsPremium { get; set; }

        public virtual Holding Owner { get; set; }

        public virtual List<Room> Rooms { get; set; } = new List<Room>();

        public virtual List<Tag> Tags { get; set; } = new List<Tag>();

        public virtual List<Service> Services { get; set; } = new List<Service>();

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
