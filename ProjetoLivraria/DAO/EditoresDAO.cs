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
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioconexao;

        public BindingList<Editores> BuscaEditores(decimal? adcIDEditor = null)
        {
            BindingList<Editores> loListEditores = new BindingList<Editores>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    if (adcIDEditor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @ideditor", ioconexao);
                        ioQuery.Parameters.Add(new SqlParameter("@ideditor", adcIDEditor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioconexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Editores loNovoEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListEditores.Add(loNovoEditor);
                        }
                        loReader.Close();
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("Erro ao ler editores" + ex.Message, ex);
                }

            }
            return loListEditores;
        }
        public int InsereEditor(Editores aoNovoEditor)
        {
            if (aoNovoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO EDI_EDITORES (EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_EMAIL, EDI_DS_URL) VALUES (@ideditor, @nomeeditor, @emaileditor, @urleditor)", ioconexao);

                    ioQuery.Parameters.Add(new SqlParameter("@ideditor", aoNovoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeeditor", aoNovoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emaileditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urleditor", aoNovoEditor.edi_ds_url));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao inserir novo autor" + ex.Message, ex);
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveEditor(Editores aoeditor)
        {

            if (aoeditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosExcluidos = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @ideditor", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@ideditor", aoeditor.edi_id_editor));

                    liQtdRegistrosExcluidos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao excluir editor" + ex.Message, ex);
                }
            }
            return liQtdRegistrosExcluidos;
        }
        public int Atualizareditor(Editores aoeditor)
        {
            if (aoeditor == null)
                throw new NullReferenceException();
            int liQtdLinhasAtualizadas = 0;
            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @nomeeditor,EDI_DS_EMAIL = @emaileditor,EDI_DS_URL = @urleditor WHERE EDI_ID_EDITOR = @ideditor", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@ideditor", aoeditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeeditor", aoeditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emaileditor", aoeditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urleditor", aoeditor.edi_ds_url));

                    liQtdLinhasAtualizadas = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("erro ao atualizar informaçoes do editor" + ex.Message, ex);
                }
            }
            return liQtdLinhasAtualizadas;
        }
        public string BuscarNomeEditor(decimal idEditor)
        {
            string nomeEditor = string.Empty;

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    ioQuery = new SqlCommand("SELECT EDI_NM_EDITOR FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioconexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", idEditor));

                    using (SqlDataReader reader = ioQuery.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nomeEditor = reader["EDI_NM_EDITOR"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar nome do editor. Detalhes: {ex.Message}');</script>");
                }
            }

            return nomeEditor;
        }
        public decimal BuscaIdEditorByNome(string nomeEditor)
        {
            decimal idEditor = -1; // Valor padrão para indicar que o editor não foi encontrado

            using (SqlConnection ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();
                    using (SqlCommand ioQuery = new SqlCommand("SELECT EDI_ID_EDITOR FROM EDI_EDITORES WHERE EDI_NM_EDITOR = @nomeEditor", ioconexao))
                    {
                        ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", nomeEditor));

                        using (SqlDataReader reader = ioQuery.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idEditor = Convert.ToDecimal(reader["EDI_ID_EDITOR"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro ao buscar ID do editor. Detalhes: {ex.Message}');</script>");
                }
            }

            return idEditor;
        }
        public BindingList<Editores> FindEditorByLivro(decimal? adcIdLivro = null)
        {
            BindingList<Editores> loListEditores = new BindingList<Editores>();

            using (ioconexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioconexao.Open();

                    if (adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand(@"SELECT EDI.* FROM EDI_EDITORES EDI
                                            INNER JOIN LIV_LIVROS LIV ON EDI.EDI_ID_EDITOR = LIV.LIV_ID_EDITOR
                                            WHERE LIV_ID_LIVRO = @idLivro", ioconexao);

                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));

                        using (SqlDataReader loReader = ioQuery.ExecuteReader())
                        {
                            while (loReader.Read())
                            {
                                // Preenche os campos do editor com os valores lidos do banco de dados
                                Editores loEditor = new Editores(
                                    loReader.GetDecimal(loReader.GetOrdinal("edi_id_editor")),
                                    loReader.GetString(loReader.GetOrdinal("edi_nm_editor")),
                                    loReader.GetString(loReader.GetOrdinal("edi_ds_email")),
                                    loReader.GetString(loReader.GetOrdinal("edi_ds_url"))
                                );

                                loListEditores.Add(loEditor);
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
                    throw new Exception("Erro ao tentar buscar os editores por livro. Detalhes: " + ex.Message, ex);
                }
            }

            return loListEditores;
        }


    }
}
