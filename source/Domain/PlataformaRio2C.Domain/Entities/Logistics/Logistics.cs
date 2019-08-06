// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="Logistics.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Logistics</summary>
    public class Logistics : Entity
    {
        public DateTime? ArrivalDate { get; private set; }
        public TimeSpan? ArrivalTime { get; private set; }        
        public DateTime? DepartureDate{ get; private set; }
        public TimeSpan? DepartureTime { get; private set; }        
        public int CollaboratorId { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }
        public int EventId { get; private set; }
        public virtual Edition Edition { get; private set; }
        public string OriginalName { get; private set; }
        public string ServerName { get; private set; }

        protected Logistics()
        {

        }

        public Logistics(DateTime? arrivalDate, DateTime? departureDate)
        {
            SetArrivalDate(arrivalDate);
            SetDepartureDate(departureDate);
        }

        public void SetArrivalTime(TimeSpan? arrivalTime)
        {
            ArrivalTime = arrivalTime;
        }

        public void SetArrivalDate(DateTime? val)
        {
            ArrivalDate = val;
        }


        public void SetDepartureTime(TimeSpan? departureTime)
        {
            DepartureTime = departureTime;
        }

        public void SetDepartureDate(DateTime? val)
        {
            DepartureDate = val;
        }

        public void SetOriginalName(string originalName)
        {
            OriginalName = originalName;
        }

        public void SetServerName(string serverName)
        {
            ServerName = serverName;
        }

        public void SetCollaborator(Collaborator collaborator)
        {
            Collaborator = collaborator;
            if (collaborator != null)
            {
                CollaboratorId = collaborator.Id;                
            }
        }

        public void SetEvent(Edition eventEntity)
        {
            Edition = eventEntity;
            if (eventEntity != null)
            {
                EventId = eventEntity.Id;
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new LogisticsIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
