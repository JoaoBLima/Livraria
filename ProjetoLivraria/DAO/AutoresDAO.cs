using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProjetoLivraria.Models;
using System.ComponentModel;
using System.Configuration;

namespace ProjetoLivraria.DAO
{
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioconexao;

        public BindingList<Autores> BuscaAutores(decimal? adcIDAutor = null)
        {
            BindingList<Autores> loListAutores = new BindingList<Autores>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    if (adcIDAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idautor", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idautor", adcIDAutor));

                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioconexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListAutores.Add(loNovoAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao ler autores");
                }
            }
            return loListAutores;
        }

        public int InsereAutor(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();//caso o autor nao venha preenchido
            int liQtdRegistrosInseridos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idautor, @nomeautor, @sobrenomeautor, @emailautor)", ioconexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aoNovoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeautor", aoNovoAutor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeautor", aoNovoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailautor", aoNovoAutor.aut_ds_email));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao inserir novo autor" + ex.Message, ex);
                }
            }
            return liQtdRegistrosInseridos;

        }
        public int RemoveAutor(Autores aoautor)
        {
            if (aoautor == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idautor", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aoautor.aut_id_autor));

                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao excluir autor" + ex.Message,ex);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int Atualizarautor(Autores aoautor)
        {
            if (aoautor == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("UPDATE AUT_AUTORES SET AUT_NM_NOME = @nomeautor,AUT_NM_SOBRENOME = @sobrenomeautor,AUT_DS_EMAIL = @emailautor WHERE AUT_ID_AUTOR = @idautor", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idautor", aoautor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeautor", aoautor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeautor", aoautor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailautor", aoautor.aut_ds_email));

                   liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao atualizar informaçoes do autor" + ex.Message,ex);
                }
            }
            return liQtdLinhasAtualizadas;
        }

        public BindingList<Autores> FindAutorByLivros(decimal? adcIdLivro = null)
        {
            BindingList<Autores> loListAutores = new BindingList<Autores>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT AUT.*  FROM AUT_AUTORES AUT
                                                    INNER JOIN LIA_LIVRO_AUTOR LIA ON AUT.AUT_ID_AUTOR = LIA_ID_AUTOR
                                                    INNER JOIN LIV_LIVROS LIV ON LIV_ID_LIVRO = LIA_ID_LIVRO
                                                    WHERE LIV_ID_LIVRO = @idLivro", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Autores loAutor = new Autores(
                                    loReader.GetDecimal(loReader.GetOrdinal("aut_id_autor")),
                                    loReader.GetString(loReader.GetOrdinal("aut_nm_nome")),  
                                    loReader.GetString(loReader.GetOrdinal("aut_nm_sobrenome")),    
                                    loReader.GetString(loReader.GetOrdinal("aut_ds_email"))

                                );

                                loListAutores.Add(loAutor);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("ID do Livro não fornecido.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por autor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListAutores;
        }
        public decimal BuscaIdAutorByNome(string nomeAutor)
        {
            decimal idAutor = -1; // Valor padrão para indicar que o autor não foi encontrado

            using (SqlConnection ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    //faz consulta SQL utilizando da sua tabela associativa (LivroAutor)
                    using (SqlCommand ioQuery = new SqlCommand("SELECT AUT_ID_AUTOR FROM AUT_AUTORES WHERE AUT_NM_NOME = @nomeAutor", ioconexao))
                    {
                        ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", nomeAutor));

                        using (SqlDataReader reader = ioQuery.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idAutor = Convert.ToDecimal(reader["AUT_ID_AUTOR"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID do autor. Detalhes: {ex.Message}');</script>");
                }
            }

            return idAutor;
        }

    }
}

