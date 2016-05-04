using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Intranet.Models;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace Intranet.Controllers
{
    public class ReclamacaoController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public ReclamacaoController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
            ViewData["provinciaId"] = new SelectList(_context.Provincia, "id", "descricao");
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome");
            return View();
        }

        // POST: Reclamacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reclamacao reclamacao, ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _context.Reclamacao.Add(reclamacao);
                reclamacao.tipoIdentificacao = "BI";
                _context.SaveChanges();

                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                uploads = Path.Combine(uploads, "Hotel");
                uploads = Path.Combine(uploads, reclamacao.hotelId.ToString());
                uploads = Path.Combine(uploads, reclamacao.id.ToString());

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        await file.SaveAsAsync(Path.Combine(uploads, fileName));

                        FileDescription f = new FileDescription()
                        {
                            ContentType = file.ContentType,
                            Description = "Avatar",
                            FileName = fileName,
                            CreatedTimestamp = DateTime.Now,
                            UpdatedTimestamp = DateTime.Now,
                        };

                        _context.FileDescription.Add(f);
                        _context.SaveChanges();
                        reclamacao.fileId = f.Id;

                        break;

                        
                    }

                }

                return RedirectToAction("Index");
            }
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome", reclamacao.hotelId);
            ViewData["provinciaId"] = new SelectList(_context.Provincia, "id", "descricao");
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
