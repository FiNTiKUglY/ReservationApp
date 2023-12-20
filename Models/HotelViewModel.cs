using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationApp.Entities;

namespace ReservationApp.Models
{
    public class HotelViewModel
    {
        public Hotel? Hotel { get; set; }

        public int SelectedGrade { get; set; }

        public List<SelectListItem> Grades { get; set; } = new List<SelectListItem>();

        public string Text { get; set; }
    }
}
