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
    public partial class GerenciamentoRespostas : System.Web.UI.Page
    {
        QuestionariosDAO ioQuestionariosDAO = new QuestionariosDAO();
        PerguntasDAO ioPerguntasDAO = new PerguntasDAO();
        OpcoesRespostaDAO ioRespostasDAO = new OpcoesRespostaDAO();

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
                    this.CarregaDropdownPerguntas();
                }
                return (BindingList<Perguntas>)ViewState["ViewStateListaPerguntas"];
            }
            set
            {
                ViewState["ViewStateListaPerguntas"] = value;
            }
        }

        public BindingList<OpcoesResposta> ListaRespostas
        {
            get
            {
                if ((BindingList<OpcoesResposta>)ViewState["ViewStateListaRespostas"] == null)
                {
                    this.CarregaDropdownPerguntas();
                }
                return (BindingList<OpcoesResposta>)ViewState["ViewStateListaRespostas"];
            }
            set
            {
                ViewState["ViewStateListaRespostas"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CarregaDropdownQuestionarios();
                this.CarregaDropdownPerguntas();
                this.CarregarDados();
            }
        }

        protected void CarregarDados()
        {
            try
            {
                this.ListaRespostas = ioRespostasDAO.BuscaRespostas();
                this.gvGerenciamentoRespostas.DataSource = this.ListaRespostas;
                this.gvGerenciamentoRespostas.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar perguntas.');</script>");
            }
        }

        protected void CarregaDropdownPerguntas()
        {
            try
            {
                this.ListaPerguntas = ioPerguntasDAO.BuscaPerguntas();

                this.ddListPerguntas.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                foreach (Perguntas pergunta in ListaPerguntas)
                {
                    this.ddListPerguntas.Items.Add(new ListItem(pergunta.per_ds_pergunta, pergunta.per_id_pergunta.ToString()));
                }

                this.ddListPerguntas.SelectedIndex = 0;
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Perguntas.');</script>");
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

        protected void BtnNovaResposta_Click(object sender, EventArgs e)
        {
            try
            {
                decimal oprIdOpcaoResposta;
                try
                {
                    oprIdOpcaoResposta = this.ListaRespostas.OrderByDescending(r => r.opr_id_opcao_resposta).FirstOrDefault().opr_id_opcao_resposta + 1;
                }
                catch
                {
                    oprIdOpcaoResposta = 1;
                }

                decimal oprIdPergunta = Convert.ToDecimal(this.ddListPerguntas.SelectedItem.Value);
                string oprDsOpcaoResposta = this.tbxCadastroDescricao.Text;
                string oprChRespostaCorreta = this.ddCadastroRespostaCorreta.SelectedItem.Value;
                int oprNuOrdem = Convert.ToInt32(this.tbxCadastroOrdem.Text);

                if (ioRespostasDAO.BuscarRespostasPorOrdem(oprNuOrdem, oprIdPergunta).Count == 0)
                {
                    OpcoesResposta resposta = new OpcoesResposta(oprIdOpcaoResposta, oprIdPergunta,
                    oprDsOpcaoResposta, oprChRespostaCorreta, oprNuOrdem);

                    this.ioRespostasDAO.InsereResposta(resposta);
                    this.CarregarDados();
                    HttpContext.Current.Response.Write("<script>alert('Resposta cadastrada com sucesso!');</script>");

                    this.ddListPerguntas.SelectedIndex = 0;
                    this.tbxCadastroDescricao.Text = string.Empty;
                    this.ddCadastroRespostaCorreta.SelectedIndex = 0;
                    this.tbxCadastroOrdem.Text = string.Empty;
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>alert('Opções de resposta da mesma Pergunta não podem possuir a mesma informação no campo Ordem.');</script>");
                }                
                    
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro ao cadastrar resposta!');</script>");
            }
        }
        protected void gvGerenciamentoRespostas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoRespostas.EditIndex = -1;
            this.CarregarDados();
        }
        protected void gvGerenciamentoRespostas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoRespostas.EditIndex = e.NewEditIndex;
            this.CarregarDados();
        }
        protected void gvGerenciamentoRespostas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                decimal oprIdOpcaoResposta = Convert.ToDecimal((this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("lblEditIdResposta") as Label).Text);
                decimal oprIdPergunta = Convert.ToDecimal((this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("lblEditIdPergunta") as Label).Text);
                string oprDsOpcaoResposta = (this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("tbxEditDsResposta") as TextBox).Text;
                string oprChRespostaCorreta = (this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("ddEditCadastroRespostaCorreta") as DropDownList).SelectedItem.Value;
                int oprNuOrdem = Convert.ToInt32((this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("tbxEditOrdem") as TextBox).Text);
                if (String.IsNullOrWhiteSpace(oprDsOpcaoResposta))
                {
                    HttpContext.Current.Response.Write("<script>alert('Digite a descrição da resposta.');</script>");
                }
                else if (String.IsNullOrWhiteSpace(oprChRespostaCorreta))
                {
                    HttpContext.Current.Response.Write("<script>alert('Selecione se esta é a resposta correta.');</script>");
                }
                else if (String.IsNullOrWhiteSpace(oprNuOrdem.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Selecione a ordem da resposta.');</script>");
                }
                else
                {
                    try
                    {
                        OpcoesResposta resposta = new OpcoesResposta(oprIdOpcaoResposta, oprIdPergunta,
                        oprDsOpcaoResposta, oprChRespostaCorreta, oprNuOrdem);

                        this.ioRespostasDAO.AtualizaResposta(resposta);
                        this.gvGerenciamentoRespostas.EditIndex = -1;
                        this.CarregarDados();
                        HttpContext.Current.Response.Write("<script>alert('Resposta atualizada com sucesso!');</script>");
                    }
                    catch
                    {
                        HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao atualizar resposta.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Informações inválidas!');</script>");
            }                  
        }
        protected void gvGerenciamentoRespostas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal oprIdOpcaoResposta = Convert.ToDecimal((this.gvGerenciamentoRespostas.Rows[e.RowIndex].FindControl("lblIdResposta") as Label).Text);
                OpcoesResposta resposta = this.ioRespostasDAO.BuscaRespostas(oprIdOpcaoResposta).FirstOrDefault();

                if (resposta != null)
                {
                    this.ioRespostasDAO.RemoveResposta(resposta);
                    this.CarregarDados();
                    HttpContext.Current.Response.Write("<script>alert('Resposta removida com sucesso.');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Ocorreu um erro ao remover resposta!');</script>");
            }
        }
    }
}