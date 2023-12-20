using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using ReservationApp.Entities;
using ReservationApp.Models;

namespace ReservationApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        ApplicationContext _context;
        public ProfileController(UserManager<User> userManager, ApplicationContext context) 
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("/profile")]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var orders = await _context.Orders.Where(o => o.User == user).ToListAsync();
            var model = new ProfileViewModel();
            foreach (var order in orders)
            {
                model.Orders.Add(new OrderInfoViewModel { Cost = order.Cost, HotelTitle = order.Room.Hotel.Title, OrderDates = $"{order.BeginDate.ToString("dd.MM.yyyy")} - {order.EndDate.ToString("dd.MM.yyyy")}" });
            }
            return View(model);
        }
    }
}
