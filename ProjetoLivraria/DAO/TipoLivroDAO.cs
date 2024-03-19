using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class TipoLivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioconexao;

        public BindingList<TipoLivro> BuscaTipo(decimal? adcIdTipo = null)
        {
            BindingList<TipoLivro> loListTipo = new BindingList<TipoLivro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    if (adcIdTipo != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idtipo ORDER BY TIL_DS_DESCRICAO", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idtipo", adcIdTipo));

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioconexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            TipoLivro loNovoTipo = new TipoLivro(loReader.GetDecimal(0), loReader.GetString(1));
                            loListTipo.Add(loNovoTipo);
                        }
                        loReader.Close();
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("Erro ao ler tipos" + ex.Message, ex);
                }
            }
            return loListTipo;
        }
        public int InsereTipo(TipoLivro aoNovoTipo)
        {
            if (aoNovoTipo == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO) VALUES (@idtipo, @descricao)", ioconexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idtipo", aoNovoTipo.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", aoNovoTipo.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao inserir novo tipo" + ex.Message, ex);
                }
            }
            return liQtdRegistrosInseridos;

        }
        public int RemoveTipo(TipoLivro aotipo)
        {
            if (aotipo == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idtipo", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idtipo", aotipo.til_id_tipo_livro));

                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao excluir tipo" + ex.Message, ex);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizarTipo(TipoLivro aotipo)
        {
            if (aotipo == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("UPDATE TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @descricao WHERE TIL_ID_TIPO_LIVRO = @idtipo", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idtipo", aotipo.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", aotipo.til_ds_descricao));
                   

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao atualizar informaçoes do tipo" + ex.Message, ex);
                }
            }
            return liQtdLinhasAtualizadas;
        }
  
        public decimal BuscaIdCategoriaByNome(string nomeCategoria)
        {
            decimal idCategoria = -1; // Valor padrão para indicar que a categoria não foi encontrada

            using (SqlConnection ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    using (SqlCommand ioQuery = new SqlCommand("SELECT TIL_ID_TIPO_LIVRO FROM TIL_TIPO_LIVRO WHERE TIL_DS_DESCRICAO = @nomeCategoria", ioconexao))
                    {
                        ioQuery.Parameters.Add(new SqlParameter("@nomeCategoria", nomeCategoria));

                        using (SqlDataReader reader = ioQuery.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idCategoria = Convert.ToDecimal(reader["TIL_ID_TIPO_LIVRO"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID da categoria. Detalhes: {ex.Message}');</script>");
                }
            }

            return idCategoria;
        }
    }
}
    