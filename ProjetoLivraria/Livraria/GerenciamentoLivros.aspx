<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="row" style ="text-align:left;">
        <h2>Cadastro de novo livro</h2>
        <table>
            <tr style="display:grid;">
                <td>
                    <asp:Label ID="lblCadastroNomeLivro" runat="server" Font-Size="16pt" Text="Nome: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeLivro" runat="server" CssClass="form-control" Height="35px" Width="400px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="tbxCadastroNomeLivro"
                Style="color:red;" ErrorMessage="*Digite o nome do livro" ></asp:RequiredFieldValidator>
                </td>
                <td>
    <asp:Label ID="lblCadastroPrecoLivro" runat="server" Font-Size="16pt" Text="Preço: "></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtCadastroPrecoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtCadastroPrecoLivro" Style="color:red;" ErrorMessage="* Digite o preço do livro"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegexValidatorPreco" runat="server" Display="Dynamic" ControlToValidate="txtCadastroPrecoLivro" ValidationExpression="\d{1,6}(,\d{1,2})?" ErrorMessage="* Formato de preço inválido(0,00)" ForeColor="Red"></asp:RegularExpressionValidator>
</td>
                <td>
                    <asp:Label ID="lblCadastroRoyaltyLivro" runat="server" Font-Size="16pt" Text="Royalty: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="Dynamic" ControlToValidate="txtCadastroRoyaltyLivro" Style="color:red;" ErrorMessage="* Digite o royalty do livro"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegexRoyalty" runat="server"  Display="Dynamic" ControlToValidate="txtCadastroRoyaltyLivro"
    ValidationExpression="^(100(\,0{1,2})?|\d{1,2}(\,\d{1,2})?)$" ErrorMessage="* Formato de Royalty inválido, informe um valor entre 0 e 100%" ForeColor="Red">
</asp:RegularExpressionValidator>


                </td>
                <td>
    <asp:Label ID="lblCadastroResumoLivro" runat="server" Font-Size="16pt" Text="Resumo: "></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtCadastroResumoLivro" runat="server" CssClass="form-control" Height="35px" Width="281px" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="Dynamic" ControlToValidate="txtCadastroResumoLivro" Style="color:red;" ErrorMessage="* Digite o resumo do livro"></asp:RequiredFieldValidator>
</td>
<td>
    <asp:Label ID="lblCadastroEdicaoLivro" runat="server" Font-Size="16pt" Text="Edição: "></asp:Label>
</td>
                <td>
                    <asp:TextBox ID="txtCadastroEdicaoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  Display="Dynamic" ControlToValidate="txtCadastroEdicaoLivro" Style="color:red;" ErrorMessage="* Digite a edição do livro"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"  Display="Dynamic" ControlToValidate="txtCadastroEdicaoLivro" ValidationExpression="\d+" ErrorMessage="* Digite um número inteiro" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
                <td>
    <asp:Label ID="lblTipoLivro" runat="server" Font-Size="16pt" Text="Tipo de Livro: "></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlTipoLivro" runat="server" CssClass="form-control" Height="35px" Width="281px" Margin="5px"></asp:DropDownList>
    
</td>

<!-- Adicionar DropDownList para Editor -->
<td>
    <asp:Label ID="lblEditor" runat="server" Font-Size="16pt" Text="Editor: "></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlEditor" runat="server" CssClass="form-control" Height="35px"  Width="281px" Margin="5px"></asp:DropDownList>
   
</td>
   <td>
    <asp:Label ID="lblAutor" runat="server" Font-Size="16pt" Text="Autor: "></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlAutor" runat="server" CssClass="form-control" Height="35px" Width="281px" Margin="5px"></asp:DropDownList>
  
</td>
                <td>
                    <asp:Button ID="btnNovoLivro" runat="server" CssClass="btn btn-success" Style="margin-top:10px" Text="Salvar" OnClick="BtnNovoLivro_Click" />
                </td>

            </tr>
        </table>
    </div>
     <div class="row">
        <h2 style="text-align:center;"> Lista de Livros cadastrados </h2>
        <asp:GridView ID="gvGerenciamentoLivros" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoLivros_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoLivros_RowEditing" OnRowUpdating="gvGerenciamentoLivros_RowUpdating"
            OnRowDeleting="gvGerenciamentoLivros_RowDeleting"  OnRowDataBound="gvGerenciamentoLivros_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvGerenciamentoLivros_PageIndexChanging">  
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdLivro" runat="server" Text='<%# Eval("liv_id_livro") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdLivro" runat="server" Style="width100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblIdLivro" runat="server" Style="text-align:center;" Text='<%# Eval("liv_id_livro") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeLivro" runat="server" CssClass="form-control" Height="35px" Width="170px" MaxLength="30" Text='<%# Eval("liv_nm_titulo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeLivro" runat="server" Style="text-align:center; display:block;"  Text="Título"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblNomeLivro" runat="server" Style="text-align:Left;" Text='<%# Eval("liv_nm_titulo") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
               <asp:TemplateField>
    <EditItemTemplate>
        <asp:TextBox ID="tbxEditPrecoLivro" runat="server" CssClass="form-control" Height="35px" Width="70px" MaxLength="45"  Text='<%# String.Format("{0:0.00}", Eval("liv_vl_preco")) %>'>
             
        </asp:TextBox>
        <asp:RegularExpressionValidator ID="RegexValidatorPreco" runat="server" ValidationGroup="update" Display="Dynamic" ControlToValidate="tbxEditPrecoLivro" ValidationExpression="\d{1,6}(,\d{1,2})?" ErrorMessage="* Formato de preço inválido(0,00)" ForeColor="Red"></asp:RegularExpressionValidator>
    </EditItemTemplate>
    <HeaderTemplate>
        <asp:Label ID="lblTextoPrecoLivro" runat="server" Style="text-align:center; display:block;" Text="Preço"></asp:Label>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="lblPrecoLivro" runat="server" Style="text-align:center;" Text='<%# String.Format("R$ {0:0.00}", Eval("liv_vl_preco")) %>'></asp:Label>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="50px" />
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>

               <asp:TemplateField>
    <EditItemTemplate>
        <asp:TextBox ID="tbxEditRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Width="70px" Text='<%# String.Format("{0:0.00}",Eval("liv_pc_royalty")) %>'></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegexRoyalty" runat="server" ValidationGroup="update" Display="Dynamic" ControlToValidate="tbxEditRoyaltyLivro"
    ValidationExpression="^(100(\,0{1,2})?|\d{1,2}(\,\d{1,2})?)$" ErrorMessage="* Formato de Royalty inválido, informe um valor entre 0 e 100%" ForeColor="Red">
</asp:RegularExpressionValidator>
    </EditItemTemplate>
    <HeaderTemplate>
        <asp:Label ID="lblTextoRoyaltyLivro" runat="server" Style="text-align:center; display:block;"  Text="Royalty"></asp:Label>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="lblRoyaltyLivro" runat="server" Style="text-align:center;">
            <%# String.Format("{0:0.00}", Eval("liv_pc_royalty")) %>
          
            <asp:Literal ID="litRoyaltySymbol" runat="server" Text="%"></asp:Literal>
        </asp:Label>
    </ItemTemplate>
    <HeaderStyle HorizontalAlign="Center" Width="50px" />
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
                 <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditResumoLivro" runat="server" CssClass="form-control" Height="35px" textmode = multiline Width="200px" MaxLength="50" Text='<%# Eval("liv_ds_resumo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoResumoLivro" runat="server" Style="text-align:center; display:block;"  Text="Resumo"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblResumoLivro" runat="server" Style="text-align:center; display:block; word-break: break-all; word-wrap: break-word; white-space: normal;" Text='<%# Eval("liv_ds_resumo") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" Wrap="true" />

                </asp:TemplateField>
                 <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEdicaoLivro" runat="server" CssClass="form-control" Height="35px" Width="40px" MaxLength="50" Text='<%# Eval("liv_nu_edicao") %>'></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="update" Display="Dynamic" ControlToValidate="tbxEditEdicaoLivro" ValidationExpression="\d+" ErrorMessage="* Digite um número inteiro" ForeColor="Red"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEdicaoLivro" runat="server" Style="text-align:center; display:block;" Text="Edição"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblEdicaoLivro" runat="server" Style="text-align:center;" Text='<%# Eval("liv_nu_edicao") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>

                <asp:TemplateField>
<EditItemTemplate>
<asp:DropDownList ID="ddlEditEditor" CssClass="form-control" Height="35px" Width="170px" runat="server"></asp:DropDownList>
</EditItemTemplate>
<HeaderTemplate>
<asp:Label ID="lblTextoEditor" runat="server" Style="text-align:center; display:block;" Text="Editor"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lblEditor" runat="server" Style="text-align:center;" Text='<%# this.ioEditoresDAO.BuscaEditores(Convert.ToDecimal(Eval("liv_id_editor"))).FirstOrDefault()?.edi_nm_editor ?? "" %>'> </asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" Width="100px" />
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
                <asp:TemplateField>
<EditItemTemplate>
<asp:DropDownList ID="ddlEditTipoLivro" CssClass="form-control" Height="35px" Width="170px" runat="server"></asp:DropDownList>
</EditItemTemplate>
<HeaderTemplate>
<asp:Label ID="lblTextoTipoLivro" runat="server" Style="text-align:center; display:block;" Text="Categoria"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lblTipoLivro" runat="server" Style="text-align:center;" Text='<%# this.ioTipoLivrosDAO.BuscaTipo(Convert.ToDecimal(Eval("liv_id_tipo_livro"))).FirstOrDefault()?.til_ds_descricao ?? "" %>'> </asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" Width="100px" />
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
            <asp:TemplateField>
<EditItemTemplate>
<asp:DropDownList  ID="ddlEditAutorLivro" CssClass="form-control" runat="server" Width="170px" Height="35px">
</asp:DropDownList>
</EditItemTemplate>
<HeaderTemplate>
<asp:Label ID="lblTextoAutor" runat="server" Style="text-align:center; display:block;" Text="Autor"></asp:Label>
</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lblAutor" runat="server" Style="text-align:left;" Text='<%# this.ioAutoresDAO.FindAutorByLivros(Convert.ToDecimal(Eval("liv_id_livro"))).FirstOrDefault()?.aut_nm_nome ?? "" %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" ValidationGroup="update" CommandName="Update"  CssClass="btn btn-success"  Text="Atualizar" CausesValidation="true" />
                      <asp:Button ID="btnCancelar" runat="server" CommandArgument="Cancel" style="margin-top: 10px;" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" CommandName="Cancel" />

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarLivro" runat="server" CssClass="btn btn-warning" Text="Editar" CommandName="Edit"  CausesValidation="false" />
                        <asp:Button ID="btnDeletarLivro" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja deletar?');"  />
                       

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"   />
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
