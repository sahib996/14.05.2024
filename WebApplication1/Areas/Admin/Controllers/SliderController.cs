using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DataAccesLayer;
using Pronia.Migrations;
using Pronia.Models;
using Pronia.ViewModels.Sliders;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(ProniaContext _context) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var data = await _context.Sliders.Select(s=>new GetSliderVM
            {
                Discount=s.Discount,
                Id=s.Id,
                ImageUrl=s.ImageUrl,
                Subtitle=s.Subtitle,
                Title=s.Title
            }).ToListAsync(); 
            return View(data ?? new List<GetSliderVM>());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlidersVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Discount = vm.Discount,
                CreatedTime=DateTime.Now,
                ImageUrl=vm.ImageUrl,
                IsDeleted=false,
                Subtitle=vm.Subtitle,
                Title=vm.Title
        };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null ||id < 1) { return BadRequest(); }
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();

            UpdateSlidersVM sliderVm = new UpdateSlidersVM
            {
                Discount = slider.Discount,
                Subtitle = slider.Subtitle,
                Title = slider.Title,
                ImageUrl = slider.ImageUrl,

            };

            return View(sliderVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlidersVM sliderVM)
        {
        
            if (id == null || id < 1) { return BadRequest(); }
            Slider existed = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();
            existed.Title = sliderVM.Title;
            existed.Discount = sliderVM.Discount;
            existed.Subtitle = sliderVM.Subtitle;
            existed.ImageUrl = sliderVM.ImageUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id) 
        { 
            if (id == null || id < 1) return BadRequest(); 

            var dataDel = await _context.Sliders.FindAsync(id);
                 
            if (dataDel == null) return NotFound();

            _context.Sliders.Remove(dataDel); 

            await _context.SaveChangesAsync(); 

            return RedirectToAction(nameof(Index));
        }
    }
}
