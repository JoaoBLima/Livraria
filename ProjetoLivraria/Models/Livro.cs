﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livro
    {
        public decimal liv_id_livro { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }

       public Livro(decimal adcIDLivro, decimal adcIdTipoLivro,decimal adcIDEditor,string asNomeTitulo, decimal valorLivro, decimal pcRoyalty,string asResumo, int edicao)
        {
            this.liv_id_livro = adcIDLivro;
            
            this.liv_id_tipo_livro = adcIdTipoLivro;
            this.liv_id_editor = adcIDEditor;

            this.liv_nm_titulo = asNomeTitulo;
            this.liv_vl_preco = valorLivro;
            this.liv_pc_royalty = pcRoyalty;
            this.liv_ds_resumo = asResumo;
            this.liv_nu_edicao = edicao;
        }
    }
}