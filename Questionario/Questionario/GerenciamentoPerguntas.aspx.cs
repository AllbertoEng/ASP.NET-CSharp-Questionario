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
    public partial class GerenciamentoPerguntas : System.Web.UI.Page
    {
        QuestionariosDAO ioQuestionariosDAO = new QuestionariosDAO();
        PerguntasDAO ioPerguntasDAO = new PerguntasDAO();

        public Questionarios QuestionarioSessao
        {
            get { return (Questionarios)Session["SessionQuestionarioSelecionado"]; }
            set { Session["SessionQuestionarioSelecionado"] = value; }
        }

        public BindingList<Questionarios> ListaQuestionarios
        {
            get
            {
                if ((BindingList<Questionarios>)ViewState["ViewStateListaQuestionarios"] == null)
                {
                    this.CarregaDropdownQuestionarios();
                }
                return (BindingList<Questionarios>)ViewState["ViewStateListaQuestionarios"];
            }
            set
            {
                ViewState["ViewStateListaQuestionarios"] = value;
            }
        }

        public BindingList<Perguntas> ListaPerguntas
        {
            get
            {
                if ((BindingList<Perguntas>)ViewState["ViewStateListaPerguntas"] == null)
                {
                    this.CarregarDados();
                }
                return (BindingList<Perguntas>)ViewState["ViewStateListaPerguntas"];
            }
            set
            {
                ViewState["ViewStateListaPerguntas"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.QuestionarioSessao == null)
                {
                    this.CarregaDropdownQuestionarios();
                    this.CarregarDados();
                }
                else
                {
                    this.CarregaDropdownQuestionarios();
                    this.CarregarDados();
                    this.QuestionarioSessao = null;
                }

            }
        }

        protected void ddListQuestionarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal idQuestionario = Convert.ToDecimal(this.ddListQuestionarios.SelectedItem.Value);
            Questionarios questionario = this.ioQuestionariosDAO.BuscaQuestionarios(idQuestionario).FirstOrDefault();
            this.CarregarDados(idQuestionario);

            if (questionario.qst_tp_questionario == "A")
            {
                this.ddCadastroTipoPergunta.SelectedIndex = 1;
                this.ddCadastroTipoPergunta.Enabled = false;
            }
            else
            {
                this.ddCadastroTipoPergunta.SelectedIndex = 0;
                this.ddCadastroTipoPergunta.Enabled = true;
            }
        }

        protected void CarregarDados(decimal? idQuestionario = null)
        {
            try
            {
                if (idQuestionario == null)
                {
                    this.ListaPerguntas = ioPerguntasDAO.BuscaPerguntas();
                }
                else
                {
                    this.ListaPerguntas = ioPerguntasDAO.BuscarPerguntarPorQuestionario(idQuestionario);
                }
                
                this.gvGerenciamentoPerguntas.DataSource = this.ListaPerguntas;
                this.gvGerenciamentoPerguntas.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar perguntas.');</script>");
            }
        }
        protected void CarregaDropdownQuestionarios()
        {
            try
            {
                this.ListaQuestionarios = ioQuestionariosDAO.BuscaQuestionarios();

                this.ddListQuestionarios.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                foreach (Questionarios questionario in ListaQuestionarios)
                {
                    this.ddListQuestionarios.Items.Add(new ListItem(questionario.qst_nm_questionario, questionario.qst_id_questionario.ToString()));
                }

                this.ddListQuestionarios.SelectedIndex = 0;
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Questionarios.');</script>");
            }
        }       

        public void BtnNovoQuestionario_Click(object sender, EventArgs e)
        {
            try
            {
                decimal perIdPergunta;
                try
                {
                    perIdPergunta = this.ListaPerguntas.OrderByDescending(p => p.per_id_pergunta).FirstOrDefault().per_id_pergunta + 1;
                }
                catch
                {
                    perIdPergunta = 1;
                }

                decimal perIdQuestionario = Convert.ToDecimal(this.ddListQuestionarios.SelectedItem.Value);
                string perDsPergunta = this.tbxCadastroDescricao.Text;
                string perTpPergunta = this.ddCadastroTipoPergunta.SelectedItem.Value;
                string perChRespostaObrigatoria = this.ddCadastroObrigatoriedade.SelectedItem.Value;
                int perNuOrdem = Convert.ToInt32(this.tbxCadastroOrdem.Text);

                if (ioPerguntasDAO.BuscarPerguntasPorOrdem(perNuOrdem, perIdQuestionario).Count == 0)
                {
                    Perguntas pergunta = new Perguntas(perIdPergunta, perIdQuestionario,
                    perDsPergunta, perTpPergunta, perChRespostaObrigatoria, perNuOrdem);

                    this.ioPerguntasDAO.InserePergunta(pergunta);
                    this.CarregarDados();
                    HttpContext.Current.Response.Write("<script>alert('Pergunta cadastrada com sucesso!');</script>");

                    this.ddListQuestionarios.SelectedIndex = 0;
                    this.tbxCadastroDescricao.Text = string.Empty;
                    this.ddCadastroTipoPergunta.SelectedIndex = 0;
                    this.ddCadastroObrigatoriedade.SelectedIndex = 0;
                    this.tbxCadastroOrdem.Text = string.Empty;
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>alert('Perguntas do mesmo questionário não podem possuir a mesma informação no campo Ordem.');</script>");                   
                }                
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro ao cadastrar pergunta!');</script>");
            }
        }
        public void gvGerenciamentoPerguntas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoPerguntas.EditIndex = -1;
            this.CarregarDados();
        }
        public void gvGerenciamentoPerguntas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoPerguntas.EditIndex = e.NewEditIndex;
            this.CarregarDados();
        }
        public void gvGerenciamentoPerguntas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal perIdPergunta = Convert.ToDecimal((this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblEditIdPergunta") as Label).Text);
            decimal perIdQuestionario = Convert.ToDecimal((this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblEditIdQuestionario") as Label).Text);
            string perDsPergunta = (this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("tbxEditDsPergunta") as TextBox).Text;
            string perTpPergunta = (this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("ddEditCadastroTipoPergunta") as DropDownList).SelectedItem.Value;
            string perChRespostaObrigatoria = (this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("ddEditCadastroObrigatoriedadePergunta") as DropDownList).SelectedItem.Value;
            int perNuOrdem = Convert.ToInt32((this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("tbxEditOrdem") as TextBox).Text);

            Questionarios questionario = this.ioQuestionariosDAO.BuscaQuestionarios(perIdQuestionario).FirstOrDefault();
            if (questionario.qst_tp_questionario == "A" && perTpPergunta == "M")
            {
                HttpContext.Current.Response.Write("<script>alert('Um Questionário de Avaliação não pode conter Perguntas de múltipla escolha.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(perDsPergunta))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite a descrição da pergunta.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(perDsPergunta))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite a descrição da pergunta.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(perTpPergunta))
            {
                HttpContext.Current.Response.Write("<script>alert('Selecione o tipo da pergunta.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(perChRespostaObrigatoria))
            {
                HttpContext.Current.Response.Write("<script>alert('Selecione a obrigatoriedade da pergunta.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(perNuOrdem.ToString()))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite a ordem da pergunta.');</script>");
            }
            else if (ioPerguntasDAO.BuscarPerguntasPorOrdem(perNuOrdem, perIdQuestionario).Count != 0)
            {
                HttpContext.Current.Response.Write("<script>alert('Perguntas do mesmo questionário não podem possuir a mesma informação no campo Ordem.');</script>");
            }
            else
            {
                try
                {
                    Perguntas pergunta = new Perguntas(perIdPergunta, perIdQuestionario,
                    perDsPergunta, perTpPergunta, perChRespostaObrigatoria, perNuOrdem);

                    this.ioPerguntasDAO.AtualizaPergunta(pergunta);
                    this.gvGerenciamentoPerguntas.EditIndex = -1;
                    this.CarregarDados();
                    HttpContext.Current.Response.Write("<script>alert('Pergunta atualizada com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao atualizar pergunta.');</script>");
                }
            }
        }
        public void gvGerenciamentoPerguntas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal perIdPergunta = Convert.ToDecimal((this.gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblIdPergunta") as Label).Text);
                Perguntas pergunta = this.ioPerguntasDAO.BuscaPerguntas(perIdPergunta).FirstOrDefault();

                if (pergunta != null)
                {
                    OpcoesRespostaDAO ioRespostasDAO = new OpcoesRespostaDAO();
                    if (ioRespostasDAO.BuscarRespostasPorPergunta(pergunta).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover pergunta pois ela possue respostas associadas.');</script>");
                    }
                    else
                    {
                        this.ioPerguntasDAO.RemovePergunta(pergunta);
                        this.CarregarDados();
                        HttpContext.Current.Response.Write("<script>alert('Pergunta removida com sucesso.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao remover pergunta!');</script>");
            }
        }
        public void gvGerenciamentoPerguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}