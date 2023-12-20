namespace ReservationApp.Entities
{
    public class Holding
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string WebSite { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public virtual User Moderator { get; set; }
    }
}
