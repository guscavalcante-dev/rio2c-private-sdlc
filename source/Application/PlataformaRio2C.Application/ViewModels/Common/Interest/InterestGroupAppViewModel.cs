using System;
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Application.ViewModels
{
    public class InterestGroupAppViewModel : EntityViewModel<InterestGroupAppViewModel, InterestGroup>, IEntityViewModel<InterestGroup>
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public InterestGroupAppViewModel()
        {

        }

        public InterestGroupAppViewModel(InterestGroup interestGroup)
        {
            Uid = interestGroup.Uid;
            CreationDate = interestGroup.CreationDate;
            Name = interestGroup.Name;
            Type = interestGroup.Type;
        }

        public InterestGroup MapReverse()
        {
            if (Type == null)
            {
                Type = InterestGroupTypeCodes.Multiple.ToString();
            }
            
            var entity = new InterestGroup(Name, (InterestGroupTypeCodes)Enum.Parse(typeof(InterestGroupTypeCodes), Type));
            return entity;
        }

        public InterestGroup MapReverse(InterestGroup entity)
        {
            if (Type == null)
            {
                Type = InterestGroupTypeCodes.Multiple.ToString();
            }

            entity.SetName(Name);
            entity.SetType((InterestGroupTypeCodes)Enum.Parse(typeof(InterestGroupTypeCodes), Type));
            return entity;
        }
    }
}
