using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels
{
    public class InterestAppViewModel : EntityViewModel<InterestAppViewModel, Interest>, IEntityViewModel<Interest>
    {
        public string Name { get; set; }

        public InterestGroupAppViewModel Group { get; set; }
        public Guid GroupUid { get; set; }

        public IEnumerable<InterestGroupAppViewModel> GroupOptions { get; set; }

        public InterestAppViewModel()
        {

        }

        public InterestAppViewModel(Interest entity)
        {
            Uid = entity.Uid;
            CreationDate = entity.CreationDate;
            Name = entity.Name;

            Group = new InterestGroupAppViewModel(entity.InterestGroup);
            GroupOptions = new List<InterestGroupAppViewModel>();

            if (entity.InterestGroup != null)
            {
                GroupUid = entity.InterestGroup.Uid;
            }
        }

        public Interest MapReverse()
        {
            var entity = new Interest(Name, null);

            return entity;
        }

        public Interest MapReverse(Interest entity)
        {
            entity.SetName(Name);

            return entity;
        }
    }
}
