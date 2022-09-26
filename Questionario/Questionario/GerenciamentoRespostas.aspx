<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoRespostas.aspx.cs" Inherits="Questionario.Questionario.GerenciamentoRespostas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de nova Resposta</h2>
        <table>
            <tr style="display: grid;">   
                <!--Questionário-->
                <td>
                    <asp:Label ID="lblSelecaoQuestionario" runat="server" Font-Size="16pt" Text="Questionário: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddListQuestionarios" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddListQuestionarios"
                        Style="color: red;" ErrorMessage="* Selecione o questionário."></asp:RequiredFieldValidator>
                </td>

                <!--Perguntas-->
                <td>
                    <asp:Label ID="lblSelecaoPergunta" runat="server" Font-Size="16pt" Text="Perguntas: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddListPerguntas" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddListPerguntas"
                        Style="color: red;" ErrorMessage="* Selecione a pergunta."></asp:RequiredFieldValidator>
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

                <!--Correta-->
                <td>
                    <asp:Label ID="lblCadastroRespostaCorreta" runat="server" Font-Size="16pt" Text="Resposta correta: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddCadastroRespostaCorreta" runat="server" CssClass="form-control" Height="35px" Width="400px">
                        <asp:ListItem Selected="True" Value=""></asp:ListItem>
                        <asp:ListItem Value="S"> Sim </asp:ListItem>
                        <asp:ListItem Value="N"> Não </asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddCadastroRespostaCorreta"
                        Style="color: red;" ErrorMessage="* Selecione se esta é a resposta correta."></asp:RequiredFieldValidator>
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
                    <asp:Button ID="btnNovaResposta" runat="server" CssClass="btn btn-sucess" Style="margin-top: 10px" Text="Salvar"
                        OnClick="BtnNovaResposta_Click" />
                </td>
            </tr>
        </table>     
    </div>

    <div class="row">
            <h2 style="text-align: center;">Lista de respostas cadastradas</h2>
             <asp:GridView ID="gvGerenciamentoRespostas" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" OnRowCancelingEdit="gvGerenciamentoRespostas_RowCancelingEdit" OnRowEditing="gvGerenciamentoRespostas_RowEditing"
            OnRowUpdating ="gvGerenciamentoRespostas_RowUpdating" OnRowDeleting="gvGerenciamentoRespostas_RowDeleting">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdResposta" runat="server" Text='<%# Eval("opr_id_opcao_resposta") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdResposta" runat="server" Style="width: 100%" Text="ID Resposta"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdResposta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_id_opcao_resposta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdPergunta" runat="server" Text='<%# Eval("opr_id_pergunta") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdPergunta" runat="server" Style="width: 100%" Text="ID Pergunta"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_id_pergunta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditDsResposta" runat="server" Text='<%# Eval("opr_ds_opcao_resposta") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoDsResposta" runat="server" Style="width: 100%" Text="Descrição"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDsResposta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_ds_opcao_resposta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddEditCadastroRespostaCorreta" runat="server"  Text='<%# Eval("opr_ch_resposta_correta") %>'>
                            <asp:ListItem Selected="True" Value=""></asp:ListItem>
                            <asp:ListItem Value="S"> Sim </asp:ListItem>
                            <asp:ListItem Value="N"> Não </asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoRespostaCorreta" runat="server" Style="width: 100%" Text="Resposta Correta"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRespostaCorreta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_ch_resposta_correta") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditOrdem" runat="server" Text='<%# Eval("opr_nu_ordem") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoOrdem" runat="server" Style="width: 100%" Text="Ordem"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblOrdem" runat="server" Style="text-align: center;" Text='<%# Eval("opr_nu_ordem") %>'></asp:Label>
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
                        <asp:Button ID="btnEditarResposta" runat="server" CommandName="Edit" CssClass="btn btn-sucess" Text="Editar" CausesValidation="false" />
                        <asp:Button ID="btnDeletarResposta" runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Deletar" CausesValidation="false" />
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
