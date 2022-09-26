<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoQuestionarios.aspx.cs" Inherits="Questionario.Questionario.GerenciamentoQuestionarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row" style="text-align: left;">
        <h2>Cadastro de novo Questionário</h2>
        <table>
            <tr style="display: grid;">                
                <td>
                    <asp:Label ID="lblCadastroNomeQuestionario" runat="server" Font-Size="16pt" Text="Nome: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeQuestionario" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeQuestionario"
                        Style="color: red;" ErrorMessage="* Digite o nome do questionário."></asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Label ID="lblCadastroTipoQuestionario" runat="server" Font-Size="16pt" Text="Tipo: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddCadastroTipoQuestionario" runat="server" CssClass="form-control" Height="35px" Width="400px">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="P"> Pesquisa </asp:ListItem>
                        <asp:ListItem Value="A"> Avaliação </asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddCadastroTipoQuestionario"
                        Style="color: red;" ErrorMessage="* Selecione o tipo do questionário."></asp:RequiredFieldValidator>
                </td>


                <td>
                    <asp:Label ID="lblCadastroLinkInstrucoes" runat="server" Font-Size="16pt" Text="Instruções: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroLinkInstrucoes" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="tbxCadastroLinkInstrucoes"
                        ValidationExpression="^((H|h)(T|t)(T|t)(P|p):\/\/)\w{0,100}" 
                         Style="color: red;" ErrorMessage="* Um Questionário não pode possuir link de instrução que não inicie com “http://”." ></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxCadastroLinkInstrucoes"
                        Style="color: red;" ErrorMessage="* Digite o link contendo as instruções do questionário."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoQuestionario" runat="server" CssClass="btn btn-sucess" Style="margin-top: 10px" Text="Salvar"
                        OnClick="BtnNovoQuestionario_Click" />
                </td>
            </tr>
        </table>     
    </div>

    <div class="row">
            <h2 style="text-align: center;">Lista de questionários cadastrados</h2>
             <asp:GridView ID="gvGerenciamentoQuestionarios" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" OnRowCancelingEdit="gvGerenciamentoQuestionarios_RowCancelingEdit" OnRowEditing="gvGerenciamentoQuestionarios_RowEditing"
            OnRowUpdating ="gvGerenciamentoQuestionarios_RowUpdating" OnRowDeleting="gvGerenciamentoQuestionarios_RowDeleting" 
            OnRowCommand="gvGerenciamentoQuestionarios_RowCommand">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdQuestionario" runat="server" Text='<%# Eval("qst_id_questionario") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdQuestionario" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_id_questionario") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeQuestionario" runat="server" Text='<%# Eval("qst_nm_questionario") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeQuestionario" runat="server" Style="width: 100%" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomeQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_nm_questionario") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddCadastroTipoQuestionario" runat="server"  Text='<%# Eval("qst_tp_questionario") %>'>
                            <asp:ListItem Selected="True" Value=""></asp:ListItem>
                            <asp:ListItem Value="P"> Pesquisa </asp:ListItem>
                            <asp:ListItem Value="A"> Avaliação </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoTipoQuestionario" runat="server" Style="width: 100%" Text="Tipo"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTipoQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_tp_questionario") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditLinkQuestionario" runat="server" Text='<%# Eval("qst_ds_link_instrucoes") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoLinkQuestionario" runat="server" Style="width: 100%" Text="Instruções"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLinkQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_ds_link_instrucoes") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sucess" Text="Atualizar" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarQuestionario" runat="server" CommandName="Edit" CssClass="btn btn-sucess" Text="Editar" CausesValidation="false" />
                        <asp:Button ID="btnDeletarQuestionario" runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Deletar" CausesValidation="false" />
                        <asp:Button ID="btnCarregaPerguntasQuestionario" runat="server" CommandName="CarregaPerguntasQuestionario" CssClass="btn btn-primary" 
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Perguntas" CausesValidation="false" />&nbsp;
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                </asp:TemplateField>

            </Columns>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" Font-Size="14px" />
            <FooterStyle BackColor="#507CD1" Font-Bold="true" ForeColor="White" />
            <HeaderStyle HorizontalAlign="Center" Wrap="true" BackColor="#507CD1" Font-Bold="true" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" BackColor="#F5F7FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="true" ForeColor="#333333" Font-Size="14px" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</asp:Content>
