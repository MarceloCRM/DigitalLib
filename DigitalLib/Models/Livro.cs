using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DigitalLib.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataPublicacao { get; set; }
        public double? Preco { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
        public ICollection<Aluguel>? Alugueis { get; set; }
    }
}
