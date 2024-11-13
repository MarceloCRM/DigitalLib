using Microsoft.AspNetCore.Mvc;
using DigitalLib.Models;
using DigitalLib.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalLib.Controllers
{
    public class AutorController : Controller
    {
        private readonly DigitalLibContext _context;

        public AutorController(DigitalLibContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var autor = _context.Autor.ToList();
            return View(autor);
        }

        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nome", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome")] Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }
            if (autor == null)
            {
                return NotFound();
            }
            _context.Add(autor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
