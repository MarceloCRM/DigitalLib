using DigitalLib.Models;
using Microsoft.EntityFrameworkCore;
namespace DigitalLib.Data
{
    public class BibliotecaDigitalContext : DbContext
    {
        public BibliotecaDigitalContext(DbContextOptions<BibliotecaDigitalContext> options) : base(options) { }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Aluguel> Aluguel { get; set; }
    }
}
