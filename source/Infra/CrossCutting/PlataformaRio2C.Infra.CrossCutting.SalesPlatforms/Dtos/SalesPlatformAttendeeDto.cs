// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="SalesPlatformAttendeeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;

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
        public string AttendeeStatus { get; private set; }
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

        /// <summary>Initializes a new instance of the <see cref="SalesPlatformAttendeeDto"/> class.</summary>
        public SalesPlatformAttendeeDto()
        {
        }

        public SalesPlatformAttendeeDto(Attendee eventBriteAttendee)
        {
            // Event
            this.EventId = eventBriteAttendee.EventId;

            // Order
            this.OrderId = eventBriteAttendee.OrderId;
            //this.OrderStatus = eventBriteAttendee.Status;

            // Attendee
            this.AttendeeId = eventBriteAttendee.Id;
            this.AttendeeStatus = eventBriteAttendee.Status;
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
        }
    }
}