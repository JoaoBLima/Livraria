using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class LivroAutor
    {
        public decimal lia_id_autor { get; set; }
        public decimal lia_id_livro { get; set; }
        public decimal lia_pc_royalty { get; set; }

        public LivroAutor(decimal adcIdAutor,decimal adcIdLivro, decimal pcRoyalty)
        {
            this.lia_id_autor = adcIdAutor;
            this.lia_id_livro = adcIdLivro;
            this.lia_pc_royalty = pcRoyalty;
        }

        public LivroAutor()
        {
        }
    }
}