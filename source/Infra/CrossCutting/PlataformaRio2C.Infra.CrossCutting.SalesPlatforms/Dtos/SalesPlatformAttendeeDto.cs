// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="SalesPlatformAttendeeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos
{
    /// <summary>SalesPlatformAttendeeDto</summary>
    public class SalesPlatformAttendeeDto
    {
        // Event
        public string EventId { get; private set; }

        // Order
        public string OrderId { get; private set; }
        //public string OrderStatus { get; private set; } // Change to static class

        // Attendee
        public string AttendeeId { get; private set; }
        public DateTime SalesPlatformUpdateDate { get; private set; }
        public string SalesPlatformAttendeeStatus { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool IsCheckedIn { get; private set; }
        public string TicketClassId { get; private set; }
        public string TicketClassName { get; private set; }

        // Profile
        public string FirstName { get; private set; }
        public string LastMame { get; private set; }
        public string Gender { get; private set; }
        public int? Age { get; private set; }
        public string Name { get; private set; }
        public string BirthDate { get; private set; }
        public string CellPhone { get; private set; }
        public string Email { get; private set; }
        public string JobTitle { get; private set; }

        // Barcode
        public string Barcode { get; private set; }
        public bool IsBarcodePrinted { get; private set; }
        public bool IsBarcodeUsed { get; private set; }
        public DateTime? BarcodeUpdateDate { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformAttendeeDto"/> class.</summary>
        public SalesPlatformAttendeeDto()
        {
        }

        public SalesPlatformAttendeeDto(EventbriteAttendee eventBriteAttendee)
        {
            // Event
            this.EventId = eventBriteAttendee.EventId;

            // Order
            this.OrderId = eventBriteAttendee.OrderId;
            //this.OrderStatus = eventBriteAttendee.Status;

            // Attendee
            this.AttendeeId = eventBriteAttendee.Id;
            this.SalesPlatformUpdateDate = eventBriteAttendee.Changed;
            this.SalesPlatformAttendeeStatus = eventBriteAttendee.GetSalesPlatformAttendeeStatus();
            this.IsCancelled = eventBriteAttendee.Cancelled;
            this.IsCheckedIn = eventBriteAttendee.CheckedIn;
            this.TicketClassId = eventBriteAttendee.TicketClassId;
            this.TicketClassName = eventBriteAttendee.TicketClassName;

            // Profile
            this.FirstName = eventBriteAttendee.Profile.FirstName;
            this.LastMame = eventBriteAttendee.Profile.LastMame;
            this.Gender = eventBriteAttendee.Profile.Gender;
            this.Age = eventBriteAttendee.Profile.Age;
            this.Name = eventBriteAttendee.Profile.Name;
            this.BirthDate = eventBriteAttendee.Profile.BirthDate;
            this.CellPhone = eventBriteAttendee.Profile.CellPhone;
            this.Email = eventBriteAttendee.Profile.Email;
            this.JobTitle = eventBriteAttendee.Profile.JobTitle;

            // Barcode
            var barcode = eventBriteAttendee.GetBarcode();
            this.Barcode = barcode?.Barcode;
            this.IsBarcodePrinted = barcode?.IsPrinted ?? false;
            this.IsBarcodeUsed = barcode?.IsBarcodeUsed() ?? false;
            this.BarcodeUpdateDate = barcode?.Changed;
        }

        public SalesPlatformAttendeeDto(IntiSaleOrCancellation intiSale)
        {
            // Event
            this.EventId = intiSale.relationships.event_id;

            // Order
            this.OrderId = intiSale.relationships.order_id;

            // Attendee
            this.AttendeeId = intiSale.relationships.buyer_id; // FIXME
            this.SalesPlatformUpdateDate = DateTime.Now;
            this.SalesPlatformAttendeeStatus = Dtos.SalesPlatformAttendeeStatus.Attending;
            this.IsCancelled = false;
            this.IsCheckedIn = false;
            this.TicketClassId = intiSale.price_name;
            this.TicketClassName = intiSale.price_name;

            // Profile
            this.FirstName = intiSale.name.Contains(" ") ? intiSale.name.Split(Convert.ToChar(" "))[0] : intiSale.name;
            this.LastMame = intiSale.name.Contains(" ") ? intiSale.name.Split(Convert.ToChar(" "))[1] : ""; 
            this.Gender = "";
            this.Age = null;
            this.Name = intiSale.name;
            this.BirthDate = "";
            this.CellPhone = "0";
            this.Email = intiSale.email;
            this.JobTitle = "_";

            // Barcode            
            this.Barcode = intiSale.validator_code;
            this.IsBarcodePrinted = false;
            this.IsBarcodeUsed = false;
            this.BarcodeUpdateDate = null;
        }
    }
}