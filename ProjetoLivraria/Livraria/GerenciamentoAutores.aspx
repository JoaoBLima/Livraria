<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoAutores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="row" style ="text-align:left;">
        <h2>Cadastro de novo autor</h2>
        <table>
            <tr style="display:grid;">
                <td>
                    <asp:Label ID="lblCadastroNomeAutor" runat="server" Font-Size="16pt" Text="Nome: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeAutor" runat="server" CssClass="form-control" Height="35px" Width="400px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeAutor"
                Style="color:red;" ErrorMessage="*Digite o nome do autor" ></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexNomeAutor" runat="server" ControlToValidate="tbxCadastroNomeAutor"
        ValidationExpression="^[a-zA-ZÀ-ÿ\u00C0-\u017F\s]+$"
        ErrorMessage="*Somente letras são permitidas no nome do autor" 
        Style="color:red;"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroSobrenomeAutor" runat="server" Font-Size="16pt" Text="Sobrenome: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroSobrenomeAutor" runat="server" CssClass="form-control" Height="35px"
                Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCadastroSobrenomeAutor" Style="color:red;" ErrorMessage="* Digite o sobrenome do autor"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegexSobrenomeAutor" runat="server" ControlToValidate="txtCadastroSobrenomeAutor"
        ValidationExpression="^[a-zA-ZÀ-ÿ\u00C0-\u017F\s]+$"
        ErrorMessage="*Somente letras são permitidas no sobrenome do autor" 
        Style="color:red;"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroEmailAutor" runat="server" Font-Size="16pt" Text="Email: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroEmailAutor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCadastroEmailAutor" Style="color:red;" ErrorMessage="* Digite o email do autor"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegexValidatorEmail" runat="server" ControlToValidate="txtCadastroEmailAutor" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="* Formato de e-mail inválido" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoAutor" runat="server" CssClass="btn btn-success" Style="margin-top:10px" Text="Salvar" OnClick="BtnNovoAutor_Click" />
                </td>

            </tr>
        </table>
    </div>
    <div class="row">
        <h2 style="text-align:center;"> Lista de autores cadastrados </h2>
        <asp:GridView ID="gvGerenciamentoAutores" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoAutores_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoAutores_RowEditing" OnRowUpdating="gvGerenciamentoAutores_RowUpdating"
            OnRowDeleting="gvGerenciamentoAutores_RowDeleting" OnRowCommand="gvGerenciamentoAutores_RowCommand" AllowPaging="true" OnPageIndexChanging="gvGerenciamentoAutores_PageIndexChanging">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdAutor" runat="server" Text='<%# Eval("aut_id_autor") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdAutor" runat="server" Style="width:100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblIdAutor" runat="server" Style="text-align:center;" Text='<%# Eval("aut_id_autor") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeAutor" runat="server"  CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("aut_nm_nome") %>'></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegexNomeAutor" ValidationGroup="update" runat="server" Display="Dynamic" ControlToValidate="tbxEditNomeAutor"
        ValidationExpression="^[a-zA-ZÀ-ÿ\u00C0-\u017F\s]+$"
        ErrorMessage="*Somente letras são permitidas no nome do autor" 
        Style="color:red;"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeAutor" runat="server" Style="text-align:center;" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblNomeAutor" runat="server" Style="text-align:Left;" Text='<%# Eval("aut_nm_nome") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditSobrenomeAutor" runat="server" CssClass="form-control" Height="35px" MaxLength="45" Text='<%# Eval("aut_nm_sobrenome") %>'></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegexSobrenomeAutor" ValidationGroup="update" runat="server" Display="Dynamic" ControlToValidate="tbxEditSobrenomeAutor"
        ValidationExpression="^[a-zA-ZÀ-ÿ\u00C0-\u017F\s]+$"
        ErrorMessage="*Somente letras são permitidas no sobrenome do autor" 
        Style="color:red;"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoSobrenomeAutor" runat="server" Style="text-align:center;" Text="Sobrenome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSobrenomeAutor" runat="server" Style="text-align:center;" Text='<%# Eval("aut_nm_sobrenome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
               
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEmailAutor" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("aut_ds_email") %>'></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegexValidatorEmail" ValidationGroup="update" runat="server" Display="Dynamic" ControlToValidate="tbxEditEmailAutor" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="* Formato de e-mail inválido" ForeColor="Red"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEmailAutor" runat="server" Style="text-align:center;" Text="E-mail"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblEmailAutor" runat="server" Style="text-align:center;" Text='<%# Eval("aut_ds_email") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
               
                 <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" ValidationGroup="update" CommandName="Update" style="margin-right: 10px;" CssClass="btn btn-success" Text="Atualizar" CausesValidation="true" />
                       <asp:Button ID="btnCancelar" runat="server" CommandArgument="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" CommandName="Cancel"  />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarAutor" runat="server" CssClass="btn btn-warning" Text="Editar" CommandName="Edit" CausesValidation="false" />
                        <asp:Button ID="btnDeletarAutor" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja deletar?');" />
                        <asp:Button ID="btnCarregaLivrosAutor" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosAutor" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="150px"  />
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
