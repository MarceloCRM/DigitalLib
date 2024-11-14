using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalLib.Data;
using DigitalLib.Models;

namespace DigitalLib.Controllers
{
    public class ClienteController : Controller
    {
        private readonly BibliotecaDigitalContext _context;

        public ClienteController(BibliotecaDigitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cliente = _context.Cliente.ToList();
            return View(cliente);
        }

        public IActionResult Create()
        {
            ViewData["Generos"] = new List<string> { "Masculino", "Feminino", "Prefiro não dizer" };
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Nome", "DataNascimento", "Genero")] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Generos"] = new List<string> { "Masculino", "Feminino", "Prefiro não dizer" };
                return View(cliente);
            }
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["Generos"] = new List<string> { "Masculino", "Feminino", "Prefiro não dizer" };
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(int? id, [Bind("Id", "Nome", "DataNascimento", "Genero")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Generos"] = new List<string> { "Masculino", "Feminino", "Prefiro não dizer" };
                return View(cliente);
            }

            try
            {
                _context.Update(cliente);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cliente.Any(e => e.Id == cliente.Id))
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

            var cliente = _context.Cliente.FirstOrDefault(e => e.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var cliente = _context.Cliente.FirstOrDefault(e => e.Id == id);
            if (cliente != null)
            {
                _context.Remove(cliente);
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

            var cliente = _context.Cliente.Include(c => c.Alugueis).ThenInclude(a => a.Livro).FirstOrDefault(m => m.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

    }
}
