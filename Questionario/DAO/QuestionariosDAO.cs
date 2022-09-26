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
    public class QuestionariosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Questionarios> BuscaQuestionarios(decimal? idQuestionario = null)
        {
            BindingList<Questionarios> listaQuestionarios = new BindingList<Questionarios>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (idQuestionario != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_BETO WHERE qst_id_questionario = @idQuestionario", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", idQuestionario));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_BETO", ioConexao);
                    }
                    using (SqlDataReader ioReader = ioQuery.ExecuteReader())
                    {
                        while (ioReader.Read())
                        {
                            Questionarios questionario = new Questionarios(ioReader.GetDecimal(0), ioReader.GetString(1), ioReader.GetString(2), ioReader.GetString(3));
                            listaQuestionarios.Add(questionario);
                        }
                        ioReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) questionário(s).");
                }          
            }
            return listaQuestionarios;
        }

        public int InsereQuestionario(Questionarios novoQuestionario)
        {
            if (novoQuestionario == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO QST_QUESTIONARIO_BETO (qst_id_questionario, qst_nm_questionario, qst_tp_questionario, qst_ds_link_instrucoes) " +
                                                "VALUES(@idQuestionario, @nomeQuestionario, @tipoQuestionario, @linkQuestionario)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", novoQuestionario.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeQuestionario", novoQuestionario.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tipoQuestionario", novoQuestionario.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@linkQuestionario", novoQuestionario.qst_ds_link_instrucoes));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar questionário.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaQuestionario(Questionarios questionario)
        {
            if (questionario == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosAlterados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE QST_QUESTIONARIO_BETO " +
                                                 "SET qst_nm_questionario = @nomeQuestionario, qst_tp_questionario = @tipoQuestionario, qst_ds_link_instrucoes = @linkQuestionario " +
                                                 "WHERE qst_id_questionario = @idQuestionario", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", questionario.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeQuestionario", questionario.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tipoQuestionario", questionario.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@linkQuestionario", questionario.qst_ds_link_instrucoes));

                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar questionário.");
                }
            }
            
            return qtdRegistrosAlterados;
        }

        public int RemoveQuestionario(Questionarios questionario)
        {
            if (questionario == null)
            {
                throw new NullReferenceException();
            }
            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM QST_QUESTIONARIO_BETO " +
                                                "WHERE qst_id_questionario = @idQuestionario", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", questionario.qst_id_questionario));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover questionário.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}