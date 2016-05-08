using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Intranet.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;

namespace Intranet.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.hoteis = _context.Hotel.Include(r => r.provincia).Include(r=>r.listReclamacoes); 
            ViewData["provinciaId"] = new SelectList(_context.Provincia, "id", "descricao");
            ViewData["hotelId"] = new SelectList(_context.Hotel, "id", "nome");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
