using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class CollaboratorOptionAppViewModel : EntityViewModel<CollaboratorOptionAppViewModel, Collaborator>
    {        
        public string Name { get; set; }

        public CollaboratorOptionAppViewModel()
        {

        }

        public CollaboratorOptionAppViewModel(Collaborator entity)
            :base(entity)
        {

            Name = entity.Name;

            if (entity.Players != null && entity.Players.Any())
            {                
                Name = string.Format("{0} - {1} - {2}", entity.Name, string.Join(", ", entity.Players.Select(e => e.Name)), string.Join(",", entity.Players.Select(e => e.Holding.Name)));
            }
            else if(entity.ProducersEvents != null && entity.ProducersEvents.Any( e => !string.IsNullOrWhiteSpace(e.Producer.Name)))
            {
                var namesProducers = entity.ProducersEvents.Select(e => e.Producer.Name).Where(e => !string.IsNullOrWhiteSpace(e)).Distinct();
                Name = string.Format("{0} - {1}", entity.Name, string.Join(", ", namesProducers));
            }
        }       
    }
}
