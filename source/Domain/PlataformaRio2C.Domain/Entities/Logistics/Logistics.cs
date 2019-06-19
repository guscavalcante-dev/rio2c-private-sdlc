using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Web;

namespace PlataformaRio2C.Domain.Entities
{
    public class Logistics : Entity
    {
        public DateTime? ArrivalDate { get; private set; }
        public TimeSpan? ArrivalTime { get; private set; }        
        public DateTime? DepartureDate{ get; private set; }
        public TimeSpan? DepartureTime { get; private set; }        
        public int CollaboratorId { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }
        public int EventId { get; private set; }
        public virtual Event Event { get; private set; }
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

        public void SetEvent(Event eventEntity)
        {
            Event = eventEntity;
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
