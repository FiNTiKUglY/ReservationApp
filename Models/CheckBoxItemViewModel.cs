using ReservationApp.Entities;

namespace ReservationApp.Models
{
	public class CheckBoxItemViewModel
	{
		public Tag? Tag { get; set; }

		public bool Checked { get; set; } = false;
	}
}
