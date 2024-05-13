using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DataAccesLayer;
using Pronia.Extensions;
using Pronia.ViewModels.Products;

namespace Pronia.Areas.Admin.Controllers;

[Area("Admin")]
    public class ProductController(ProniaContext _context,IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.Products.Select(p=>new GetProdactAdminVM
            {
                CostPrice = p.CostPrice,
                Discount = p.Discount,
                Id = p.Id,
                Name = p.Name,
                Raiting = p.Raiting,
                SellPrice = p.SellPrice,
                StockCount = p.StockCount,
                ImageUrl=p.IamgeUrl

            }).ToListAsync());
        }

           public async Task<IActionResult> Create()
           {
             return View();
           }
    [HttpPost]
    public async Task<IActionResult> Get(CreateProductVM data)
    {
        if (!data.ImageFile.IsValidType("image")) 
        {
            ModelState.AddModelError("ImageFile", "sekil formati yanlisdir!");
        }
        if(data.ImageFile.IsValidLength(20))
        {
            ModelState.AddModelError("ImageFile", "sekilin olcusu cox boyukdur");
        }
        if (!ModelState.IsValid)
        {
            return View();
        }
        string fileName = await data.ImageFile.SaveFileAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));
        await _context.Products.AddAsync(new Models.Product
        {
            CostPrice = data.CostPrice,
            CreatedTime = DateTime.Now,
            Discount = data.Discount,
            IamgeUrl = Path.Combine("imgs", "products", fileName),
            IsDeleted = false,
            Name = data.Name,
            Raiting = data.Raiting,
            SellPrice = data.SellPrice,
            StockCount = data.StockCount,
        });
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
 
     }

