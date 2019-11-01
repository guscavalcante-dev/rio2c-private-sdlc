using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels.Admin
{
    public class ProjectDetailAppViewModel : EntityViewModel<ProjectDetailAppViewModel, Project>
    {
        public static readonly int ReasonMaxLength = ProjectPlayerEvaluation.ReasonMaxLength;
        public IEnumerable<ProjectDetailItemPlayerAppViewModel> Players { get; set; }

        public string Title { get; set; }

        public ProjectDetailAppViewModel()
            : base()
        {

        }

        public ProjectDetailAppViewModel(Project entity)
            : base(entity)
        {
            if (entity.Titles.Any())
            {
                var titlePt = entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr");
                var titleEn = entity.Titles.FirstOrDefault(e => e.Language.Code == "En");

                if (titlePt != null && titleEn != null)
                {
                    Title = string.Format("{0} | {1}", titlePt.Value, titleEn.Value);
                }
            }

            //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
            //{
            //    Players = ProjectDetailItemPlayerAppViewModel.MapList(entity.PlayersRelated).ToList();
            //}
        }
    }

    public class ProjectDetailItemPlayerAppViewModel : EntityViewModel<ProjectDetailItemPlayerAppViewModel, ProjectPlayer>
    {
        public int Id { get; set; }
        public bool Sent { get; set; }
        public string PlayerName { get; set; }
        public string SavedUser { get; set; }
        public string SendingUser { get; set; }
        public DateTime? DateSaved { get; set; }
        public DateTime? DateSending { get; set; }
        public PlayerProjectStatusAppViewModel Evaluation { get; set; }

        public string Status { get; set; }        
        public string Reason { get; set; }

        public ProjectDetailItemPlayerAppViewModel()
            : base()
        {

        }

        public ProjectDetailItemPlayerAppViewModel(ProjectPlayer entity)
            : base(entity)
        {
            Id = entity.Id;
            Sent = entity.Sent;
            PlayerName = entity.Player.Name;

            if (entity.SavedUser!= null)
            {
                SavedUser = entity.SavedUser.Name;
            }

            if (entity.SendingUser != null)
            {
                SendingUser = entity.SendingUser.Name;
            }
            
            DateSaved = entity.DateSaved;
            DateSending = entity.DateSending;

            if (entity.Evaluation != null)
            {
                if (entity.EvaluationId > 0 && entity.Evaluation != null && entity.Evaluation.StatusId > 0 && entity.Evaluation.Status != null)
                {
                  
                    Status = Labels.ResourceManager.GetString(entity.Evaluation.Status.Code);
                    Reason = entity.Evaluation.Reason;
                }
                else
                {
                 
                    Status = Labels.ResourceManager.GetString(StatusProjectCodes.OnEvaluation.ToString());
                }
            }
        }
    }
}
