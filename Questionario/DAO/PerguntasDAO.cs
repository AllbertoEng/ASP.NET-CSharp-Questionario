using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Questionario.Models;

namespace Questionario.DAO
{
    public class PerguntasDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Perguntas> BuscarPerguntasPorOrdem(int perNuOrdem, decimal idQuestionario)
        {
            if (perNuOrdem == null || idQuestionario == null)
            {
                throw new NullReferenceException();
            }
            BindingList<Perguntas> listaPerguntas = new BindingList<Perguntas>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT per_id_pergunta, per_id_questionario, per_ds_pergunta, per_tp_pergunta, per_ch_resposta_obrigatoria, per_nu_ordem " +
                                                 "FROM PER_PERGUNTA_BETO " +
                                                 "INNER JOIN QST_QUESTIONARIO_BETO ON qst_id_questionario = per_id_questionario " +
                                                 "WHERE per_nu_ordem = @perNuOrdem AND qst_id_questionario = @idQuestionario", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@perNuOrdem", perNuOrdem));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", idQuestionario));

                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            Perguntas pergunta = new Perguntas(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2),
                                ioReader.GetString(3), ioReader.GetString(4), ioReader.GetInt32(5));
                            listaPerguntas.Add(pergunta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao encontrar perguntas.");
                }
            }
            return listaPerguntas;
        }

        public BindingList<Perguntas> BuscarPerguntarPorQuestionario(decimal? idQuestionario)
        {
            if (idQuestionario == null)
            {
                throw new NullReferenceException();
            }

            BindingList<Perguntas> listaPerguntas = new BindingList<Perguntas>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT per_id_pergunta, per_id_questionario, per_ds_pergunta, per_tp_pergunta, per_ch_resposta_obrigatoria, per_nu_ordem " +
                                                 "FROM PER_PERGUNTA_BETO " +
                                                 "INNER JOIN QST_QUESTIONARIO_BETO ON qst_id_questionario = per_id_questionario " +
                                                 "WHERE qst_id_questionario = @idQuestionario", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", idQuestionario));

                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            Perguntas pergunta = new Perguntas(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2),
                                ioReader.GetString(3), ioReader.GetString(4), ioReader.GetInt32(5));
                            listaPerguntas.Add(pergunta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao encontrar perguntas.");
                }
            }
            return listaPerguntas;
        }

        public BindingList<Perguntas> BuscaPerguntas(decimal? idPergunta = null)
        {
            BindingList<Perguntas> listaPerguntas = new BindingList<Perguntas>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idPergunta != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM PER_PERGUNTA_BETO WHERE per_id_pergunta = @idPergunta", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idPergunta", idPergunta));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM PER_PERGUNTA_BETO", ioConexao);
                    }
                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            Perguntas pergunta = new Perguntas(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2), ioReader.GetString(3),
                                ioReader.GetString(4), ioReader.GetInt32(5));
                            listaPerguntas.Add(pergunta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar a(s) Perguntas(s).");
                }
            }
            return listaPerguntas;
        }

        public int InserePergunta(Perguntas pergunta)
        {
            if (pergunta == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO PER_PERGUNTA_BETO (per_id_pergunta, per_id_questionario, per_ds_pergunta, " +
                                                "per_tp_pergunta, per_ch_resposta_obrigatoria, per_nu_ordem) " +
                                                "VALUES(@perIdPergunta, @perIdQuestionario, @perDsPergunta, @perTpPergunta, " +
                                                "@perChRespostaObrigatoria, @perNuOrdem)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@perIdPergunta", pergunta.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perIdQuestionario", pergunta.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@perDsPergunta", pergunta.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perTpPergunta", pergunta.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perChRespostaObrigatoria", pergunta.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@perNuOrdem", pergunta.per_nu_ordem));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar pergunta.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaPergunta(Perguntas pergunta)
        {
            if (pergunta == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosAlterados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE PER_PERGUNTA_BETO " +
                                                 "SET per_id_questionario = @perIdQuestionario, per_ds_pergunta = @perDsPergunta, per_tp_pergunta = @perTpPergunta, " +
                                                 "per_ch_resposta_obrigatoria = @perChRespostaObrigatoria, per_nu_ordem = @perNuOrdem " +
                                                 "WHERE per_id_pergunta = @perIdPergunta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@perIdPergunta", pergunta.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perIdQuestionario", pergunta.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@perDsPergunta", pergunta.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perTpPergunta", pergunta.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@perChRespostaObrigatoria", pergunta.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@perNuOrdem", pergunta.per_nu_ordem));

                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar pergunta.");
                }
            }

            return qtdRegistrosAlterados;
        }

        public int RemovePergunta(Perguntas pergunta)
        {
            if (pergunta == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM PER_PERGUNTA_BETO " +
                                                "WHERE per_id_pergunta = @perIdPergunta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@perIdPergunta", pergunta.per_id_pergunta));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover pergunta.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}