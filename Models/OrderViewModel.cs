using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models
{
    public class OrderViewModel
    {
        public Guid[] SelectedServices { get; set; }

        public List<SelectListItem> Services { get; set; } = new List<SelectListItem>();

        public Guid SelectedRoom { get; set; }

        public List<SelectListItem> Rooms { get; set; } = new List<SelectListItem>();

        [BindProperty, DataType(DataType.Date)]
        public DateTime? BeginDateTime { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime? EndDateTime { get; set; }
    }
}
