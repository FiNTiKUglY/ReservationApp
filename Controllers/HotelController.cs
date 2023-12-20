using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using ReservationApp.Entities;
using ReservationApp.Models;
using System.Linq;

namespace ReservationApp.Controllers
{
    public class HotelController : Controller
    {
        ApplicationContext _context;
		private readonly UserManager<User> _userManager;
		public HotelController(ApplicationContext context, UserManager<User> userManager) 
        { 
            _context = context;
			_userManager = userManager;
		}

        [Route("/")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            MainPageViewModel model = new MainPageViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var hotels = await _context.Hotels.ToListAsync();
            List<Order> orders = new List<Order>();
            if (user != null)
            {
                orders = await _context.Orders.Where(o => o.User == user).ToListAsync();
            }

            foreach (var h in hotels)
			{
                int coeff = 0;
                foreach (var order in orders)
                {
                    foreach (var tag in order.Room.Hotel.Tags)
                    {
                        if (order.Room.Hotel != h) continue;
                        if (h.Tags.Contains(tag)) coeff++;
                    }
                }
                var reviews = await _context.Reviews.Where(r => r.Hotel == h).ToListAsync();
				string grade;
				if (reviews.Count != 0)
				{
					grade = (reviews.Sum(g => g.Grade) / (float)reviews.Count).ToString("0.0") + "/5";
				}
				else
				{
					grade = "Нет отзывов";
				}
                model.Hotels.Add(new HotelInfoViewModel { Hotel = h, Grade = grade, Coeff = coeff });
			}
            model.Hotels = model.Hotels.OrderByDescending(h => h.Coeff).ToList();
            var tags = await _context.Tags.ToListAsync();
            
            foreach (var tag in tags) 
            {
                model.Tags.Add(new CheckBoxItemViewModel { Tag = tag });
            }
            var regions = await _context.Regions.ToListAsync();
            model.Regions.Add(new SelectListItem { Text = $"", Value = "Все" });
			foreach (var region in regions)
			{
				model.Regions.Add(new SelectListItem { Text = $"{region.City}, {region.Country}", Value = region.Id.ToString() });
			}
			return View(model);
        }

		[Route("/")]
		[HttpPost]
		public async Task<ActionResult> Index(MainPageViewModel model)
		{
			var regions = await _context.Regions.ToListAsync();
			var tags = await _context.Tags.ToListAsync();
			for (int i = 0; i < tags.Count; i++)
			{
                model.Tags[i].Tag = tags[i];
			}
			model.Regions.Add(new SelectListItem { Text = $"", Value = "Все" });
			foreach (var region in regions)
			{
				model.Regions.Add(new SelectListItem { Text = $"{region.City}, {region.Country}", Value = region.Id.ToString() });
			}
            var selectedTags = model.Tags.Where(m => m.Checked == true).Select(m => m.Tag).ToList();
            var preModel = new List<Hotel>();
            if (model.SelectedRegion != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                preModel = await _context.Hotels.Where(h => h.Region.Id == model.SelectedRegion).ToListAsync();
			}
            else
            {
                preModel = await _context.Hotels.ToListAsync();
			}
            preModel = preModel.Where(h => h.Tags.Intersect(selectedTags).ToList().Count == selectedTags.Count).ToList();
            foreach (var h in preModel)
            {
                var reviews = await _context.Reviews.Where(r => r.Hotel == h).ToListAsync();
				string grade;
				if (reviews.Count != 0)
				{
					grade = (reviews.Sum(g => g.Grade) / (float)reviews.Count).ToString("0.0") + "/5";
				}
				else
				{
					grade = "Нет отзывов";
				}
				model.Hotels.Add(new HotelInfoViewModel { Hotel = h, Grade = grade });
            }
			return View(model);
		}

		[Route("hotel/{id}")]
        [HttpGet]
        public async Task<ActionResult> Hotel(Guid id)
        {
            var model = new HotelViewModel();
            for (int i = 1; i <= 5; i++)
            {
                model.Grades.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            model.Hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            return View(model);
        }

        [Authorize]
        [Route("hotel/{id}")]
        [HttpPost]
        public async Task<ActionResult> Hotel(HotelViewModel model, Guid id)
        {
            model.Hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            for (int i = 1; i <= 5; i++)
            {
                model.Grades.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            if (model.Text == null)
            {
                return View(model);
            }
            await _context.Reviews.AddAsync(new Review
            {
                Grade = model.SelectedGrade,
                Text = model.Text,
                Hotel = model.Hotel,
                User = await _userManager.GetUserAsync(HttpContext.User),
                Id = Guid.NewGuid()
            });
            await _context.SaveChangesAsync();  
            return View(model);
        }

        [Authorize]
        [Route("hotel/{id}/order")]
        [HttpGet]
        public async Task<ActionResult> Order(Guid id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            var services = hotel.Services;
            var rooms = hotel.Rooms;
            var model = new OrderViewModel();
            foreach (var service in services)
            {
                model.Services.Add(new SelectListItem { Text = $"{service.Title}, {service.CostPerDay}$ в день", Value = service.Id.ToString() });
            }
            foreach (var room in rooms)
            {
                model.Rooms.Add(new SelectListItem { Text = $"{room.Number} {room.Description}, {room.AdultAmount} взрослых, {room.KidAmount} детей, {room.CostPerDay}$ в день", Value = room.Id.ToString() });
            }
            return View(model);
        }

        [Route("hotel/{id}/order")]
        [HttpPost]
        public async Task<ActionResult> Order(OrderViewModel model, Guid id)
        {
            var services = await _context.Services.Where(s => model.SelectedServices.Contains(s.Id)).ToListAsync();
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == model.SelectedRoom);
			if (model.BeginDateTime.HasValue && model.EndDateTime.HasValue && model.BeginDateTime.Value <= model.EndDateTime.Value)
            {
                await _context.Orders.AddAsync(new Order
                {
                    Id = Guid.NewGuid(),
                    Room = room,
                    BeginDate = model.BeginDateTime.Value,
                    EndDate = model.EndDateTime.Value,
                    Services = services,
                    User = await _userManager.GetUserAsync(HttpContext.User),
                    Cost = (services.Sum(s => s.CostPerDay) + room.CostPerDay) * (model.EndDateTime.Value - model.BeginDateTime.Value).Days
				});
			}
            else
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
                services = hotel.Services;
                var rooms = hotel.Rooms;
                model = new OrderViewModel();
                foreach (var service in services)
                {
                    model.Services.Add(new SelectListItem { Text = $"{service.Title}, {service.CostPerDay}$ в день", Value = service.Id.ToString() });
                }
                foreach (var room1 in rooms)
                {
                    model.Rooms.Add(new SelectListItem { Text = $"{room1.Number} {room1.Description}, {room1.AdultAmount} взрослых, {room1.KidAmount} детей, {room1.CostPerDay}$ в день", Value = room1.Id.ToString() });
                }
                return View(model);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hotel");
        }
    }
}
    