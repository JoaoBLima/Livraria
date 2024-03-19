<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="ProjetoLivraria.Livraria.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container at-5 text-center">
        <div class="row justify-content-center">
            <div class="justify-content-center">
                <h1 class="mb-4">Página de Gerenciamento principal</h1>
                <p>Essa é a página principal da Livraria Online. Selecione uma das opções abaixo:</p>
            </div>
        </div>
        <div class="row at-4 justify-content-center">
            <div class="col-md-3 mb-4">
                <a href="GerenciamentoAutores.aspx" class="btn btn-primary btn-block">Gerenciamento de Autores</a>
            </div>
             <div class="col-md-3 mb-4">
                <a href="GerenciamentoEditores.aspx" class="btn btn-success btn-block">Gerenciamento de Editores</a>
            </div>
             <div class="col-md-3 mb-4">
                <a href="GerenciamentoCategorias.aspx" class="btn btn-info btn-block">Gerenciamento de Categorias</a>
            </div>
             <div class="col-md-3 mb-4">
                <a href="GerenciamentoLivros.aspx" class="btn btn-warning btn-block">Gerenciamento de Livros</a>
            </div>
        </div>
    </div>
</asp:Content>
