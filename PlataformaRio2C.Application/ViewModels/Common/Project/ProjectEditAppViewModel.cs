using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectEditAppViewModel : ProjectBasicAppViewModel
    {
        
        public Guid UIdCreate { get; set; }
        #region props

        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }
        public int[] InterestsSelected { get; set; }

        #endregion

        #region ctor

        public ProjectEditAppViewModel()
            :base()
        {
            LanguagesOptions = new List<LanguageAppViewModel>();
        }

        public ProjectEditAppViewModel(Project entity)
            : base(entity)
        {

        }

        #endregion

        #region Public methods

      

        #endregion
    }
}
