using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionario.Models
{
    [Serializable]
    public class Perguntas
    {
        public decimal per_id_pergunta { get; set; }
        public decimal per_id_questionario { get; set; }
        public string per_ds_pergunta { get; set; }
        public string per_tp_pergunta { get; set; }
        public string per_ch_resposta_obrigatoria { get; set; }
        public int per_nu_ordem { get; set; }

        public Perguntas(decimal perIdPergunta, decimal perIdQuestionario, string perDsPergunta, string perTpPergunta, string perChRespostaObrigatoria, int perNuOrdem)
        {
            this.per_id_pergunta = perIdPergunta;
            this.per_id_questionario = perIdQuestionario;
            this.per_ds_pergunta = perDsPergunta;
            this.per_tp_pergunta = perTpPergunta;
            this.per_ch_resposta_obrigatoria = perChRespostaObrigatoria;
            this.per_nu_ordem = perNuOrdem;
        }
    }
}