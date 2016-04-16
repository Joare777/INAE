using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class ReclamacaoController : Controller
    {
        private ApplicationDbContext _context;

        public ReclamacaoController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Reclamacao
        public IActionResult Index()
        {
            var applicationDbContext = _context.Reclamacao.Include(r => r.hotel);
            return View(applicationDbContext.ToList());
        }

        // GET: Reclamacao/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reclamacao reclamacao = _context.Reclamacao.Single(m => m.id == id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }

            return View(reclamacao);
        }

        // GET: Reclamacao/Create
        public IActionResult Create()
        {
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome");
            return View();
        }

        // POST: Reclamacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reclamacao reclamacao)
        {
            if (ModelState.IsValid)
            {
                _context.Reclamacao.Add(reclamacao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome", reclamacao.hotelId);
            return View(reclamacao);
        }

        // GET: Reclamacao/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reclamacao reclamacao = _context.Reclamacao.Single(m => m.id == id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome", reclamacao.hotelId);
            return View(reclamacao);
        }

        // POST: Reclamacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reclamacao reclamacao)
        {
            if (ModelState.IsValid)
            {
                _context.Update(reclamacao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "hotel", reclamacao.hotelId);
            return View(reclamacao);
        }

        // GET: Reclamacao/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Reclamacao reclamacao = _context.Reclamacao.Single(m => m.id == id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }

            return View(reclamacao);
        }

        // POST: Reclamacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Reclamacao reclamacao = _context.Reclamacao.Single(m => m.id == id);
            _context.Reclamacao.Remove(reclamacao);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
