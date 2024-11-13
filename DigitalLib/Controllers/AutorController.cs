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
        public IActionResult Create([Bind("Nome, DataNascimento, Nacionalidade")] Autor autor)
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = _context.Autor.Find(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id", "Nome", "DataNascimento", "Nacionalidade")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            try
            {
                _context.Update(autor);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Autor.Any(e => e.Id == autor.Id))
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

            var autor = _context.Autor.FirstOrDefault(e => e.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var autor = _context.Autor.FirstOrDefault(e => e.Id == id);
            if (autor != null)
            {
                _context.Remove(autor);
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

            var autor = _context.Autor.FirstOrDefault(i => i.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        private bool AutorExists(int id)
        {
            return _context.Autor.Any(e => e.Id == id);
        }
    }
}
