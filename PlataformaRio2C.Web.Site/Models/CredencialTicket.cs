using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Web.Site.Models
{
    public class CredencialTicket
    {
        public CredencialTicket(CredencialTicket entity)
        {
            if (entity != null)
            {
                nome_credencial = entity.nome_credencial;
                empresa_credencial = entity.empresa_credencial;
                necessidades_especiais = entity.necessidades_especiais;
                necessidades_obs = entity.necessidades_obs;
                nomes_para_credencial = entity.nomes_para_credencial;
                dt_nascimento = entity.dt_nascimento;
                sexo_credencial = entity.sexo_credencial;
                outros = entity.outros;
                email_credencial = entity.email_credencial;
                ingresso = entity.ingresso;
            }
        }

        public string nome_credencial { get; set; }
        public string empresa_credencial { get; set; }
        public bool necessidades_especiais { get; set; }
        public string necessidades_obs { get; set; }
        public string nomes_para_credencial { get; set; }
        public DateTime dt_nascimento { get; set;}
        public char sexo_credencial { get; set; }
        public string outros { get; set; }
        public string email_credencial { get; set; }
        public string ingresso { get; set; }
    }
}