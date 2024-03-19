using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class TipoLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }

        public TipoLivro(decimal adcIDTipoLivro , string asDescricaoTipoLivro)
        {
            this.til_id_tipo_livro = adcIDTipoLivro;
            this.til_ds_descricao = asDescricaoTipoLivro;
        }
    }
}