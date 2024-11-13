using Microsoft.AspNetCore.Mvc;
using DigitalLib.Models;
using DigitalLib.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalLib.Controllers
{
    public class LivroController : Controller
    {
        private readonly DigitalLibContext _context;

        public LivroController(DigitalLibContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var livroContext = _context.Livro.Include(ap => ap.Autor);
            return View(livroContext.ToList());
        }

        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nome", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Titulo", "DataPublicacao", "Preco", "AutorId")] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nome", null);
                return View(livro);
            }
            if (livro == null)
            {
                return NotFound();
            }
            _context.Add(livro);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
