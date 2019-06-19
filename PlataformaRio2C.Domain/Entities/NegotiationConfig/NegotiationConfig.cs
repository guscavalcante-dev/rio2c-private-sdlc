using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    public class NegotiationConfig : Entity
    {
        public DateTime? Date { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }        
        public int RoudsFirstTurn { get; private set; }
        public int RoundsSecondTurn { get; private set; }
        public TimeSpan TimeIntervalBetweenTurn { get; private set; }
        public TimeSpan TimeOfEachRound { get; private set; }
        public TimeSpan TimeIntervalBetweenRound { get; private set; }
        public virtual ICollection<NegotiationRoomConfig> Rooms { get; private set; }

        protected NegotiationConfig()
        {

        }

        public NegotiationConfig(DateTime? date)
        {
            SetDate(date);
        }
        

        public void SetDate(DateTime? val)
        {
            Date = val;
        }

        public void SetStartTime(TimeSpan val)
        {
            StartTime = val;
        }

        public void SetEndTime(TimeSpan val)
        {
            EndTime = val;
        }

        public void SetTimeIntervalBetweenTurn(TimeSpan val)
        {
            TimeIntervalBetweenTurn = val;
        }

        public void SetTimeOfEachRound(TimeSpan val)
        {
            TimeOfEachRound = val;
        }

        public void SetTimeIntervalBetweenRound(TimeSpan val)
        {
            TimeIntervalBetweenRound = val;
        }

        public void SetCountSlotsFirstTurn(int val)
        {
            RoudsFirstTurn = val;
        }

        public void SetCountSlotsSecondTurn(int val)
        {
            RoundsSecondTurn = val;
        }
        public void SetRooms(ICollection<NegotiationRoomConfig> rooms)
        {
            Rooms = rooms;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new NegotiationConfigIsConsistent().Valid(this));

            if (Rooms != null && Rooms.Any())
            {
                foreach (var room in Rooms)
                {
                    ValidationResult.Add(new NegotiationRoomConfigIsConsistent().Valid(room));
                }
            }

            return ValidationResult.IsValid;
        }
    }
}
