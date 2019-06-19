using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.Dtos
{
    public struct ParticipantSympla
    {
        public string event_id { get; set; }
        public string order_num { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime order_date { get; set; }
        public string order_status { get; set; }
        public string transaction_type { get; set; }
        public double order_total_sale_price { get; set; }
        public string discount_code { get; set; }
        public string ticket_name { get; set; }
        public double ticket_sale_price { get; set; }
        public string ticket_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string check_in { get; set; }
        public string check_in_date { get; set; }
        public string banda { get; set; }
        public string country
        {
            get
            {
                return this.custom_form.Where(e => e.name.Contains("Country")).Select(e => e.value).FirstOrDefault();
            }
        }
        public IEnumerable<CusTomFormSympla> custom_form { get; set; }
    }
}
