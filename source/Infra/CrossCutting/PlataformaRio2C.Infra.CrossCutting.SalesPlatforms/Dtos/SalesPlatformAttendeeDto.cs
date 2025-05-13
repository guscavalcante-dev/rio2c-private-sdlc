// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2023
// ***********************************************************************
// <copyright file="SalesPlatformAttendeeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models;
using System;
using System.Linq;

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
        public string LastName { get; private set; }
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

        // Ticket Url
        public string TicketUrl { get; private set; }
        public bool IsTicketPrinted { get; private set; }
        public bool IsTicketUsed { get; private set; }
        public DateTime? TicketUpdateDate { get; private set; }

        // Custom Fields
        public string Payload { get; private set; }
        public bool IsOwnershipChange { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformAttendeeDto"/> class.</summary>
        public SalesPlatformAttendeeDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformAttendeeDto"/> class.
        /// </summary>
        /// <param name="eventBriteAttendee">The event brite attendee.</param>
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
            this.LastName = eventBriteAttendee.Profile.LastMame;
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

            // TicketUrl - Eventbrite uses Barcode instead of TicketUrl
            this.TicketUrl = null;
            this.IsTicketPrinted = false;
            this.IsTicketUsed = false;
            this.TicketUpdateDate = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformAttendeeDto"/> class.
        /// </summary>
        /// <param name="intiPayload">The inti payload.</param>
        public SalesPlatformAttendeeDto(IntiPayload intiPayload)
        {
            // Event
            this.EventId = intiPayload?.Relationships?.EventId;

            // Order
            this.OrderId = intiPayload?.Relationships?.OrderId;

            // Attendee
            this.AttendeeId = intiPayload?.Id;
            this.SalesPlatformUpdateDate = intiPayload.Timestamp;
            this.SalesPlatformAttendeeStatus = intiPayload?.GetSalesPlatformAttendeeStatus();
            this.IsCancelled = false;
            this.IsCheckedIn = false;
            this.TicketClassId = intiPayload?.PriceName;
            this.TicketClassName = intiPayload?.PriceName;

            // Profile
            this.FirstName = intiPayload?.Name?.Contains(" ") == true ? intiPayload?.Name?.Split(Convert.ToChar(" "))[0] : intiPayload?.Name;
            this.LastName = intiPayload?.Name?.Contains(" ") == true ? intiPayload?.Name?.Split(Convert.ToChar(" "))[1] : " ";
            this.Name = intiPayload?.Name;
            this.Email = intiPayload?.Email;
            this.Gender = "";
            this.Age = null;
            this.BirthDate = "";
            this.CellPhone = "0";
            this.JobTitle = "_";

            // Barcode - INTI uses TicketUrl instead of Barcode
            this.Barcode = null;
            this.IsBarcodePrinted = false;
            this.IsBarcodeUsed = false;
            this.BarcodeUpdateDate = null;

            // TicketUrl            
            this.TicketUrl = intiPayload?.TicketUrl;
            this.IsTicketPrinted = false;
            this.IsTicketUsed = false;
            this.TicketUpdateDate = intiPayload?.Timestamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesPlatformAttendeeDto" /> class.
        /// </summary>
        /// <param name="symplaPayload">The sympla payload.</param>
        /// <param name="eventId">The event identifier.</param>
        public SalesPlatformAttendeeDto(SymplaParticipant symplaPayload)
        {
            // Event
            this.EventId = symplaPayload?.EventId;

            // Order
            this.OrderId = symplaPayload?.OrderId;

            // Attendee
            this.AttendeeId = symplaPayload?.Id.ToString();
            this.SalesPlatformUpdateDate = symplaPayload.UpdatedDate;
            this.SalesPlatformAttendeeStatus = symplaPayload?.GetSalesPlatformAttendeeStatus();
            this.IsCancelled = false;
            this.IsCheckedIn = symplaPayload?.Checkin?.Count(c => c.CheckIn) > 0;
            this.TicketClassId = symplaPayload?.TicketName;
            this.TicketClassName = symplaPayload?.TicketName;

            // Profile
            this.FirstName = symplaPayload?.FirstName;
            this.LastName = symplaPayload?.LastName;
            this.Name = $"{symplaPayload?.FirstName} {symplaPayload?.LastName}";
            this.Email = symplaPayload?.Email;
            this.Gender = null;
            this.Age = null;
            this.BirthDate = null;
            this.CellPhone = null;
            this.JobTitle = null;

            // Barcode
            this.Barcode = symplaPayload?.TicketNumQrCode;
            this.IsBarcodePrinted = false;
            this.IsBarcodeUsed = false;
            this.BarcodeUpdateDate = null;

            // TicketUrl - Sympla uses QRCode instead of TicketUrl          
            this.TicketUrl = null;
            this.IsTicketPrinted = false;
            this.IsTicketUsed = false;
            this.TicketUpdateDate = null;

            // Custom Fields
            this.Payload = JsonConvert.SerializeObject(symplaPayload, Formatting.None);
            this.IsOwnershipChange = symplaPayload.IsOwnershipChange;
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPayload<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.Payload);
        }
    }
}