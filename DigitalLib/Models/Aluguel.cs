using DigitalLib.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalLib.Models
{
    public class Aluguel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataEmprestimo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataDevolucao { get; set; }

        public int LivroId { get; set; }
        public Livro? Livro { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        //Teste push
    }
}
