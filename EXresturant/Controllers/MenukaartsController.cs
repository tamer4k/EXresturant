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
    public class MenukaartsController : Controller
    {
        private readonly ApplicationDbContext db;

        public MenukaartsController(ApplicationDbContext db)
        {
            this.db = db;
        }



        // GET: Menukaarts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Menukaarts.ToListAsync());
        }

        

        // GET: Menukaarts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menukaarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam")] Menukaart menukaart)
        {
            if (ModelState.IsValid)
            {
                db.Menukaarts.Add(menukaart);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menukaart);
        }

        // GET: Menukaarts/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menukaart = await db.Menukaarts.FindAsync(id);

            if (menukaart == null)
            {
                return NotFound();
            }

            return View(menukaart);
        }

        // POST: Menukaarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam")] Menukaart menukaart)
        {
            if (id != menukaart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Menukaarts.Update(menukaart);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenukaartExists(menukaart.Id))
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
            return View(menukaart);
        }


        // GET: Menukaarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menukaart = await db.Menukaarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menukaart == null)
            {
                return NotFound();
            }

            return View(menukaart);
        }


        // POST: Menukaarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menukaart = await db.Menukaarts.FindAsync(id);
            db.Menukaarts.Remove(menukaart);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenukaartExists(int id)
        {
            return db.Menukaarts.Any(e => e.Id == id);
        }




        // GET: Menukaarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menukaart = await db.Menukaarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menukaart == null)
            {
                return NotFound();
            }

            return View(menukaart);
        }


    }
}
