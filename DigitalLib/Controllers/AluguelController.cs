using DigitalLib.Data;
using DigitalLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalLib.Controllers
{
    public class AluguelController : Controller
    {
        private readonly BibliotecaDigitalContext _context;

        public AluguelController(BibliotecaDigitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aluguelContext = _context.Aluguel.Include(ap => ap.Livro).Include(ap => ap.Cliente);
            return View(aluguelContext.ToList());
        }
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo");
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DataEmprestimo", "DataDevolucao", "LivroId", "ClienteId")] Aluguel aluguel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo", null);
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", null);
                return View(aluguel);
            }
            if (aluguel == null)
            {
                return NotFound();
            }

            if (aluguel.DataDevolucao < aluguel.DataEmprestimo)
            {
                ModelState.AddModelError("DataDevolucao", "A data de devolução não pode ser antes da data de empréstimo.");
                ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo", null);
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", null);
                return View(aluguel);
            }

            _context.Add(aluguel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var aluguel = _context.Aluguel.Find(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            
            ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo", null);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", null);
            return View(aluguel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id", "DataEmprestimo", "DataDevolucao", "LivroId", "ClienteId")] Aluguel aluguel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo", null);
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", null);
                return View(aluguel);
            }
            if (aluguel == null)
            {
                return NotFound();
            }
            if (aluguel.DataDevolucao < aluguel.DataEmprestimo)
            {
                ViewData["LivroId"] = new SelectList(_context.Livro, "Id", "Titulo", null);
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", null);
                return View(aluguel);
            }
            try
            {
                _context.Update(aluguel);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Aluguel.Any(e => e.Id == aluguel.Id))
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

            var aluguel = _context.Aluguel.Include(ap => ap.Livro).Include(ap => ap.Cliente).FirstOrDefault(a => a.Id == id);
            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var aluguel = _context.Aluguel.Include(ap => ap.Livro).Include(ap => ap.Cliente).FirstOrDefault(a => a.Id == id);
            if (aluguel != null)
            {
                _context.Remove(aluguel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Details(int id)
        {
            var aluguel = _context.Aluguel.Include(ap => ap.Cliente).Include(ap => ap.Livro).FirstOrDefault(m => m.Id == id);

            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }
    }
}
