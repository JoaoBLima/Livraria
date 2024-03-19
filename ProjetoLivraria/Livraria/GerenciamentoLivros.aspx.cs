using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetoLivraria.Models;
using ProjetoLivraria.DAO;
using System.ComponentModel;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        LivroDAO ioLivrosDAO = new LivroDAO();
        protected LivroAutorDAO ioLivroAutoresDAO = new LivroAutorDAO();
        protected AutoresDAO ioAutoresDAO = new AutoresDAO();
        protected EditoresDAO ioEditoresDAO = new EditoresDAO();
        protected TipoLivroDAO ioTipoLivrosDAO = new TipoLivroDAO();

       

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                CarregaDados();
            }
        }


        //Mudança das listas do tipo BingdingList para Somente List, assim permitindo a paginação
        public List<Livro> ListaLivros
        {
            get
            {
                if ((List<Livro>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();
                return (List<Livro>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }

        public List<TipoLivro> ListaTipoLivros
        {
            get
            {
                if ((List<TipoLivro>)ViewState["ViewStateListaTipoLivros"] == null)
                    this.CarregaDados();
                return (List<TipoLivro>)ViewState["ViewStateListaTipoLivros"];
            }
            set
            {
                ViewState["ViewStateListaTipoLivros"] = value;
            }
        }

        public List<Editores> ListaEditores
        {
            get
            {
                if ((List<Editores>)ViewState["ViewStateListaEditores"] == null)
                    this.CarregaDados();
                return (List<Editores>)ViewState["ViewStateListaEditores"];
            }
            set
            {
                ViewState["ViewStateListaEditores"] = value;
            }
        }

        public List<Autores> ListaAutores
        {
            get
            {
                if ((List<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();
                return (List<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }


        /* private void CarregaDados()
         {
             try
             {


                 if (AutorSessao != null)
                 {
                     this.ListaLivros = this.ioLivrosDAO.FindLivrosByAutores(AutorSessao).OrderBy(loLivroAutor => loLivroAutor.liv_nm_titulo).ToList();

                 }
                 else if (EditorSessao != null)
                 {
                     this.ListaLivros = this.ioLivrosDAO.FindLivrosByEditores(EditorSessao).OrderBy(loLivroEditor => loLivroEditor.liv_nm_titulo).ToList();
                 }
                 else if (TipoSessao != null)
                 {
                     this.ListaLivros = this.ioLivrosDAO.FindLivrosByTipos(TipoSessao).OrderBy(loLivroTipoLivro => loLivroTipoLivro.liv_nm_titulo).ToList();
                 }
                 else
                 {
                       this.ListaLivros = this.ioLivrosDAO.BuscaLivro().OrderBy(loLivro => loLivro.liv_nm_titulo).ToList();

                 }

                 this.gvGerenciamentoLivros.DataSource = this.ListaLivros;
                 this.gvGerenciamentoLivros.DataBind();

                 CarregarDropDownListAutor();
                 CarregarDropDownListEditor();
                 CarregarDropDownListTipoLivro();
             }
             catch
             {
                 HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Livros!');</script>");
             }
         }*/
        private void CarregaDados()
        {
            try
            {

                if (AutorSessao != null)
                {
                    this.ListaLivros = this.ioLivrosDAO.FindLivrosByAutores(AutorSessao).OrderBy(loLivroAutor => loLivroAutor.liv_nm_titulo).ToList();
                }
                else if (EditorSessao != null)
                {
                    this.ListaLivros = this.ioLivrosDAO.FindLivrosByEditores(EditorSessao).OrderBy(loLivroEditor => loLivroEditor.liv_nm_titulo).ToList();
                }
                else if (TipoSessao != null)
                {
                    this.ListaLivros = this.ioLivrosDAO.FindLivrosByTipos(TipoSessao).OrderBy(loLivroTipoLivro => loLivroTipoLivro.liv_nm_titulo).ToList();
                }
                else
                {
                    this.ListaLivros = this.ioLivrosDAO.BuscaLivro().OrderBy(loLivro => loLivro.liv_nm_titulo).ToList();
                }
                if (AutorSessao != null && ListaLivros.Count == 0)
                {
                    // Exibe a mensagem de alerta específica para autores sem livros
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", "alert('O autor selecionado não possui livros'); window.location.href = '/Livraria/GerenciamentoAutores.aspx';", true);
                    return; // Encerra a execução do método para evitar processamento adicional
                }
                else if (EditorSessao != null && ListaLivros.Count == 0)
                {
                    // Exibe a mensagem de alerta específica para editores sem livros
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", "alert('O editor selecionado não possui livros'); window.location = '/Livraria/GerenciamentoEditores';", true);
                    return; // Encerra a execução do método para evitar processamento adicional
                }
                else if (TipoSessao != null && ListaLivros.Count == 0)
                {
                    // Exibe a mensagem de alerta específica para editores sem livros
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", "alert('A categoria selecionada não possui livros'); window.location = '/Livraria/GerenciamentoCategorias';", true);
                    return; // Encerra a execução do método para evitar processamento adicional
                }
                    this.gvGerenciamentoLivros.DataSource = this.ListaLivros;
                this.gvGerenciamentoLivros.DataBind();

                CarregarDropDownListAutor();
                CarregarDropDownListEditor();
                CarregarDropDownListTipoLivro();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Livros!');</script>");
            }
        }



        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdLivro") as
                Label).Text);
            string IsTituloLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditNomeLivro") as
                TextBox).Text;
            decimal ldcIdTipoLivro = this.ioTipoLivrosDAO.BuscaIdCategoriaByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditTipoLivro") as DropDownList).SelectedValue);
            decimal ldcIdEditor = this.ioEditoresDAO.BuscaIdEditorByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditEditor") as DropDownList).SelectedValue);
            decimal ldcIdAutor = this.ioAutoresDAO.BuscaIdAutorByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditAutorLivro") as DropDownList).SelectedValue);
            decimal ldcPrecoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as
                TextBox).Text);
            decimal ldcRoyaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as
                TextBox).Text);
            string lsResumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
            int IntEdicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEdicaoLivro") as
                TextBox).Text);

            if (String.IsNullOrWhiteSpace(IsTituloLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o Titulo do Livro!');</script>");
            else if (ldcIdTipoLivro == -1)
                HttpContext.Current.Response.Write("<script>alert('Selecione o Tipo do Livro!');</script>");
            else if (ldcIdEditor == -1)
                HttpContext.Current.Response.Write("<script>alert('Selecione o Editor!');</script>");
            else if (ldcIdAutor == -1)
                HttpContext.Current.Response.Write("<script>alert('Selecione o Autor!');</script>");
            else if (ldcPrecoLivro == 0)
                HttpContext.Current.Response.Write("<script>alert('Digite o preço do livro!');</script>");
            else if (ldcRoyaltyLivro == 0)
                HttpContext.Current.Response.Write("<script>alert('Digite o Royalty do livro');</script>");
            else if (String.IsNullOrWhiteSpace(lsResumoLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o resumo do livro!');</script>");
            else if (IntEdicaoLivro == 0)
                HttpContext.Current.Response.Write("<script>alert('Digite o número da edição!');</script>");
            else
            {
                try
                {
                    Livro loLivro = new Livro(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, IsTituloLivro, ldcPrecoLivro,
                                                        ldcRoyaltyLivro, lsResumoLivro, IntEdicaoLivro);
                    LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyaltyLivro);
                    this.ioLivrosDAO.AtualizarLivro(loLivro);
                    this.ioLivroAutoresDAO.Atualizarlivroautor(loLivroAutor);
                    this.gvGerenciamentoLivros.EditIndex = -1;
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Livro atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do Livro!');</script>");
                }
            }
        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = this.gvGerenciamentoLivros.Rows[e.RowIndex];
                decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblIdLivro") as Label).Text);
                Livro loLivro = this.ioLivrosDAO.BuscaLivro(ldcIdLivro).FirstOrDefault();
                LivroAutor loLivroAutor = this.ioLivroAutoresDAO.BuscaLivroAutor(ldcIdLivro).FirstOrDefault();
                this.ioLivrosDAO.RemoveLivro(loLivro);
                this.ioLivroAutoresDAO.RemoveLivroAutor(loLivroAutor);

                HttpContext.Current.Response.Write("<script>alert('Livro removido com sucesso!');</script>");

                this.CarregaDados();

            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro ao excluir livro: {ex.Message}');</script>");
            }
        }


        protected void gvGerenciamentoLivros_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate))
                {
                    DropDownList ddlEditTipoLivro = (DropDownList)e.Row.FindControl("ddlEditTipoLivro");
                    DropDownList ddlEditEditor = (DropDownList)e.Row.FindControl("ddlEditEditor");
                    DropDownList ddlEditAutor = (DropDownList)e.Row.FindControl("ddlEditAutorLivro");

                    Livro var = (Livro)e.Row.DataItem;

                    // Preencher DropDownList para Tipo de Livro
                    ddlEditTipoLivro.DataSource = ddlTipoLivro.Items;
                    ddlEditTipoLivro.SelectedValue = this.ioTipoLivrosDAO.BuscaTipo(var.liv_id_tipo_livro).FirstOrDefault()?.til_ds_descricao ?? "";
                    ddlEditTipoLivro.DataBind();

                    // Preencher DropDownList para Editor
                    ddlEditEditor.DataSource = ddlEditor.Items;
                    ddlEditEditor.SelectedValue = this.ioEditoresDAO.BuscaEditores(var.liv_id_editor).FirstOrDefault()?.edi_nm_editor ?? "";
                    ddlEditEditor.DataBind();

                    ddlEditAutor.DataSource = ddlAutor.Items;
                    ddlEditAutor.SelectedValue = this.ioAutoresDAO.FindAutorByLivros(var.liv_id_livro).FirstOrDefault()?.aut_nm_nome ?? "";
                    ddlEditAutor.DataBind();
                }
            }
        }


        private void CarregarDropDownListAutor()
        {
            try
            {
                ddlAutor.DataSource = ioAutoresDAO.BuscaAutores().OrderBy(AUT => AUT.aut_nm_nome);
                ddlAutor.DataTextField = "aut_nm_nome";
                ddlAutor.DataValueField = "aut_id_autor";
                ddlAutor.DataBind();
                ddlAutor.Items.Insert(0, new ListItem("Selecione...", ""));
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro Ao carregar lista dos autores!');</script>");
            }

        }
        private void CarregarDropDownListEditor()
        {
            try
            {
                ddlEditor.DataSource = ioEditoresDAO.BuscaEditores().OrderBy(EDI => EDI.edi_nm_editor);
                ddlEditor.DataTextField = "edi_nm_editor";
                ddlEditor.DataValueField = "edi_id_editor";
                ddlEditor.DataBind();
                ddlEditor.Items.Insert(0, new ListItem("Selecione...", ""));
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro Ao carregar lista dos editores!');</script>");
            }
        }

        private void CarregarDropDownListTipoLivro()
        {
            try
            {
                ddlTipoLivro.DataSource = ioTipoLivrosDAO.BuscaTipo().OrderBy(TIL => TIL.til_ds_descricao);
                ddlTipoLivro.DataTextField = "til_ds_descricao";
                ddlTipoLivro.DataValueField = "til_id_tipo_livro";
                ddlTipoLivro.DataBind();
                ddlTipoLivro.Items.Insert(0, new ListItem("Selecione...", ""));
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro Ao carregar lista dos Tipo de livro!');</script>");
            }
        }

        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        public Editores EditorSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }

        public TipoLivro TipoSessao
        {
            get { return (TipoLivro)Session["SessionTipoSelecionado"]; }
            set { Session["SessionTipoSelecionado"] = value; }
        }


        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {

            try
            {
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(L => L.liv_id_livro).First().liv_id_livro + 1;
                decimal ldcIdTipoLivro = Convert.ToDecimal(ddlTipoLivro.SelectedValue);
                decimal ldcIdEditor = Convert.ToDecimal(ddlEditor.SelectedValue);
                decimal ldcIdAutor = Convert.ToDecimal(ddlAutor.SelectedValue);
                string IsTituloLivro = this.tbxCadastroNomeLivro.Text;
                decimal ldcPrecoLivro = Convert.ToDecimal(txtCadastroPrecoLivro.Text);
                decimal ldcRoyaltyLivro = Convert.ToDecimal(txtCadastroRoyaltyLivro.Text);
                string IsResumoLivro = this.txtCadastroResumoLivro.Text;
                int IntEdicaoLivro = Convert.ToInt32(txtCadastroEdicaoLivro.Text);

                Livro loLivro = new Livro(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, IsTituloLivro, ldcPrecoLivro,
                                                        ldcRoyaltyLivro, IsResumoLivro, IntEdicaoLivro);
                LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyaltyLivro);
                this.ioLivrosDAO.InsereLivro(loLivro);
                this.ioLivroAutoresDAO.InsereLivroAutor(loLivroAutor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Livro!');</script>");
            }

            this.tbxCadastroNomeLivro.Text = string.Empty;
            this.ddlTipoLivro.SelectedIndex = 0;
            this.ddlEditor.SelectedIndex = 0;
            this.ddlAutor.SelectedIndex = 0;
            this.txtCadastroPrecoLivro.Text = string.Empty;
            this.txtCadastroRoyaltyLivro.Text = string.Empty;
            this.txtCadastroResumoLivro.Text = string.Empty;
            this.txtCadastroEdicaoLivro.Text = string.Empty;

        }

        protected void gvGerenciamentoLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenciamentoLivros.PageIndex = e.NewPageIndex;
            CarregaDados();
        }
    }
}