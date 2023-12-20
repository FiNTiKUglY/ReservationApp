using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationApp.Entities;

namespace ReservationApp.Models
{
    public class MainPageViewModel
    {
        public List<HotelInfoViewModel> Hotels { get; set; } = new List<HotelInfoViewModel>();

        public List<CheckBoxItemViewModel> Tags { get; set; } = new List<CheckBoxItemViewModel>();

		public Guid SelectedRegion { get; set; }

		public List<SelectListItem> Regions { get; set; } = new List<SelectListItem>();
	}
}
