using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class Negotiation : Entity
    {
        public int ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public int PlayerId { get; private set; }
        public virtual Player Player { get; private set; }
        public int RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public int? EvaluationId { get; private set; }
        public virtual ProjectPlayerEvaluation Evaluation { get; private set; }
        public DateTime? Date { get; private set; }
        public TimeSpan StarTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public int TableNumber { get; private set; }
        public int RoundNumber { get; private set; }        

        protected Negotiation()
        {

        }

        public Negotiation(DateTime? date)
        {
            SetDate(date);
        }

        public Negotiation(Project project)
        {
            SetProject(project);
        }

        public void SetProject(Project project)
        {
            Project = project;
            if (project != null)
            {
                ProjectId = project.Id;
            }
        }

        public void SetPlayer(Player entity)
        {
            Player = entity;
            if (entity != null)
            {
                PlayerId = entity.Id;
            }
        }

        public void SetSourceEvaluation(ProjectPlayerEvaluation entity)
        {
            CreationDate = DateTime.Now;
            Uid = Guid.NewGuid();
            Evaluation = entity;
            if (entity != null)
            {
                EvaluationId = entity.Id;
            }
        }

        public void SetRoom(Room entity)
        {
            Room = entity;
            if (entity != null)
            {
                RoomId = entity.Id;
            }
        }

        public void SetStarTime(TimeSpan val)
        {
            StarTime = val;
        }

        public void SetEndTime(TimeSpan val)
        {
            EndTime = val;
        }

        public void SetDate(DateTime? val)
        {
            Date = val;

        }

        public void SetTable(int val)
        {
            TableNumber = val;
        }

        public void SetSlotNumber(int val)
        {
            RoundNumber = val;
        }       

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new NegotiationIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
