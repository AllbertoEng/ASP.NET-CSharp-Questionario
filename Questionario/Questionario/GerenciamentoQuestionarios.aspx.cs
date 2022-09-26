using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questionario.DAO;
using Questionario.Models;

namespace Questionario.Questionario
{
    public partial class GerenciamentoQuestionarios : System.Web.UI.Page
    {
        QuestionariosDAO ioQuestionariosDAO = new QuestionariosDAO();

        public BindingList<Questionarios> ListaQuestionarios
        {
            get
            {
                if ((BindingList<Questionarios>)ViewState["ViewStateListaQuestionarios"] == null)
                {
                    this.CarregarDados();
                }
                return (BindingList<Questionarios>)ViewState["ViewStateListaQuestionarios"];
            }
            set
            {
                ViewState["ViewStateListaQuestionarios"] = value;
            }
        }

        public Questionarios QuestionarioSessao
        {
            get { return (Questionarios)Session["SessionQuestionarioSelecionado"]; }
            set { Session["SessionQuestionarioSelecionado"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CarregarDados();
            }
        }

        protected void CarregarDados()
        {
            try
            {
                this.ListaQuestionarios = this.ioQuestionariosDAO.BuscaQuestionarios();
                this.gvGerenciamentoQuestionarios.DataSource = this.ListaQuestionarios.OrderBy(questionario => questionario.qst_nm_questionario);
                this.gvGerenciamentoQuestionarios.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Questionarios.');</script>");
            }            
        }

        protected void BtnNovoQuestionario_Click(object sender, EventArgs e)
        {
            try
            {
                decimal idQuestionario;
                try
                {
                    idQuestionario = this.ListaQuestionarios.OrderByDescending(questionario => questionario.qst_id_questionario).FirstOrDefault().qst_id_questionario + 1;
                }
                catch
                {
                    idQuestionario = 1;
                }                
                string nomeQuestionario = this.tbxCadastroNomeQuestionario.Text;
                string tipoQuestionario = this.ddCadastroTipoQuestionario.SelectedItem.Value;
                string linkQuestionario = this.tbxCadastroLinkInstrucoes.Text.ToLower();

                Questionarios novoQuestionario = new Questionarios(idQuestionario, nomeQuestionario, tipoQuestionario, linkQuestionario);

                this.ioQuestionariosDAO.InsereQuestionario(novoQuestionario);
                this.CarregarDados();
                HttpContext.Current.Response.Write("<script>alert('Questionário cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro ao cadastrar questionário!');</script>");
            }
            this.tbxCadastroNomeQuestionario.Text = string.Empty;
            this.ddCadastroTipoQuestionario.SelectedIndex = 0;
            this.tbxCadastroLinkInstrucoes.Text = string.Empty;
                        
        }

        protected void gvGerenciamentoQuestionarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoQuestionarios.EditIndex = -1;
            this.CarregarDados();
        }
        protected void gvGerenciamentoQuestionarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoQuestionarios.EditIndex = e.NewEditIndex;
            this.CarregarDados();
        }
        protected void gvGerenciamentoQuestionarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal idQuestionario = Convert.ToDecimal((this.gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("lblEditIdQuestionario") as Label).Text);
            string nomeQuestionario = (this.gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("tbxEditNomeQuestionario") as TextBox).Text;
            string tipoQuestionario = ((this.gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("ddCadastroTipoQuestionario") as DropDownList).SelectedItem.Value).ToLower();
            string linkQuestionario = (this.gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("tbxEditLinkQuestionario") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(nomeQuestionario))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do questionário.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(tipoQuestionario))
            {
                HttpContext.Current.Response.Write("<script>alert('Selecione o tipo do questionário.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(linkQuestionario))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite o link de instruções do questionário.');</script>");
            }
            else
            {
                try
                {
                    Questionarios questionario = new Questionarios(idQuestionario, nomeQuestionario, tipoQuestionario, linkQuestionario);

                    this.ioQuestionariosDAO.AtualizaQuestionario(questionario);
                    this.gvGerenciamentoQuestionarios.EditIndex = -1;
                    this.CarregarDados();
                    HttpContext.Current.Response.Write("<script>alert('Questionário atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao atualizar questionário!');</script>");
                }
            }


        }
        protected void gvGerenciamentoQuestionarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal idQuestionario = Convert.ToDecimal((this.gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("lblIdQuestionario") as Label).Text);
                Questionarios questionario = this.ioQuestionariosDAO.BuscaQuestionarios(idQuestionario).FirstOrDefault();

                if (questionario != null)
                {
                    PerguntasDAO ioPerguntasDAO = new PerguntasDAO();
                    if (ioPerguntasDAO.BuscarPerguntarPorQuestionario(questionario.qst_id_questionario).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover questionário pois ele possue perguntas associadas.');</script>");
                    }
                    else
                    {
                        this.ioQuestionariosDAO.RemoveQuestionario(questionario);
                        this.CarregarDados();
                        HttpContext.Current.Response.Write("<script>alert('Questionário removido com sucesso.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao remover questionário!');</script>");
            }            
        }
        protected void gvGerenciamentoQuestionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
              switch (e.CommandName)
            {
                case "CarregaPerguntasQuestionario":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal idQuestionario = Convert.ToDecimal((this.gvGerenciamentoQuestionarios.Rows[liRowIndex].FindControl("lblIdQuestionario") as Label).Text);
                    string nomeQuestionario = (this.gvGerenciamentoQuestionarios.Rows[liRowIndex].FindControl("lblNomeQuestionario") as Label).Text;
                    string tipoQuestionario = (this.gvGerenciamentoQuestionarios.Rows[liRowIndex].FindControl("lblTipoQuestionario") as Label).Text;
                    string linkQuestionario = (this.gvGerenciamentoQuestionarios.Rows[liRowIndex].FindControl("lblLinkQuestionario") as Label).Text;

                    Questionarios questionario = new Questionarios(idQuestionario, nomeQuestionario, tipoQuestionario, linkQuestionario);

                    this.QuestionarioSessao = questionario;

                    Response.Redirect("/Questionario/GerenciamentoPerguntas");
                    break;
                default:
                    break;
            }
             
        }
    }
}