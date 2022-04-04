using EXresturant.Data;
using EXresturant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EXresturant.Controllers
{
    public class ReserveringsController : Controller
    {
        private readonly ApplicationDbContext db;


        [TempData]
        public string StatusMessage { get; set; }
        public ReserveringsController(ApplicationDbContext db)
        {
            this.db = db;
        }



        // GET:
        [HttpGet]
        public async Task<IActionResult> Index()
        {



            var reserverings = await db.Reserverings.ToListAsync();
            return View(reserverings);
        }
        public async Task<IActionResult> check2(int id, string button1, Reservering reservering)
        {
            reservering = await db.Reserverings.FindAsync(id);

            reservering.IsGekomen = true;
            db.Update(reservering);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public async Task<IActionResult> Tafel(int id)

        //{
        //   var  Tafelnummer = db.Reserverings.Find(id);


        //    //return View(await db.Reserverings.ToListAsync());

        //    var reserverings = await db.Reserverings.ToListAsync();
        //    return View(Tafelnummer);
        //}

        // GET: /Create
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }



        // POST: /Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Id,Name,Tafelnummer,PhoneNummer,Email,Aantalpersoon,Datum,BeginTijd")] Reservering reservering)
        {

            



            if (ModelState.IsValid)
            {
                var isTableAlreadyExists = db.Reserverings.Any(m => m.Tafelnummer == reservering.Tafelnummer);
                var isTimeAlreadyExists = db.Reserverings.Any(m => m.Datum == reservering.Datum);
                var isBeginTijdAlreadyExists = db.Reserverings.Any(m => m.BeginTijd == reservering.BeginTijd);
                if (isTableAlreadyExists && isTimeAlreadyExists && isBeginTijdAlreadyExists)
                {
                    ModelState.AddModelError("Tafelnummer", "Tafel is already gereserveerd op dit tijd");
                    return View(reservering);
                }
              
                    //Reservering newreservering = new Reservering();
                    //newreservering.Tafelnummer = reservering.Tafelnummer;
                    db.Reserverings.Add(reservering);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

            }
            return View(reservering);
        }


        // GET: /Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservering = await db.Reserverings.FindAsync(id);

            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }


        // POST: /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Tafelnummer,PhoneNummer,Email,Aantalpersoon,Datum,BeginTijd,IsGekomen")] Reservering reservering)
        {
            if (id != reservering.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {


                    db.Reserverings.Update(reservering);
                    await db.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveringExists(reservering.Id))
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
            return View(reservering);
        }



        // GET: /Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservering = await db.Reserverings.FindAsync(id);

            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Reservering reservering)
        {

            db.Reserverings.Remove(reservering);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: /Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var reservering = await db.Reserverings.FindAsync(id);

            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }



        private bool ReserveringExists(int id)
        {
            return db.Reserverings.Any(e => e.Id == id);
        }




    }
}
