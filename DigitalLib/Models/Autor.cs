using System.ComponentModel.DataAnnotations;

namespace DigitalLib.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
    }
}
