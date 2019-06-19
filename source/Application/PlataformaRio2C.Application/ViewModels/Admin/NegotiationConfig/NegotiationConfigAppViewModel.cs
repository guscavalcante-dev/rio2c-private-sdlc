using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class NegotiationConfigAppViewModel : EntityViewModel<NegotiationConfigAppViewModel, NegotiationConfig>, IEntityViewModel<NegotiationConfig>
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int RoundsFirstTurn { get; set; }
        public int RoundsSecondTurn { get; set; }
        public string TimeIntervalBetweenTurn { get; set; }
        public string TimeOfEachRound { get; set; }
        public string TimeIntervalBetweenRound { get; set; }
        public IEnumerable<NegotiationRoomConfigAppViewModel> Rooms { get; set; }
        public NegotiationConfigAppViewModel()
            : base()
        {
            Rooms = new List<NegotiationRoomConfigAppViewModel>() { new NegotiationRoomConfigAppViewModel() };
        }

        public NegotiationConfigAppViewModel(NegotiationConfig entity)
            : base(entity)
        {
            Date = entity.Date.Value.ToString("dd/MM/yyyy");
            StartTime = entity.StartTime.ToString("hh':'mm");
            EndTime = entity.EndTime.ToString("hh':'mm");
            RoundsFirstTurn = entity.RoudsFirstTurn;
            RoundsSecondTurn = entity.RoundsSecondTurn;
            TimeIntervalBetweenTurn = entity.TimeIntervalBetweenTurn.ToString("hh':'mm");
            TimeOfEachRound = entity.TimeOfEachRound.ToString("hh':'mm");
            TimeIntervalBetweenRound = entity.TimeIntervalBetweenRound.ToString("hh':'mm");

            if (entity.Rooms != null && entity.Rooms.Any())
            {
                Rooms = NegotiationRoomConfigAppViewModel.MapList(entity.Rooms);
            }
            else
            {

                Rooms = new List<NegotiationRoomConfigAppViewModel>() { new NegotiationRoomConfigAppViewModel() };
            }
        }
        public NegotiationConfig MapReverse()
        {
            var entity = new NegotiationConfig(null);

            if (Date != null)
            {


                entity = new NegotiationConfig(DateTime.Parse(Date));

                if (StartTime != null)
                {
                    entity.SetStartTime(TimeSpan.Parse(StartTime));
                }

                if (EndTime != null)
                {
                    entity.SetEndTime(TimeSpan.Parse(EndTime));
                }

                if (EndTime != null)
                {
                    entity.SetEndTime(TimeSpan.Parse(EndTime));
                }


                entity.SetCountSlotsFirstTurn(RoundsFirstTurn);
                entity.SetCountSlotsSecondTurn(RoundsSecondTurn);

                if (TimeIntervalBetweenTurn != null)
                {
                    entity.SetTimeIntervalBetweenTurn(TimeSpan.Parse(TimeIntervalBetweenTurn));
                }

                if (TimeOfEachRound != null)
                {
                    entity.SetTimeOfEachRound(TimeSpan.Parse(TimeOfEachRound));
                }

                if (TimeIntervalBetweenRound != null)
                {
                    entity.SetTimeIntervalBetweenRound(TimeSpan.Parse(TimeIntervalBetweenRound));
                }                
            }

            return entity;
        }

        public NegotiationConfig MapReverse(NegotiationConfig entity)
        {
            entity.SetStartTime(TimeSpan.Parse(StartTime));
            entity.SetEndTime(TimeSpan.Parse(EndTime));
            entity.SetCountSlotsFirstTurn(RoundsFirstTurn);
            entity.SetCountSlotsSecondTurn(RoundsSecondTurn);
            entity.SetTimeIntervalBetweenTurn(TimeSpan.Parse(TimeIntervalBetweenTurn));
            entity.SetTimeOfEachRound(TimeSpan.Parse(TimeOfEachRound));
            entity.SetTimeIntervalBetweenRound(TimeSpan.Parse(TimeIntervalBetweenRound));

            return entity;
        }
    }


}
