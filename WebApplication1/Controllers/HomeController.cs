using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Pronia.DataAccesLayer;
using Pronia.Models;
using Pronia.ViewModels.Categories;
using Pronia.ViewModels.Sliders;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProniaContext _context;

        public  HomeController(ProniaContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var data = await _context.Sliders.Select(s => new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Subtitle = s.Subtitle,
                Title = s.Title
            }).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Contact() 
        {
            return View();

        }
        public async Task<IActionResult> AboutUs()
        {
            return View();

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            var cat = await _context.Categories.FindAsync(id);

            if (cat == null) return NotFound();

            _context.Categories.Remove(cat);

            await _context.SaveChangesAsync();

            return Content(cat.Name);
        }

           
        

        

       
    }
}
