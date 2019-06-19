using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ActivityAppViewModel : IEntityViewModel<Activity>
    {
        public string Name { get; set; }

        public ActivityAppViewModel()
        {

        }

        public ActivityAppViewModel(Domain.Entities.Activity entity)
        {
            Name = entity.Name;
        }

        public Activity MapReverse()
        {
            return new Domain.Entities.Activity(this.Name);
        }

        public Activity MapReverse(Activity entity)
        {
            entity.SetName(Name);

            return entity;
        }
    }
}
