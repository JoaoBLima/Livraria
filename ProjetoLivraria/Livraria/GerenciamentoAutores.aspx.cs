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
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            //Se o comando de carregamento da página não vier de um dos controles dela, execute o método CarregaDados().
            if (!this.IsPostBack)
                this.CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                //Chamando o método BuscaAutores para salvar os autores cadastrados na ViewState.
                this.ListaAutores = this.ioAutoresDAO.BuscaAutores().OrderBy(loAutor => loAutor.aut_nm_nome).ToList();
                //Indicando onde o GridView deve buscar os valores que serão listados.
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores;
                //Ppopulando o GridView com os dados de ListaAutores.
                this.gvGerenciamentoAutores.DataBind();
            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Autores.');</script>");
            }
        }

        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                //Utilizando o Linq para obter o maior ID de autores cadastrados e incrementando o valor em 1
                //para garantir que a chave primária não se repita (esse campo não é auto-increment no banco).
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                //Salvando os valores que o usuário preencheu em cada campo do formulário (utilizando o "this.NomeDoControle"
                //é possível recuperar o controle e acessar suas propriedades, isso é possível pois todo controle ASP tem
                //um ID único na página e deve ser marcado como runat="server" para virar um "ServerControl" e ser acessível
                //aqui no "CodeBehind" da página.
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.txtCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.txtCadastroEmailAutor.Text;
                //Instanciando um objeto do tipo Autores para ser adicionado (perceba que só existe um construtor para essa classe
                //onde devem ser passados todos os valores, fizemos isso como mais uma forma de garantir que não será possível
                //cadastrar autores com informações faltando, mesmo que o banco permita isso – além dos RequiredFieldValidator).
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                //Chamando o método de inserir o novo autor na base de dados.
                this.ioAutoresDAO.InsereAutor(loAutor);
                //Atualizando a ViewState com o novo autor recém-inserido.
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>");
            }
            //Limpando os campos do formulário.
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.txtCadastroSobrenomeAutor.Text = String.Empty;
            this.txtCadastroEmailAutor.Text = String.Empty;
        }

        protected void gvGerenciamentoAutores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //O índice -1 indica que nenhuma linha está sendo editada.
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Capturando os valores digitados pelo usuário nos campos de edição do GridView
            //e o ID do usuário que está sendo editado.
            decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblEditIdAutor") as
           Label).Text);
            string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor") as TextBox).Text;
            string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditSobrenomeAutor") as
           TextBox).Text;
            string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditEmailAutor") as TextBox).Text;
            //Validando se todos os campos estão preenchidos, caso não estejam, é enviado uma mensagem para o usuário.
            if (String.IsNullOrWhiteSpace(lsNomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsSobrenomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o sobrenome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o E-mail do autor.');</script>");
            else
            {
                try
                {
                    //Instanciando um objeto do tipo Autor com os dados digitados pelo usuário e o ID do autor que está sendo editado.
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                    //Chamando o método de atualização do DAO.
                    this.ioAutoresDAO.Atualizarautor(loAutor);
                    //Alterando a propriedade EditIndex para -1, para indicar que acabou a edição dessa linha.
                    this.gvGerenciamentoAutores.EditIndex = -1;
                    //Atualizando a lista de autores na ViewState e os valores do GridView.
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Autor atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do autor.');</script>");
                }
            }
        }
        protected void gvGerenciamentoAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoAutores.Rows[e.RowIndex];
                decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblIdAutor") as
               Label).Text);
                Autores loAutor = this.ioAutoresDAO.BuscaAutores(ldcIdAutor).FirstOrDefault();
                if (loAutor != null)
                {
                    //Crie LivrosDAO e o método FindLivrosByAutor() que deve receber um Autor como parâmetro e retornar
                    //uma lista de livros.
                    LivroDAO loLivrosDAO = new LivroDAO();
                    if (loLivrosDAO.FindLivrosByAutor(loAutor.aut_id_autor).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Autor não pode ser removido pois tem livros associados,se desejar, clique em livros para saber mais..');</script>");
                    }
                    else
                    {
                        this.ioAutoresDAO.RemoveAutor(loAutor);
                        HttpContext.Current.Response.Write("<script>alert('Autor removido com sucesso!');</script>");
                        this.CarregaDados();
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor selecionado.');</script>");
            }
        }
        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        protected void gvGerenciamentoAutores_RowCommand(object sender , GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosAutor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblIdAutor") as Label).Text);
                    string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblNomeAutor") as Label).Text;
                    string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblSobrenomeAutor") as Label).Text;
                    string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblEmailAutor") as Label).Text;
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                    this.AutorSessao = loAutor;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;

                default:
                    break;

            }
        }

        protected void gvGerenciamentoAutores_PageIndexChanging(Object sender,GridViewPageEventArgs e)
        {
            gvGerenciamentoAutores.PageIndex = e.NewPageIndex;
            CarregaDados();
        }
        }

    }


