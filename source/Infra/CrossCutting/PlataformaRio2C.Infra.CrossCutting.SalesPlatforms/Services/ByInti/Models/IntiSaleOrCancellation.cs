using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    public class IntiSaleOrCancellation
    {
        public string action { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public string timestamp { get; set; } //


        public string price_name { get; set; }
        public string event_name { get; set; }
        public string event_date { get; set; } //
        public string ticket_type { get; set; }
        public decimal amount { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string seat { get; set; }
        public string validator_code { get; set; }
        public string block_name { get; set; }
        public string completed_at { get; set; }//
        public string canceled_at { get; set; }//
        public IntiSaleOrCancellationExtraValue[] extra_values { get; set; }
        public IntiSaleOrCancellationDiscount[] discount { get; set; }
        public IntiSaleOrCancellationRelationships relationships { get; set; }

    }

    public class IntiSaleOrCancellationExtraValue
    {
        public string name { get; set; }
        public decimal amount { get; set; }
    }

    public class IntiSaleOrCancellationDiscount
    {
        public string code { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }

    public class IntiSaleOrCancellationRelationships
    {
        public string event_id { get; set; }
        public string event_date_id { get; set; }
        public string order_id { get; set; }
        public string buyer_id { get; set; }
    }
}
