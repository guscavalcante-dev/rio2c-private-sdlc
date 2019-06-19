using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceLecturerAppViewModel : EntityViewModel<ConferenceLecturerAppViewModel, ConferenceLecturer>, IEntityViewModel<ConferenceLecturer>
    {

        public ConferenceLecturerAppViewModel()
            : base()
        {

        }

        public ConferenceLecturerAppViewModel(ConferenceLecturer entity)
            : base(entity)
        {

        }

        public ConferenceLecturer MapReverse()
        {


            return null;
        }

        public ConferenceLecturer MapReverse(ConferenceLecturer entity)
        {


            return entity;
        }
    }
}
