<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoPerguntas.aspx.cs" Inherits="Questionario.Questionario.GerenciamentoPerguntas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de nova Pergunta</h2>
        <table>
            <tr style="display: grid;">   
                <!--Questionário-->
                <td>
                    <asp:Label ID="lblSelecaoQuestionario" runat="server" Font-Size="16pt" Text="Questionário: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddListQuestionarios" runat="server" CssClass="form-control" Height="35px" Width="400px"
                        AutoPostBack="true" OnSelectedIndexChanged="ddListQuestionarios_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddListQuestionarios"
                        Style="color: red;" ErrorMessage="* Selecione o questionário."></asp:RequiredFieldValidator>
                </td>

                <!--Descrição-->
                <td>
                    <asp:Label ID="lblCadastroDescricao" runat="server" Font-Size="16pt" Text="Descrição: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroDescricao" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroDescricao"
                        Style="color: red;" ErrorMessage="* Digite a descrição da pergunta."></asp:RequiredFieldValidator>
                </td>

                <!--Tipo da pergunta-->
                <td>
                    <asp:Label ID="lblCadastroTipoPergunta" runat="server" Font-Size="16pt" Text="Tipo: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddCadastroTipoPergunta" runat="server" CssClass="form-control" Height="35px" Width="400px">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="U"> Única escolha </asp:ListItem>
                        <asp:ListItem Value="M"> Múltipla escolha </asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddCadastroTipoPergunta"
                        Style="color: red;" ErrorMessage="* Selecione o tipo da pergunta."></asp:RequiredFieldValidator>
                </td>

                <!--Obrigatoriedade-->
                <td>
                    <asp:Label ID="lblCadastroObrigatoriedade" runat="server" Font-Size="16pt" Text="Resposta obrigatória: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddCadastroObrigatoriedade" runat="server" CssClass="form-control" Height="35px" Width="400px">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="S"> Sim </asp:ListItem>
                        <asp:ListItem Value="N"> Não </asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddCadastroObrigatoriedade"
                        Style="color: red;" ErrorMessage="* Selecione a obrigatoriedade da pergunta."></asp:RequiredFieldValidator>
                </td>

                <!--Ordem-->
                <td>
                    <asp:Label ID="lblCadastroOrdem" runat="server" Font-Size="16pt" Text="Ordem da pergunta: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroOrdem" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxCadastroOrdem"
                        Style="color: red;" ErrorMessage="* Digite a ordem da pergunta."></asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Button ID="btnNovoQuestionario" runat="server" CssClass="btn btn-sucess" Style="margin-top: 10px" Text="Salvar"
                        OnClick="BtnNovoQuestionario_Click" />
                </td>
            </tr>
        </table>     
    </div>

    <div class="row">
            <h2 style="text-align: center;">Lista de perguntas cadastradas</h2>
             <asp:GridView ID="gvGerenciamentoPerguntas" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" OnRowCancelingEdit="gvGerenciamentoPerguntas_RowCancelingEdit" OnRowEditing="gvGerenciamentoPerguntas_RowEditing"
            OnRowUpdating ="gvGerenciamentoPerguntas_RowUpdating" OnRowDeleting="gvGerenciamentoPerguntas_RowDeleting" 
            OnRowCommand="gvGerenciamentoPerguntas_RowCommand">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdPergunta" runat="server" Text='<%# Eval("per_id_pergunta") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdPergunta" runat="server" Style="width: 100%" Text="ID Pergunta"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_id_pergunta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdQuestionario" runat="server" Text='<%# Eval("per_id_questionario") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdQuestionario" runat="server" Style="width: 100%" Text="ID Questionario"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("per_id_questionario") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditDsPergunta" runat="server" Text='<%# Eval("per_ds_pergunta") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoDsPergunta" runat="server" Style="width: 100%" Text="Descrição"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDsPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_ds_pergunta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddEditCadastroTipoPergunta" runat="server"  Text='<%# Eval("per_tp_pergunta") %>'>
                            <asp:ListItem Selected="True" Value=""></asp:ListItem>
                            <asp:ListItem Value="U"> Única escolha </asp:ListItem>
                            <asp:ListItem Value="M"> Múltipla escolha </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoTipoPergunta" runat="server" Style="width: 100%" Text="Tipo"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTipoPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_tp_pergunta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddEditCadastroObrigatoriedadePergunta" runat="server"  Text='<%# Eval("per_ch_resposta_obrigatoria") %>'>
                            <asp:ListItem Selected="True" Value=""></asp:ListItem>
                            <asp:ListItem Value="S"> Sim </asp:ListItem>
                            <asp:ListItem Value="N"> Não </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoObrigatoriedadePergunta" runat="server" Style="width: 100%" Text="Obrigatoriedade"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblObrigatoriedadePergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_ch_resposta_obrigatoria") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditOrdem" runat="server" Text='<%# Eval("per_nu_ordem") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoOrdem" runat="server" Style="width: 100%" Text="Ordem"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblOrdem" runat="server" Style="text-align: center;" Text='<%# Eval("per_nu_ordem") %>'></asp:Label>
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
                        <asp:Button ID="btnEditarPergunta" runat="server" CommandName="Edit" CssClass="btn btn-sucess" Text="Editar" CausesValidation="false" />
                        <asp:Button ID="btnDeletarPergunta" runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Deletar" CausesValidation="false" />
                        <asp:Button ID="btnCarregaRespostasPergunta" runat="server" CommandName="CarregaRespostasPergunta" CssClass="btn btn-primary" 
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Respostas" CausesValidation="false" />&nbsp;
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
