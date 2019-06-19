using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class TargetAudienceAppViewModel : IEntityViewModel<TargetAudience>
    {
        public string Name { get; set; }

        public TargetAudienceAppViewModel()
        {

        }

        public TargetAudienceAppViewModel(Domain.Entities.TargetAudience entity)
        {
            Name = entity.Name;
        }

        public TargetAudience MapReverse()
        {
            return new Domain.Entities.TargetAudience(this.Name);
        }

        public TargetAudience MapReverse(TargetAudience entity)
        {
            entity.SetName(Name);

            return entity;
        }
    }
}
