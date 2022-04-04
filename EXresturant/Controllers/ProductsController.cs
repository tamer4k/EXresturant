using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EXresturant.Data;
using EXresturant.Models;

namespace EXresturant.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet]
        public IActionResult Index(int? id)
        {
            var applicationDbContext = new object();
            if (id > 0)
            {
                applicationDbContext = _context.Products
                   .Include(m => m.Categorie)
                   .Where(m => m.Categorie.MenukaartId == id)
                   .Include(m => m.Menukaart);

                ViewBag.Menukaarten = _context.Menukaarts.Find(id).Naam;
            }
            else
            {
                applicationDbContext = _context.Products
               .Include(m => m.Categorie)
               .Include(m => m.Menukaart);
         


            }
            return View( applicationDbContext);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categorie)
                .Include(p => p.Menukaart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create(int id)
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Naam");
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Image,MenukaartId,CategorieId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var isProductAlreadyExists = _context.Products.Any(m => m.Name == product.Name);
                if (isProductAlreadyExists)
                {
                    ModelState.AddModelError("Name", "Het product bestaat al");
                    ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Naam");
                    ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
                    return View(product);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Create));
                return RedirectToAction("Index", new { id = product.MenukaartId });


            }

            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Naam", product.CategorieId);
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam", product.MenukaartId);
            return View(product);


        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Naam", product.CategorieId);
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam", product.MenukaartId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Image,MenukaartId,CategorieId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", new { id = product.MenukaartId });
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Naam", product.CategorieId);
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam", product.MenukaartId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categorie)
                .Include(p => p.Menukaart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationDbContext = _context.Products.Include(p => p.Categorie).Where(p => p.Categorie.MenukaartId == id).Include(p => p.Menukaart);

            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = product.MenukaartId });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }



        // GET: /GetSubCategories
        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            List<Categorie> subCategories = new List<Categorie>();
            subCategories = await _context.Categories.Where(m => m.MenukaartId == id).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Naam"));
        }

    }
}
