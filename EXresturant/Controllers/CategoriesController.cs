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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        [TempData]
        public string StatusMessage { get; set; }

        public CategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int? id)
        {
            //var categories = await db.Categories.Include(c => c.Menukaart).ToListAsync();
            var applicationDbContext = new object();
            if (id > 0)
            {
                applicationDbContext = db.Categories
                    .Include(m => m.Menukaart)
                    .Where(m => m.Menukaart.Id == id)
                    .OrderBy(m => m.Id);
            }

            else
            {
                applicationDbContext = await db.Categories.Include(m => m.Menukaart).ToListAsync();
            }
            return View(applicationDbContext);

        }



        // GET: Categories/Create
        //public IActionResult Create()
        //{
        //    ViewData["MenukaartId"] = new SelectList(_context.Menukaarts, "Id", "Naam");
        //    return View();
        //}


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            CategorieAndMenukaartViewModel model = new CategorieAndMenukaartViewModel()
            {

                MenukaartsList = await db.Menukaarts.ToListAsync(),
                Categorie = new Categorie(),
                //SubCategoriesList = await db.Categories.OrderBy(m => m.Name).Select(m=>m.Name).Distinct().ToListAsync()
            };
            return View(model);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategorieAndMenukaartViewModel model)
        {



            if (ModelState.IsValid)
            {
                var doesExistSubCategory = await db.Categories.Include(m => m.Menukaart).Where(m => m.Menukaart.Id == model.Categorie.MenukaartId && m.Naam == model.Categorie.Naam).ToListAsync();

                if (doesExistSubCategory.Count() > 0)
                {
                    StatusMessage = "Eror : Naam Bestaat All Onder " + doesExistSubCategory.FirstOrDefault().Menukaart.Naam + " Menukaart";
                }
                else
                {

                    db.Categories.Add(model.Categorie);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }

            }

            //ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            CategorieAndMenukaartViewModel modelVM = new CategorieAndMenukaartViewModel()
            {

                MenukaartsList = await db.Menukaarts.ToListAsync(),
                Categorie = model.Categorie,
                //SubCategoriesList = await db.Categories.OrderBy(m => m.Name).Select(m => m.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);

        }

        // GET: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]

        // GET: /Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var categorie = await db.Categories.FindAsync(id);

            if (categorie == null)
            {
                return NotFound();
            }


            CategorieAndMenukaartViewModel model = new CategorieAndMenukaartViewModel()
            {

                MenukaartsList = await db.Menukaarts.ToListAsync(),
                Categorie = categorie
                //SubCategoriesList = await db.Categories.OrderBy(m => m.Name).Select(m=>m.Name).Distinct().ToListAsync()
            };
            return View(model);
        }


        // Post: /Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategorieAndMenukaartViewModel model)
        {


            if (ModelState.IsValid)
            {
                var doesExistSubCategory = await db.Categories.Include(m => m.Menukaart).Where(m => m.Menukaart.Id == model.Categorie.MenukaartId && m.Naam == model.Categorie.Naam && m.Id != model.Categorie.Id).ToListAsync();

                if (doesExistSubCategory.Count() > 0)
                {
                    StatusMessage = "Eror : Naam Bestaat All Onder " + doesExistSubCategory.FirstOrDefault().Menukaart.Naam + " Category";
                }
                else
                {

                    db.Categories.Update(model.Categorie);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }

            //ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            CategorieAndMenukaartViewModel modelVM = new CategorieAndMenukaartViewModel()
            {

                MenukaartsList = await db.Menukaarts.ToListAsync(),
                Categorie = model.Categorie,
                //SubCategoriesList = await db.Categories.OrderBy(m => m.Name).Select(m => m.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);

        }

        // GET: /GetSubCategories
        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            List<Categorie> subCategories = new List<Categorie>();
            subCategories = await db.Categories.Where(m => m.MenukaartId == id).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Naam"));
        }


        // GET: Categories/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var subCategory = db.Categories.Include(m => m.Menukaart).Where(m => m.Id == id).SingleOrDefault();
            //var subCategory = await db.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }



            return View(subCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(Categorie categorie)
        {

            db.Categories.Remove(categorie);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await db.Categories
                .Include(c => c.Menukaart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }
    }
}
