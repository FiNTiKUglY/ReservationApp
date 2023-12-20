using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using ReservationApp.Entities;
using ReservationApp.Models;

namespace ReservationApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public ManagerController(ApplicationContext context, UserManager<User> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("/manage")]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var holding = await _context.Holdings.FirstOrDefaultAsync(h => h.Moderator == user);
            var hotels = await _context.Hotels.Where(h => h.Owner == holding).ToListAsync();
            return View(hotels);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("/manage/add")]
        public async Task<ActionResult> Add()
        {
            var tags = await _context.Tags.ToListAsync();
            var regions = await _context.Regions.ToListAsync();
            var model = new HotelAddViewModel();
            foreach (var region in regions)
            {
                model.Regions.Add(new SelectListItem { Text = $"{region.Country}, {region.City}", Value = region.Id.ToString() });
            }
            foreach (var tag in tags)
            {
                model.Tags.Add(new SelectListItem { Text = $"{tag.Title}", Value = tag.Id.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("/manage/add")]
        public async Task<ActionResult> Add(HotelAddViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var tags = await _context.Tags.Where(t => model.SelectedTags.Contains(t.Id)).ToListAsync();
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == model.SelectedRegion);
            await _context.Hotels.AddAsync(new Hotel
            {
                Id = Guid.NewGuid(),
                Tags = tags,
                Region = region,
                Owner = await _context.Holdings.FirstOrDefaultAsync(h => h.Moderator == user),
                Title = model.Title,
                Description = model.Description,
                IsPremium = false
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            var tags = await _context.Tags.ToListAsync();
            var regions = await _context.Regions.ToListAsync();
            var model = new HotelAddViewModel();
            model.Id = hotel.Id;
            model.Title = hotel.Title; 
            model.Description = hotel.Description;
            foreach (var region in regions)
            {
                model.Regions.Add(new SelectListItem { Text = $"{region.Country}, {region.City}", Value = region.Id.ToString() });
            }
            foreach (var tag in tags)
            {
                model.Tags.Add(new SelectListItem { Text = $"{tag.Title}", Value = tag.Id.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}")]
        public async Task<ActionResult> Edit(HotelAddViewModel model, Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var tags = await _context.Tags.Where(t => model.SelectedTags.Contains(t.Id)).ToListAsync();
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == model.SelectedRegion);
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            hotel.Region = region;
            hotel.Title = model.Title;
            hotel.Description = model.Description;
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}/add_room")]
        public async Task<ActionResult> AddRoom(Guid id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}/add_room")]
        public async Task<ActionResult> AddRoom(Room model, Guid id)
        {
            model.Id = Guid.NewGuid();
            model.Hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            await _context.Rooms.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}/add_service")]
        public async Task<ActionResult> AddService(Guid id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("/manage/{id}/add_service")]
        public async Task<ActionResult> AddService(Service model, Guid id)
        {
            model.Id = Guid.NewGuid();
            model.Hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            await _context.Services.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Manager");
        }
    }
}
