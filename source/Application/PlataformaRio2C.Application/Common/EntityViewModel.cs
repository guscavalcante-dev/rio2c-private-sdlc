using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PlataformaRio2C.Application.Common
{
    public abstract class EntityViewModel<TViewModel, TDomainClass>
        where TViewModel : class
        where TDomainClass : class
    {
        public virtual Guid Uid { get; set; }

        [Display(Name = "Data da criação")]
        public virtual DateTime CreationDate { get; set; }

        public EntityViewModel()
        {
        }

        public EntityViewModel(TDomainClass entity)
        {
            CreationDate = (DateTime)entity.GetType().GetProperty("CreationDate").GetValue(entity, null);
            Uid = (Guid)entity.GetType().GetProperty("Uid").GetValue(entity, null);
        }

        public static IEnumerable<TViewModel> MapList(IEnumerable<TDomainClass> entities)
        {
            Type type = typeof(TViewModel);

            foreach (var entity in entities)
            {
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(TDomainClass) });
                yield return (TViewModel)ctor.Invoke(new TDomainClass[] { entity });
            }
        }
    }

    public interface IEntityViewModel<TDomainClass>
    {
        TDomainClass MapReverse();

        TDomainClass MapReverse(TDomainClass entity);
    }
}
