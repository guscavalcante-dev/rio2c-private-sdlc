using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class HoldingOptionAppViewModel
    {
        #region props
                
        public Guid Uid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        #endregion

        #region ctor

        public HoldingOptionAppViewModel()
        {

        }

        public HoldingOptionAppViewModel(Holding entity)
        {
            Uid = entity.Uid;
            Name = entity.Name;
        }

        #endregion

        #region Public methods

        public static IEnumerable<HoldingOptionAppViewModel> MapList(IEnumerable<Holding> entities)
        {
            foreach (var entity in entities)
            {
                yield return new HoldingOptionAppViewModel(entity);
            }
        }

        #endregion
    }
}
