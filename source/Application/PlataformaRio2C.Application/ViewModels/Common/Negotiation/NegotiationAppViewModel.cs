using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class NegotiationAppViewModel : EntityViewModel<NegotiationAppViewModel, Negotiation>, IEntityViewModel<Negotiation>
    {        
        public string Player { get; set; }
        public string ProjectName { get; set; }
        public string Project { get; set; }
        public string Producer { get; set; }
        public string SocialName { get; set; }

        public string Date { get; set; }
        public int Slot { get; set; }
        public string Room { get; set; }
        public int Table { get; set; }

        public string StarTime { get; set; }
        public string EndTime { get; set; }

        public string Type { get; set; }


        public NegotiationAppViewModel()
            :base()
        {

        }

        public NegotiationAppViewModel(Negotiation entity)
            : base(entity)
        {
            Player = entity.Player != null ? entity.Player.Name : null;
            //Project = entity.Project != null ? entity.Project.GetName() : null;
            //Producer = entity.Project != null && entity.Project.Producer != null ? entity.Project.Producer.Name: null;
            //SocialName = entity.Project != null && entity.Project.Producer != null ? entity.Project.Producer.TradeName : null;

            Date = entity.Date.Value.ToString("dd/MM/yyyy");
            Slot = entity.RoundNumber;
            Room = entity.Room != null ? entity.Room.GetName() : null;
            Table = entity.TableNumber;

            StarTime = entity.StarTime.ToString("hh':'mm");
            EndTime = entity.EndTime.ToString("hh':'mm");

            if (entity.EvaluationId != null)
            {
                Type = "Automatic";
            }
            else
            {
                Type = "Manual";
            }
        }

        public Negotiation MapReverse()
        {
            return null;
        }

        public Negotiation MapReverse(Negotiation entity)
        {
            return null;
        }
    }   
}
