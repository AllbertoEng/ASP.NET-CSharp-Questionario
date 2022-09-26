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
    public class OpcoesRespostaDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<OpcoesResposta> BuscarRespostasPorOrdem(int oprNuOrdem, decimal idPergunta)
        {
            if (oprNuOrdem == null || idPergunta == null)
            {
                throw new NullReferenceException();
            }
            BindingList<OpcoesResposta> listaRespostas = new BindingList<OpcoesResposta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta, opr_ch_resposta_correta, opr_nu_ordem " +
                                                 "FROM OPR_OPCAO_RESPOSTA_BETO " +
                                                 "INNER JOIN PER_PERGUNTA_BETO ON opr_id_pergunta = per_id_pergunta " +
                                                 "WHERE opr_nu_ordem = @oprNuOrdem AND per_id_pergunta = @idPergunta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@oprNuOrdem", oprNuOrdem));
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", idPergunta));

                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            OpcoesResposta resposta = new OpcoesResposta(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2), ioReader.GetString(3),
                                ioReader.GetInt32(4));
                            listaRespostas.Add(resposta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao encontrar respostas.");
                }
            }
            return listaRespostas;
        }

        public BindingList<OpcoesResposta> BuscarRespostasPorPergunta(Perguntas pergunta)
        {
            if (pergunta == null)
            {
                throw new NullReferenceException();
            }

            BindingList<OpcoesResposta> listaRespostas = new BindingList<OpcoesResposta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta, opr_ch_resposta_correta, opr_nu_ordem " +
                                                 "FROM OPR_OPCAO_RESPOSTA_BETO " +
                                                 "INNER JOIN PER_PERGUNTA_BETO ON per_id_pergunta = opr_id_pergunta " +
                                                 "WHERE per_id_pergunta = @idPergunta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", pergunta.per_id_pergunta));

                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            OpcoesResposta resposta = new OpcoesResposta(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2), ioReader.GetString(3),
                                ioReader.GetInt32(4));
                            listaRespostas.Add(resposta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao encontrar respostas.");
                }
            }
            return listaRespostas;
        }
        public BindingList<OpcoesResposta> BuscaRespostas(decimal? idResposta = null)
        {
            BindingList<OpcoesResposta> listaRespostas = new BindingList<OpcoesResposta>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idResposta != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_BETO WHERE opr_id_opcao_resposta = @idResposta", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idResposta", idResposta));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_BETO", ioConexao);
                    }
                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            OpcoesResposta resposta = new OpcoesResposta(ioReader.GetDecimal(0), ioReader.GetDecimal(1), ioReader.GetString(2), ioReader.GetString(3),
                                ioReader.GetInt32(4));
                            listaRespostas.Add(resposta);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar a(s) Respostas(s).");
                }
            }
            return listaRespostas;
        }

        public int InsereResposta(OpcoesResposta resposta)
        {
            if (resposta == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO OPR_OPCAO_RESPOSTA_BETO (opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta, " +
                                                "opr_ch_resposta_correta, opr_nu_ordem) " +
                                                "VALUES(@oprIdOpcaoResposta, @oprIdPergunta, @oprDsOpcaoResposta, @oprChRespostaCorreta, " +
                                                "@oprNuOrdem)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@oprIdOpcaoResposta", resposta.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprIdPergunta", resposta.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprDsOpcaoResposta", resposta.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprChRespostaCorreta", resposta.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprNuOrdem", resposta.opr_nu_ordem));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar resposta.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaResposta(OpcoesResposta resposta)
        {
            if (resposta == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosAlterados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO OPR_OPCAO_RESPOSTA_BETO (opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta, " +
                                                "opr_ch_resposta_correta, opr_nu_ordem) " +
                                                "VALUES(@oprIdOpcaoResposta, @oprIdPergunta, @oprDsOpcaoResposta, @oprChRespostaCorreta, " +
                                                "@oprNuOrdem)", ioConexao);

                    ioQuery = new SqlCommand("UPDATE OPR_OPCAO_RESPOSTA_BETO " +
                                                 "SET opr_id_pergunta = @oprIdPergunta, opr_ds_opcao_resposta = @oprDsOpcaoResposta, " +
                                                 "opr_ch_resposta_correta = @oprChRespostaCorreta, opr_nu_ordem = @oprNuOrdem " +
                                                 "WHERE opr_id_opcao_resposta = @oprIdOpcaoResposta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@oprIdOpcaoResposta", resposta.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprIdPergunta", resposta.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprDsOpcaoResposta", resposta.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprChRespostaCorreta", resposta.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@oprNuOrdem", resposta.opr_nu_ordem));

                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar resposta.");
                }
            }

            return qtdRegistrosAlterados;
        }

        public int RemoveResposta(OpcoesResposta resposta)
        {
            if (resposta == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM OPR_OPCAO_RESPOSTA_BETO " +
                                                "WHERE opr_id_opcao_resposta = @oprIdOpcaoResposta", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@oprIdOpcaoResposta", resposta.opr_id_opcao_resposta));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover resposta.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}