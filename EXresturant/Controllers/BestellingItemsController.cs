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
    public class BestellingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public BonregelsAndBestellengViewModel BonregelsVM { get; set; }
        public BestellingItemsController(ApplicationDbContext context)
        {
            _context = context;

            BonregelsVM = new BonregelsAndBestellengViewModel()
            {
                BonBewaren = new BonBewaren(),
                BestellingslingsList = _context.Bestellings.ToList(),
                BestellingItemslingsList = _context.BestellingItems.ToList(),
                ProductsList = _context.Products.ToList(),
                ReserveringsList = _context.Reserverings.ToList()

            };
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BestellingItems.Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Keuken()
        {
            //ViewBag.Tafelnummers = _context.Orders.Find(id).Tafelnummer;
            //ViewBag.OrderId = _context.Orders.Find(id).Id;
            //ViewBag.MenukaarId = _context.Menukaarts.Find(id);
            return View(await _context.Bestellings.Include(t => t.Items).ToListAsync());
        }
        // GET: OrderItems
        public async Task<IActionResult> BonPrinten()
        {
            return View(await _context.Bestellings.Include(t => t.Items).ToListAsync());
        }
        public async Task<IActionResult> Betaald()
        {

            return View(await _context.Bestellings.ToListAsync());
        }
        public async Task<IActionResult> check(int id, string button1, Bestelling bestelling)
        {
            bestelling = await _context.Bestellings.FindAsync(id);
            if (ModelState.IsValid)
            {
                bestelling.IsGereed = true;
                _context.Update(bestelling);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Keuken));
        }

        public async Task<IActionResult> check1(int id, string button1, Bestelling bestelling, BestellingItem bestellingItem)
        {
            bestelling = await _context.Bestellings.FindAsync(id);
            if (ModelState.IsValid)
            {
                bestelling.IsBetaald = true;
                _context.Update(bestelling);
                await _context.SaveChangesAsync();


             //Order order1 = _context.Orders.Find(id);
             //Product product = _context.Products.Find(id);
             //OrderItem orderItem1 = _context.OrderItems.Find(order1.Id);

             //   var x = new BonBewaren();
             //   x.OrderId = order1.Id;
             //   x.Tafelnummer = order1.Tafelnummer;
             //   x.ProductName = product.Name;
             //   //x.Prijs = orderItem.Product.Price;
             //   //x.Aantal = orderItem1.Order..Aantal;
             //   x.DatumBesteld = order1.DatumBesteld;
             //   x.IsBetaald = order1.IsBetaald;
             //   _context.Add(x);
             //   await _context.SaveChangesAsync();


            }
            return RedirectToAction(nameof(BonPrinten));
        }


        public async Task<IActionResult> Bon(int? id, BestellingItem bestellingItem)
        {
            //ViewBag.Tafel = _context.Bestellings.Find(id).ReserveringId;
            //ViewBag.Bon = _context.Bestellings.Find(id).BonId;
            var applicationDbContext = _context.BestellingItems.Include(b => b.Bestelling).Where(m => m.BestellingId == id);
            ViewBag.Tafelnummers = _context.Bestellings.Find(id).Tafelnummer;
            ViewBag.OrderId = _context.Bestellings.Find(id).Id;
            ViewBag.Status = _context.Bestellings.Find(id).IsBetaald;
            ViewBag.DatumBesteeld = _context.Bestellings.Find(id).DatumBesteld.ToString("dd MMMM yyyy HH:mm");
            return View(await applicationDbContext.ToListAsync());

            //var x = new BonBewaren();
            //x.OrderId = orderItem.OrderId;
            //x.Tafelnummer = orderItem.Order.Tafelnummer;
            ////x.ProductName = orderItem.Product.Name;
            //x.Prijs = orderItem.Product.Price;
            //x.Aantal = orderItem.Aantal;
            //x.DatumBesteld = orderItem.Order.DatumBesteld;
            //x.IsBetaald = orderItem.Order.IsBetaald;
            //_context.Add(x);
            //await _context.SaveChangesAsync();
        }
      

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var applicationDbContext = _context.BestellingItems.Include(b => b.Bestelling).Where(m => m.BestellingId == id);
            ViewBag.Tafelnummers = _context.Bestellings.Find(id).Tafelnummer;
            ViewBag.OrderId = _context.Bestellings.Find(id).Id;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Ober(int? id)
        {

            var applicationDbContext = _context.BestellingItems.Include(b => b.Bestelling).Where(m => m.BestellingId == id);
            ViewBag.Tafelnummers = _context.Bestellings.Find(id).Tafelnummer;
            ViewBag.OrderId = _context.Bestellings.Find(id).Id;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Aantal")] BestellingItem bestellingItem)
        {
            if (ModelState.IsValid)
            {
              
                    _context.Add(bestellingItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                

            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", bestellingItem.ProductId);
            return View(bestellingItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestellingItem = await _context.BestellingItems.FindAsync(id);
            if (bestellingItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", bestellingItem.ProductId);
            return View(bestellingItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Aantal")] BestellingItem bestellingItem)
        {
            if (id != bestellingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestellingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingItemExists(bestellingItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", bestellingItem.ProductId);
            return View(bestellingItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestellingItem = await _context.BestellingItems
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestellingItem == null)
            {
                return NotFound();
            }

            return View(bestellingItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestellingItem = await _context.BestellingItems.FindAsync(id);
            _context.BestellingItems.Remove(bestellingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingItemExists(int id)
        {
            return _context.BestellingItems.Any(e => e.Id == id);
        }
    }
}
