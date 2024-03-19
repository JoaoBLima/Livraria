﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class ="row" style ="text-align:left;">
        <h2>Cadastro de novo editor</h2>
        <table>
            <tr style="display:grid;">
                <td>
                    <asp:Label ID="lblCadastroNomeEditor" runat="server" Font-Size="16pt" Text="Nome: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeEditor" runat="server" CssClass="form-control" Height="35px" Width="400px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeEditor"
                Style="color:red;" ErrorMessage="*Digite o nome do editor" ></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegexNomeEditor" runat="server" ControlToValidate="tbxCadastroNomeEditor"
        ValidationExpression="^[a-zA-ZÀ-ÿ\u00C0-\u017F\s]+$"
        ErrorMessage="*Somente letras são permitidas no nome do editor" 
        Style="color:red;"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroEmailEditor" runat="server" Font-Size="16pt" Text="Email: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroEmailEditor" runat="server" CssClass="form-control" Height="35px"
                Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCadastroEmailEditor" Style="color:red;" ErrorMessage="* Digite o email do editor"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegexValidatorEmail" runat="server" ControlToValidate="txtCadastroEmailEditor" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="* Formato de e-mail inválido" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
               <td>
    <asp:Label ID="lblCadastroUrlEditor" runat="server" Font-Size="16pt" Text="URL: "></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtCadastroUrlEditor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCadastroUrlEditor" Style="color:red;" ErrorMessage="* Digite a url do editor"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtCadastroUrlEditor" ErrorMessage="URL inválida." ForeColor="Red"
                        ValidationExpression="^(https?://)?([\w-]+\.)+[\w-]+(/[?\w- ./%&=]*)?$"> 
</asp:RegularExpressionValidator>


</td>
                <td>
                    <asp:Button ID="btnNovoEditor" runat="server" CssClass="btn btn-success" Style="margin-top:10px" Text="Salvar" OnClick="BtnNovoEditor_Click" />
                </td>

            </tr>
        </table>
    </div>
     <div class="row">
        <h2 style="text-align:center;"> Lista de Editores cadastrados </h2>
        <asp:GridView ID="gvGerenciamentoEditores" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoEditores_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoEditores_RowEditing" OnRowUpdating="gvGerenciamentoEditores_RowUpdating"
            OnRowDeleting="gvGerenciamentoEditores_RowDeleting" OnRowCommand="gvGerenciamentoEditores_RowCommand" AllowPaging="true" OnPageIndexChanging="gvGerenciamentoEditores_PageIndexChanging">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdEditor" runat="server" Text='<%# Eval("edi_id_editor") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdEditor" runat="server" Style="width100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblIdEditor" runat="server" Style="text-align:center;" Text='<%# Eval("edi_id_editor") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("edi_nm_editor") %>'></asp:TextBox>

                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeEditor" runat="server" Style="text-align=center;" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblNomeEditor" runat="server" Style="text-align:Left;" Text='<%# Eval("edi_nm_editor") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />

                </asp:TemplateField>
              
               
                  <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEmailEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="45" Text='<%# Eval("edi_ds_email") %>'></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegexValidatorEmail" ValidationGroup="update" runat="server" Display="Dynamic" ControlToValidate="tbxEditEmailEditor" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="* Formato de e-mail inválido" ForeColor="Red"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEmailEditor" runat="server" Style="text-align=center;" Text="Email"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmailEditor" runat="server" Style="text-align:center;" Text='<%# Eval("edi_ds_email") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />

                </asp:TemplateField>
                 <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditUrlEditor" runat="server"  CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval(" edi_ds_url") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="update" 
                        ControlToValidate="tbxEditUrlEditor" ErrorMessage="URL inválida." ForeColor="Red"
                        ValidationExpression="^(https?://)?([\w-]+\.)+[\w-]+(/[?\w- ./%&=]*)?$"> 
</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoUrlEditor" runat="server" Style="text-align:center;" Text="URL"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Label ID="lblUrlEditor" runat="server" Style="text-align:center;" Text='<%# Eval(" edi_ds_url") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" />

                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" ValidationGroup="update" style="margin-right: 10px;" CssClass="btn btn-success" Text="Atualizar" CausesValidation="true" />
                        <asp:Button ID="btnCancelar" runat="server" CommandArgument="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" CommandName="Cancel" />

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarEditor" runat="server" CssClass="btn btn-warning" Text="Editar" CommandName="Edit" CausesValidation="false" />
                        <asp:Button ID="btnDeletarEditor" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja deletar?');" />
                        <asp:Button ID="btnCarregaLivrosEditor" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosEditor" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false" />

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
