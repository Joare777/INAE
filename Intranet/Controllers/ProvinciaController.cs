using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class ProvinciaController : Controller
    {
        private ApplicationDbContext _context;

        public ProvinciaController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Provincia
        public IActionResult Index()
        {
            return View(_context.Provincia);
        }

        // GET: Provincia/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provincia provincia = _context.Provincia.Single(m => m.id == id);
            if (provincia == null)
            {
                return HttpNotFound();
            }

            return View(provincia);
        }

        // GET: Provincia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provincia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                _context.Provincia.Add(provincia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provincia);
        }

        // GET: Provincia/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provincia provincia = _context.Provincia.Single(m => m.id == id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // POST: Provincia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                _context.Update(provincia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provincia);
        }

        // GET: Provincia/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provincia provincia = _context.Provincia.Single(m => m.id == id);
            if (provincia == null)
            {
                return HttpNotFound();
            }

            return View(provincia);
        }

        // POST: Provincia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Provincia provincia = _context.Provincia.Single(m => m.id == id);
            _context.Provincia.Remove(provincia);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
