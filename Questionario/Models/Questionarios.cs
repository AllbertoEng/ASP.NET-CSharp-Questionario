using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionario.Models
{
    [Serializable]
    public class Questionarios
    {
        public decimal qst_id_questionario { get; set; }
        public string qst_nm_questionario { get; set; }
        public string qst_tp_questionario { get; set; }
        public string qst_ds_link_instrucoes { get; set; }

        public Questionarios(decimal idQuestionario, string nomeQuestionario, string tipoQuestionario, string linkQuestionario)
        {
            this.qst_id_questionario = idQuestionario;
            this.qst_nm_questionario = nomeQuestionario;
            this.qst_tp_questionario = tipoQuestionario;
            this.qst_ds_link_instrucoes = linkQuestionario;
        }
    }
}