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
    public class BestellingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public BonregelsAndBestellengViewModel BonregelsVM { get; set; }
        public BestellingsController(ApplicationDbContext context)
        {

            _context = context;
            BonregelsVM = new BonregelsAndBestellengViewModel()
            {
                BonBewaren = new BonBewaren(),
                //OrderslingsList = _context.Orders.ToList(),
                BestellingItemslingsList = _context.BestellingItems.ToList(),
                ProductsList = _context.Products.ToList(),
                ReserveringsList = _context.Reserverings.ToList()

            };
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bestellings.Include(t => t.Items).ToListAsync());
        }


        public async Task<IActionResult> Keuken()
        {
            //ViewBag.Tafelnummer = _context.Orders.Find(id).Reservering.Tafelnummer;
            //ViewBag.OrderId = _context.Orders.Find(id).Id;
            var applicationDbContext = _context.BestellingItems.Include(b => b.Bestelling);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.BestellingItems.Include(t => t.Bestelling)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.ss = _context.Reserverings.ToListAsync();
            //ViewBag.Menukaart = _context.Menukaarts.ToListAsync();

            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
            ViewData["ReserveringId"] = new SelectList(_context.Reserverings.Where(m => m.IsGekomen == true).Where(m => m.Datum == @DateTime.UtcNow.Date), "Tafelnummer", "Tafelnummer");
            var model = new Bestelling();
            model.Items.Add(new BestellingItem());
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tafelnummer,IsGereed,Items,NumberOfItems")] Bestelling bestelling)
        {
            if (bestelling.NumberOfItems == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    bestelling.DatumBesteld = DateTime.UtcNow;

                    _context.Add(bestelling);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(bestelling);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBestellingItem([Bind("Items")] Bestelling bestelling)
        {

            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            bestelling.Items.Add(new BestellingItem());
            return PartialView("BestellingItem", bestelling);


        }


        // GET: /GetProducts
        [HttpGet]
        public async Task<IActionResult> GetProducts(int id)
        {
            List<Product> products = new List<Product>();
            products = await _context.Products.Where(m => m.MenukaartId == id).ToListAsync();

            return Json(new SelectList(products, "Id", "Name"));
        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
            ViewData["ReserveringId"] = new SelectList(_context.Reserverings.Where(m => m.IsGekomen == true).Where(m => m.Datum == @DateTime.UtcNow.Date), "Tafelnummer", "Tafelnummer");
            var order = await _context.Bestellings.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReserveringId,Tafelnummer,IsGereed,DatumBesteld,ProductId,Aantal")] Bestelling bestelling, BestellingItem bestellingItem)
        {
            if (id != bestelling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bestelling.DatumBesteld = DateTime.UtcNow;
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                    
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(bestelling.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bestelling);
        }


       
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellings.Include(t => t.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestellings.Include(t => t.Items).FirstOrDefaultAsync(t => t.Id == id);
            _context.Bestellings.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Bestellings.Any(e => e.Id == id);
        }
    }
}
