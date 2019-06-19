using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Web.Site.Models
{
    public class TicketJsonResponse
    {
        public string nome_pessoa { get; set; }
        public string sobrenome_pessoa { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string status_pedido { get; set; }
        public string tipo_compra { get; set; }
        public List<CredencialTicket> Credencial { get; set; }
        //public DateTime data_venda { get; set; }
        //public List<IngressoTicket> ingresso { get; set; }
    }
}