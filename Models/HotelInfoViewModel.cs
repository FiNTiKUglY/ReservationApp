using ReservationApp.Entities;

namespace ReservationApp.Models
{
	public class HotelInfoViewModel
	{
		public Hotel Hotel { get; set; }

		public string? Grade { get; set; }

		public int Coeff { get; set; }
	}
}
