using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models
{
    class IntiAction
    {

        public const string Create = "create";
        public const string Update = "update";
        public const string Delete = "delete";

        public const string OrderPlaced = "ticket_sold";
        public const string OrderRefunded = "ticket_canceled";
        public const string OrderUpdated = "participant_updated";
        


        /*
        public const string AttendeeUpdated = "attendee.updated";
        public const string AttendeeCheckedIn = "barcode.checked_in";
        public const string AttendeeCheckedOut = "barcode.un_checked_in";
        public const string OrderPlaced = "order.placed";
        public const string OrderRefunded = "order.refunded";
        public const string OrderUpdated = "order.updated";
        */
    }
}