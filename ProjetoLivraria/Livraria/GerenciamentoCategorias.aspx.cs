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
    public partial class GerenciamentoCategorias : System.Web.UI.Page
    {
        TipoLivroDAO ioTipoDAO = new TipoLivroDAO();

        public List<TipoLivro> ListaTipos
        {
            get
            {
                if ((List<TipoLivro>)ViewState["ViewStateListaTipos"] == null)
                    this.CarregaDados();
                return (List<TipoLivro>)ViewState["ViewStateListaTipos"];
            }
            set
            {
                ViewState["ViewStateListaTipos"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregaDados();
            }


        }

        private void CarregaDados()
        {
            try
            {
                this.ListaTipos = this.ioTipoDAO.BuscaTipo().OrderBy(loTipo => loTipo.til_ds_descricao).ToList();
                this.gvGerenciamentoCategorias.DataSource = this.ListaTipos;
                this.gvGerenciamentoCategorias.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar categorias');</script>");
            }

        }

        protected void BtnNovoTipo_Click(object sender, EventArgs e)
        {
            try
            {
                //Utilizando o Linq para obter o maior ID de autores cadastrados e incrementando o valor em 1
                //para garantir que a chave primária não se repita (esse campo não é auto-increment no banco).
                decimal ldcIdTipo = this.ListaTipos.OrderByDescending(r => r.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                //Salvando os valores que o usuário preencheu em cada campo do formulário (utilizando o "this.NomeDoControle"
                //é possível recuperar o controle e acessar suas propriedades, isso é possível pois todo controle ASP tem
                //um ID único na página e deve ser marcado como runat="server" para virar um "ServerControl" e ser acessível
                //aqui no "CodeBehind" da página.
                string lsDescricaoTipo = this.tbxCadastroDescricaoTipo.Text;
              
                //Instanciando um objeto do tipo Autores para ser adicionado (perceba que só existe um construtor para essa classe
                //onde devem ser passados todos os valores, fizemos isso como mais uma forma de garantir que não será possível
                //cadastrar autores com informações faltando, mesmo que o banco permita isso – além dos RequiredFieldValidator).
                TipoLivro loTipo = new TipoLivro(ldcIdTipo, lsDescricaoTipo);
                //Chamando o método de inserir o novo autor na base de dados.
                this.ioTipoDAO.InsereTipo(loTipo);
                //Atualizando a ViewState com o novo autor recém-inserido.
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Tipo cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do tipo.');</script>");
            }
            //Limpando os campos do formulário.
            this.tbxCadastroDescricaoTipo.Text = String.Empty;
           
        }
        protected void gvGerenciamentoCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoCategorias.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //O índice -1 indica que nenhuma linha está sendo editada.
            this.gvGerenciamentoCategorias.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Capturando os valores digitados pelo usuário nos campos de edição do GridView
            //e o ID do usuário que está sendo editado.
            decimal ldcIdTipo = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("lblEditIdTipo") as Label).Text);
            string lsDescricaoTipo = (this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("tbxEditNomeTipo") as TextBox).Text;

            //Validando se todos os campos estão preenchidos, caso não estejam, é enviado uma mensagem para o usuário.
            if (String.IsNullOrWhiteSpace(lsDescricaoTipo))
                HttpContext.Current.Response.Write("<script>alert('Digite a descrição da categoria.');</script>");
           
            else
            {
                try
                {
                    //Instanciando um objeto do tipo categoria com os dados digitados pelo usuário e o ID do tipo que está sendo editado.
                    TipoLivro loTipo = new TipoLivro(ldcIdTipo, lsDescricaoTipo);
                    //Chamando o método de atualização do DAO.
                    this.ioTipoDAO.AtualizarTipo(loTipo);
                    //Alterando a propriedade EditIndex para -1, para indicar que acabou a edição dessa linha.
                    this.gvGerenciamentoCategorias.EditIndex = -1;
                    //Atualizando a lista de tipos na ViewState e os valores do GridView.
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Categoria atualizada com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização da categoria.');</script>");
                }
            }
        }

        protected void gvGerenciamentoCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoCategorias.Rows[e.RowIndex];
                decimal ldcIdTipo = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("lblIdTipo") as
               Label).Text);
                TipoLivro loTipo = this.ioTipoDAO.BuscaTipo(ldcIdTipo).FirstOrDefault();
                if (loTipo != null)
                {
                    //Crie LivrosDAO e o método FindLivrosByTipo() que deve receber um tipo como parâmetro e retornar
                    //uma lista de livros.
                    LivroDAO loLivrosDAO = new LivroDAO();
                    if (loLivrosDAO.FindLivrosByTipo(loTipo.til_id_tipo_livro).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Categoria não pode ser removida pois tem livros associados,se desejar, clique em livros para saber mais.');</script>");
                    }
                    else
                    {
                        this.ioTipoDAO.RemoveTipo(loTipo);
                        HttpContext.Current.Response.Write("<script>alert('Categoria removida com sucesso!');</script>");
                        this.CarregaDados();
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção da categoria selecionada.');</script>");
            }
        }

        public TipoLivro TipoSessao
        {
            get { return (TipoLivro)Session["SessionTipoSelecionado"]; }
            set { Session["SessionTipoSelecionado"] = value; }
        }

        protected void gvGerenciamentoCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosTipo":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdTipo = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[liRowIndex].FindControl("lblIdTipo") as Label).Text);
                    string lsDescricaoTipo = (this.gvGerenciamentoCategorias.Rows[liRowIndex].FindControl("lblNomeTipo") as Label).Text;
                    
                    TipoLivro loTipo = new TipoLivro(ldcIdTipo, lsDescricaoTipo);

                    this.TipoSessao = loTipo;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;

            }
        }
        protected void gvGerenciamentoCategorias_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvGerenciamentoCategorias.PageIndex = e.NewPageIndex;
            CarregaDados();
        }
    }


}
