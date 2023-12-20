namespace ReservationApp.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public int Grade { get; set; }

        public virtual User User { get; set; }

        public virtual Hotel Hotel { get; set; }

        public string Text { get; set; }
    }
}
