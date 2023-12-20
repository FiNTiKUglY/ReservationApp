using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationApp.Entities;

namespace ReservationApp.Models
{
    public class HotelAddViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid SelectedRegion {  get; set; }

        public List<SelectListItem> Regions { get; set; } = new List<SelectListItem>();

        public Guid[] SelectedTags { get; set; }

        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();
    }
}
