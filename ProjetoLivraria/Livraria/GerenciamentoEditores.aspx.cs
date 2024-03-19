using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregaDados();
            }

        }

        protected void gvGerenciamentoEditores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoEditores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoEditores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //O índice -1 indica que nenhuma linha está sendo editada.
            this.gvGerenciamentoEditores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoEditores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Capturando os valores digitados pelo usuário nos campos de edição do GridView
            //e o ID do usuário que está sendo editado.
            decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblEditIdEditor") as Label).Text);
            string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditNomeEditor") as TextBox).Text;
            
            string lsEmailEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditEmailEditor") as
           TextBox).Text;
            string lsUrlEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditUrlEditor") as TextBox).Text;

            //Validando se todos os campos estão preenchidos, caso não estejam, é enviado uma mensagem para o usuário.
            if (String.IsNullOrWhiteSpace(lsNomeEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do editor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite o e-mail do editor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsUrlEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite a url do editor.');</script>");
            else
            {
                try
                {
                    //Instanciando um objeto do tipo editor com os dados digitados pelo usuário e o ID do editor que está sendo editado.
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor,  lsEmailEditor, lsUrlEditor);
                    //Chamando o método de atualização do DAO.
                    this.ioEditoresDAO.Atualizareditor(loEditor);
                    //Alterando a propriedade EditIndex para -1, para indicar que acabou a edição dessa linha.
                    this.gvGerenciamentoEditores.EditIndex = -1;
                    //Atualizando a lista de editores na ViewState e os valores do GridView.
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Editor atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do Editor.');</script>");
                }
            }
        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoEditores.Rows[e.RowIndex];
                decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblIdEditor") as
               Label).Text);
                Editores loEditor = this.ioEditoresDAO.BuscaEditores(ldcIdEditor).FirstOrDefault();
                if (loEditor != null)
                {
                    //Criei o método FindLivrosByEditor() que deve receber um editor como parâmetro e retornar
                    //uma lista de livros.
                    LivroDAO loLivrosDAO = new LivroDAO();
                    if (loLivrosDAO.FindLivrosByEditor(loEditor.edi_id_editor).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Editor não pode ser removido pois tem livros associados, se desejar, clique em livros para saber mais.');</script>");
                    }
                    else
                    {
                        this.ioEditoresDAO.RemoveEditor(loEditor);
                        HttpContext.Current.Response.Write("<script>alert('Editor removido com sucesso!');</script>");
                        this.CarregaDados();
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do editor selecionado.');</script>");
            }
        }
        public Editores EditorSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }

        protected void gvGerenciamentoEditores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosEditor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblIdEditor") as Label).Text);
                    string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblNomeEditor") as Label).Text;
                    string lsEmailEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblEmailEditor") as Label).Text;
                    string lsUrlEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblUrlEditor") as Label).Text;
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor,lsUrlEditor, lsEmailEditor);

                    this.EditorSessao = loEditor;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }

        private void CarregaDados()
        {
            try
            {
                //Chamando o método BuscaEditores para salvar os editores cadastrados na ViewState.
                this.ListaEditores = this.ioEditoresDAO.BuscaEditores().OrderBy(loEditor => loEditor.edi_nm_editor).ToList();
                //Indicando onde o GridView deve buscar os valores que serão listados.
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores;
                //Ppopulando o GridView com os dados de ListaEditores.
                this.gvGerenciamentoEditores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar editores.');</script>");
            }
        }

        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                //Utilizando o Linq para obter o maior ID de editores cadastrados e incrementando o valor em 1
                //para garantir que a chave primária não se repita (esse campo não é auto-increment no banco).
                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(r => r.edi_id_editor).First().edi_id_editor + 1;
                //Salvando os valores que o usuário preencheu em cada campo do formulário (utilizando o "this.NomeDoControle"
                //é possível recuperar o controle e acessar suas propriedades, isso é possível pois todo controle ASP tem
                //um ID único na página e deve ser marcado como runat="server" para virar um "ServerControl" e ser acessível
                //aqui no "CodeBehind" da página.
                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsEmailEditor = this.txtCadastroEmailEditor.Text;
                string lsUrlEditor = this.txtCadastroUrlEditor.Text;
                //Instanciando um objeto do tipo editores para ser adicionado (perceba que só existe um construtor para essa classe
                //onde devem ser passados todos os valores, fizemos isso como mais uma forma de garantir que não será possível
                //cadastrar editores com informações faltando, mesmo que o banco permita isso – além dos RequiredFieldValidator).
                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor,  lsEmailEditor, lsUrlEditor);
                //Chamando o método de inserir o novo editor na base de dados.
                this.ioEditoresDAO.InsereEditor(loEditor);
                //Atualizando a ViewState com o novo editor recém-inserido.
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Editor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Editor.');</script>");
            }
            //Limpando os campos do formulário.
            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.txtCadastroEmailEditor.Text = String.Empty;
            this.txtCadastroUrlEditor.Text = String.Empty;
        }

        protected void gvGerenciamentoEditores_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvGerenciamentoEditores.PageIndex = e.NewPageIndex;
            CarregaDados();
        }

    }
}