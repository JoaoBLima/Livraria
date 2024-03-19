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
    public class LivroAutorDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioconexao;

        public BindingList<LivroAutor> BuscaLivroAutor(decimal? adcIDLivro = null)
        {
            BindingList<LivroAutor> loListLivrosautores = new BindingList<LivroAutor>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    if (adcIDLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_LIVRO = @idlivro", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idlivro", adcIDLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR", ioconexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivroAutor loNovoLivroAutor = new LivroAutor(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2));
                            loListLivrosautores.Add(loNovoLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao ler livros e autores");
                }

            }
            return loListLivrosautores;
        }
        public int InsereLivroAutor(LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null)
                throw new NullReferenceException();//caso o autor nao venha preenchido
            int liQtdRegistrosInseridos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIA_LIVRO_AUTOR (LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) VALUES (@idautor, @idlivro, @royalty)", ioconexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aoNovoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aoNovoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivroAutor.lia_pc_royalty));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceção ao inserir livro e autor: {ex.Message}");
                    throw;
                }
            }
            return liQtdRegistrosInseridos;

        }
        public int RemoveLivroAutor(LivroAutor aolivroautor)
        {
            if (aolivroautor == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_LIVRO = @idlivro", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aolivroautor.lia_id_livro));

                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao excluir livroautor" + ex.Message, ex);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int Atualizarlivroautor(LivroAutor aolivroautor)
        {
            if (aolivroautor == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIA_LIVRO_AUTOR SET LIA_ID_LIVRO = @idlivro,LIA_ID_AUTOR = @idautor,LIA_PC_ROYALTY = @royalty WHERE LIA_ID_LIVRO = @idlivro", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aolivroautor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aolivroautor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aolivroautor.lia_pc_royalty));

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao atualizar informaçoes do livro e autor" + ex.Message, ex);
                }
            }
            return liQtdLinhasAtualizadas;
        }

       
    }
}
        
    
