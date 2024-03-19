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
    public class LivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioconexao;

        public BindingList<Livro> BuscaLivro(decimal? adcIDLivro = null)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    if (adcIDLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idlivro", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idlivro", adcIDLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioconexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livro loNovoLivro = new Livro(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("Erro ao ler livros. Detalhes: " + ex.Message, ex);
                }

            }
            return loListLivros;
        }
        public int InsereLivro(Livro aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIV_LIVROS (LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO) VALUES (@idlivro, @idtipo, @ideditor, @titulo, @preco, @royalty, @resumo, @edicao)", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idtipo", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@ideditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicao", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao inserir novo livro.Detalhes: " + ex.Message, ex);
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveLivro(Livro aolivro)
        {

            if (aolivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idlivro", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aolivro.liv_id_livro));

                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao excluir livro.Detalhes: " + ex.Message, ex);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int AtualizarLivro(Livro aolivro)
        {
            if (aolivro == null)
                throw new NullReferenceException();

            int liQtdLinhasAtualizadas = 0;

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    // Atualizando as informações do livro, editor e tipo
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_NM_TITULO = @titulo, LIV_VL_PRECO = @preco, LIV_PC_ROYALTY = @royalty, LIV_DS_RESUMO = @resumo, LIV_NU_EDICAO = @edicao, LIV_ID_TIPO_LIVRO = @tipoLivro, LIV_ID_EDITOR = @idEditor WHERE LIV_ID_LIVRO = @idlivro", ioconexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idlivro", aolivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", aolivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", aolivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aolivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aolivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicao", aolivro.liv_nu_edicao));
                    ioQuery.Parameters.Add(new SqlParameter("@tipoLivro", aolivro.liv_id_tipo_livro));  // Adicione esta linha
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aolivro.liv_id_editor));     // Adicione esta linha

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar informações do livro.Detalhes: " + ex.Message, ex);
                }
            }

            return liQtdLinhasAtualizadas;
        }



        public BindingList<Livro> FindLivrosByAutor(decimal? adcIdAutor = null)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (adcIdAutor != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT LIV.* 
                                          FROM LIV_LIVROS LIV
                                          INNER JOIN LIA_LIVRO_AUTOR LIA ON LIV.LIV_ID_LIVRO = LIA.LIA_ID_LIVRO
                                          WHERE LIA.LIA_ID_AUTOR = @idAutor", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAutor));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),  
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),    
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("ID do autor não fornecido.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por autor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }
    
        public BindingList<Livro> FindLivrosByEditor(decimal? adcIdEditor = null)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (adcIdEditor != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE liv_id_editor = @idEditor", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", adcIdEditor));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("ID do editor não fornecido.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por editor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }

        public BindingList<Livro> FindLivrosByTipo(decimal? adcIdTipo = null)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (adcIdTipo != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE liv_id_Tipo_Livro = @idTipo", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idTipo", adcIdTipo));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("ID do Tipo não fornecido.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por Categoria. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }
        public BindingList<Livro> FindLivrosByEditores(Editores editor)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (editor != null && editor.edi_id_editor != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE liv_id_editor = @idEditor", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", editor.edi_id_editor));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                    
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Objeto Editor não fornecido ou sem ID.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por editor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }
        public BindingList<Livro> FindLivrosByTipos(TipoLivro tipoLivro)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (tipoLivro != null && tipoLivro.til_id_tipo_livro != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT * FROM LIV_LIVROS WHERE liv_id_tipo_livro = @idTipo", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idTipo", tipoLivro.til_id_tipo_livro));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                   
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                     loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Objeto TipoLivro não fornecido ou sem ID.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por tipo. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }
        public BindingList<Livro> FindLivrosByAutores(Autores autor)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (autor != null && autor.aut_id_autor != null)
                    {
                       

                        ioQuery = new SqlCommand("SELECT L.* FROM LIV_LIVROS L INNER JOIN LIA_LIVRO_AUTOR LA ON L.LIV_ID_LIVRO = LA.LIA_ID_LIVRO WHERE LA.LIA_ID_AUTOR = @idAutor", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", autor.aut_id_autor));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do livro com os valores lidos do banco de dados
                                Livro loNovoLivro = new Livro(
                                    loReader.GetDecimal(loReader.GetOrdinal("LIV_ID_LIVRO")),
                                   
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_id_tipo_livro")),
                                     loReader.GetDecimal(loReader.GetOrdinal("liv_id_editor")),
                                    loReader.GetString(loReader.GetOrdinal("liv_nm_titulo")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_vl_preco")),
                                    loReader.GetDecimal(loReader.GetOrdinal("liv_pc_royalty")),
                                    loReader.GetString(loReader.GetOrdinal("liv_ds_resumo")),
                                    loReader.GetInt32(loReader.GetOrdinal("liv_nu_edicao"))
                                );

                                loListLivros.Add(loNovoLivro);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Objeto Autor não fornecido ou sem ID.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar os livros por autor. Detalhes: " + ex.Message, ex);
                }
            }

            return loListLivros;
        }


    }

    }




