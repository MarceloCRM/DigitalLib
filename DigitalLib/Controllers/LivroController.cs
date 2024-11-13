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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _context.Livro.Find(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nome", livro.AutorId);
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id", "Titulo", "DataPublicacao", "Preco", "AUtorId")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Nome", livro.AutorId);
                return View(livro);
            }

            try
            {
                _context.Update(livro);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Livro.Any(e => e.Id == livro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _context.Livro
                .Include(l => l.Autor) // Inclua o autor relacionado para garantir que os dados do autor sejam carregados
                .FirstOrDefault(e => e.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var livro = _context.Livro.Include(l => l.Autor).FirstOrDefault(i => i.Id == id);
            if (livro != null)
            {
                _context.Remove(livro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _context.Livro.Include(l => l.Autor).FirstOrDefault(i => i.Id == id);

            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.Id == id);
        }
    }
}
