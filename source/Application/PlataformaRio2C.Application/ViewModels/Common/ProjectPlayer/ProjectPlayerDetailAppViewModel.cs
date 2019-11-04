using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectPlayerDetailAppViewModel: ProjectDetailAppViewModel
    {
        //public static readonly int ReasonMaxLength = ProjectPlayerEvaluation.ReasonMaxLength;
        public IEnumerable<PlayerProjectStatusAppViewModel> Players { get; set; }

        public ProjectPlayerDetailAppViewModel()
            :base()
        {

        }

        //public ProjectPlayerDetailAppViewModel(ProjectPlayer entity)
        //    :base(entity.Project)
        //{
        //    //if (entity.Project.PlayersRelated != null && entity.Project.PlayersRelated.Any())
        //    //{
        //    //    Players = PlayerProjectStatusAppViewModel.MapList(entity.Project.PlayersRelated).ToList();
        //    //}
        //}
    }
}
