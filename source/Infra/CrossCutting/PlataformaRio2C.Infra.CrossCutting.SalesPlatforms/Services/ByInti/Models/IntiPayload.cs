using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    public class IntiPayload
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

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

        //TODO: CHECK THIS
        /// <summary>
        /// Gets the sales platform attendee status.
        /// </summary>
        /// <returns></returns>
        public string GetSalesPlatformAttendeeStatus()
        {
            switch (Action.ToLowerInvariant())
            {
                case IntiAction.OrderPlaced:
                    return SalesPlatformAttendeeStatus.Attending;

                case IntiAction.Update:
                    return SalesPlatformAttendeeStatus.NotAttending;

                case IntiAction.Delete:
                    return SalesPlatformAttendeeStatus.Unpaid;

                // Other Updates
                default:
                    return Action;
            }
        }
    }

    /// <summary>
    /// IntiSaleOrCancellationExtraValue
    /// </summary>
    public class IntiSaleOrCancellationExtraValue
    {
        public string name { get; set; }
        public decimal amount { get; set; }
    }

    /// <summary>
    /// IntiSaleOrCancellationDiscount
    /// </summary>
    public class IntiSaleOrCancellationDiscount
    {
        public string code { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
    }

    /// <summary>
    /// IntiSaleOrCancellationRelationships
    /// </summary>
    public class IntiSaleOrCancellationRelationships
    {
        public string event_id { get; set; }
        public string event_date_id { get; set; }
        public string order_id { get; set; }
        public string buyer_id { get; set; }
    }
}