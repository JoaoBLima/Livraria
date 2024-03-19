<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class ="row" style ="text-align:left;">
        <h2>Cadastro de nova categoria</h2>
        <table>
            <tr style="display:grid;">
                <td>
                    <asp:Label ID="lblCadastroDescricaoTipo" runat="server" Font-Size="16pt" Text="Descrição: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroDescricaoTipo" runat="server" CssClass="form-control" Height="35px" Width="400px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroDescricaoTipo"
                Style="color:red;" ErrorMessage="*Digite a descricao da categoria" ></asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:Button ID="btnNovoTipo" runat="server" CssClass="btn btn-success" Style="margin-top:10px" Text="Salvar" OnClick="BtnNovoTipo_Click" />
                </td>
                  </tr>
        </table>
    </div>
     <div class="row">
        <h2 style="text-align:center;"> Lista de Categorias cadastradas </h2>
        <asp:GridView ID="gvGerenciamentoCategorias" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoCategorias_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoCategorias_RowEditing" OnRowUpdating="gvGerenciamentoCategorias_RowUpdating"
            OnRowDeleting="gvGerenciamentoCategorias_RowDeleting" OnRowCommand="gvGerenciamentoCategorias_RowCommand" AllowPaging="true" OnPageIndexChanging="gvGerenciamentoCategorias_PageIndexChanging">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdTipo" runat="server" Text='<%# Eval("til_id_tipo_livro") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdTipo" runat="server" Style="width100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblIdTipo" runat="server" Style="text-align:center;" Text='<%# Eval("til_id_tipo_livro") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeTipo" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("til_ds_descricao") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeTipo" runat="server" Style="text-align=center;" Text="Descricao"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblNomeTipo" runat="server" Style="text-align:Left;" Text='<%# Eval("til_ds_descricao") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />

                </asp:TemplateField>
                 <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" style="margin-right: 10px;" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />
                        <asp:Button ID="btnCancelar" runat="server" CommandArgument="Cancel"  CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" CommandName="Cancel" />

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarTipo" runat="server" CssClass="btn btn-warning" Text="Editar" CommandName="Edit" CausesValidation="false" />
                        <asp:Button ID="btnDeletarTipo" runat="server" CssClass="btn btn-danger" Text="Deletar" OnClientClick="return confirm('Tem certeza que deseja deletar?');" CommandName="Delete" CausesValidation="false" />
                        <asp:Button ID="btnCarregaLivrosTipo" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosTipo" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false" />

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"  />
                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                 </Columns>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" Font-Size="14px" />
            <FooterStyle BackColor="#507cd1" Font-Bold="true" ForeColor="White" />
            <HeaderStyle HorizontalAlign="Center" Wrap="true" BackColor="#507cd1" Font-Bold="true" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="true" ForeColor="#333333" Font-Size="14px" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />


        </asp:GridView>
    </div>
</asp:Content>
