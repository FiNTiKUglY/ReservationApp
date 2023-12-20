using ReservationApp.Entities;

namespace ReservationApp.Models
{
    public class ProfileViewModel
    {
        public List<OrderInfoViewModel> Orders { get; set; } = new List<OrderInfoViewModel>();
    }
}
