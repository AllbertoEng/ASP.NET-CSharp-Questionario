using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionario.Models
{
    [Serializable]
    public class OpcoesResposta
    {
        public decimal opr_id_opcao_resposta { get; set; }
        public decimal opr_id_pergunta { get; set; }
        public string opr_ds_opcao_resposta { get; set; }
        public string opr_ch_resposta_correta { get; set; }
        public int opr_nu_ordem { get; set; }

        public OpcoesResposta(decimal oprIdOpcaoResposta, decimal oprIdPergunta, string oprDsOpcaoResposta, string oprChRespostaCorreta, int oprNuOrdem)
        {
            this.opr_id_opcao_resposta = oprIdOpcaoResposta;
            this.opr_id_pergunta = oprIdPergunta;
            this.opr_ds_opcao_resposta = oprDsOpcaoResposta;
            this.opr_ch_resposta_correta = oprChRespostaCorreta;
            this.opr_nu_ordem = oprNuOrdem;
        }
    }
}