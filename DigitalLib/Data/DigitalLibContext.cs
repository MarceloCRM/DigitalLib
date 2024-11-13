using DigitalLib.Models;
using Microsoft.EntityFrameworkCore;
namespace DigitalLib.Data
{
    public class DigitalLibContext : DbContext
    {
        public DigitalLibContext(DbContextOptions<DigitalLibContext> options) : base(options) { }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Autor> Autor { get; set; }
    }
}
