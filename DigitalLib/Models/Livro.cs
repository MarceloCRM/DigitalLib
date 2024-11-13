﻿using System.ComponentModel.DataAnnotations;

namespace DigitalLib.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataPublicacao { get; set; }
        public double? Preco { get; set; }
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
    }
}
