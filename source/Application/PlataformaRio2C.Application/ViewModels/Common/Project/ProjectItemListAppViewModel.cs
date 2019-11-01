using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectItemListAppViewModel : EntityViewModel<ProjectItemListAppViewModel, Project>
    {
        #region props

        public virtual IEnumerable<ProjectTitleAppViewModel> Titles { get; set; }

        public string TitlePt { get; set; }

        public string TitleEn { get; set; }

        public int Id { get; set; }

        public int NumberOfEvaluations { get; set; }

        public virtual IEnumerable<ProjectSummaryAppViewModel> Summaries { get; set; }

        public string[] Genres { get; set; }

        public string ProducerName { get; set; }

        public bool ProjectSubmitted { get; set; }

        public bool Pitching { get; set; }

        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayers { get; set; }

        #endregion

        #region ctor

        public ProjectItemListAppViewModel()
            : base()
        {

        }

        public ProjectItemListAppViewModel(Project entity)
            : base(entity)
        {
            Id = entity.Id;

            if (entity != null)
            {
                Uid = entity.Uid;

                if (entity.Titles.Any())
                {
                    Titles = ProjectTitleAppViewModel.MapList(entity.Titles);
                    TitlePt = string.Join("", Titles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value));
                    TitleEn = string.Join("", Titles.Where(e => e.LanguageCode == "En").Select(e => e.Value));
                }

                if (entity.Summaries.Any())
                {
                    Summaries = ProjectSummaryAppViewModel.MapList(entity.Summaries);
                }

                if (entity.Interests != null)
                {
                    var genres = entity.Interests.Where(e => e.Interest.InterestGroup.Name.Contains("Gênero")).Select(e => e.Interest.Name);

                    if (genres != null)
                    {
                        Genres = genres.ToArray();
                    }
                }

                //if (entity.Producer != null)
                //{
                //    ProducerName = entity.Producer.Name;
                //}

                //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
                //{
                //    ProjectSubmitted = entity.PlayersRelated.Any(e => e.Sent);

                //    RelatedPlayers = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);

                //    NumberOfEvaluations = entity.PlayersRelated.Count(e => e.Evaluation != null);
                //}

                if (entity.Pitching != null)
                {
                    Pitching = entity.Pitching.Value;
                }
            }
        }

        #endregion

        #region Public methods

        #endregion


    }


    public class ProjectAdminItemListAppViewModel : EntityViewModel<ProjectAdminItemListAppViewModel, Project>
    {
        #region props

        public virtual IEnumerable<ProjectTitleAppViewModel> Titles { get; set; }

        public string TitlePt { get; set; }

        public string TitleEn { get; set; }

        public int Id { get; set; }

        public int NumberOfEvaluations { get; set; }

        public virtual IEnumerable<ProjectSummaryAppViewModel> Summaries { get; set; }

        public string[] Genres { get; set; }

        public string ProducerName { get; set; }

        public bool ProjectSubmitted { get; set; }

        public bool Pitching { get; set; }

        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayers { get; set; }

        #endregion

        #region ctor

        public ProjectAdminItemListAppViewModel()
            : base()
        {

        }

        public ProjectAdminItemListAppViewModel(Project entity)
            : base(entity)
        {
            Id = entity.Id;

            if (entity != null)
            {
                Uid = entity.Uid;

                if (entity.Titles.Any())
                {
                    Titles = ProjectTitleAppViewModel.MapList(entity.Titles);
                    TitlePt = string.Join("", Titles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value));
                    TitleEn = string.Join("", Titles.Where(e => e.LanguageCode == "En").Select(e => e.Value));
                }


                //if (entity.Producer != null)
                //{
                //    ProducerName = entity.Producer.Name;
                //}

                //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
                //{
                //    ProjectSubmitted = entity.PlayersRelated.Any(e => e.Sent);

                //    RelatedPlayers = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);

                //    NumberOfEvaluations = entity.PlayersRelated.Count(e => e.Evaluation != null);
                //}

                if (entity.Pitching != null)
                {
                    Pitching = entity.Pitching.Value;
                }
            }
        }

        #endregion

        #region Public methods

        #endregion


    }

    public class ProjectItemListPlayerAppViewModel
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public bool HasImage { get; set; }
        public ImageFileAppViewModel Image { get; set; }

        public ProjectItemListPlayerAppViewModel()
        {

        }

        public ProjectItemListPlayerAppViewModel(ProjectPlayer entity)
        {
            Uid = entity.Player.Uid;
            Name = entity.Player.Name;
            HasImage = entity.Player.ImageId > 0;

            if (entity.EvaluationId > 0)
            {

            }
        }

        public static IEnumerable<ProjectItemListPlayerAppViewModel> MapList(IEnumerable<ProjectPlayer> entities)
        {
            foreach (var entity in entities)
            {

                yield return new ProjectItemListPlayerAppViewModel(entity);
            }
        }
    }


    public class ProjectExcelItemListAppViewModel : EntityViewModel<ProjectExcelItemListAppViewModel, Project>
    {
        #region props

        public virtual IEnumerable<ProjectTitleAppViewModel> Titles { get; set; }

        public string TitlePt { get; set; }

        public string TitleEn { get; set; }

        public int Id { get; set; }

        public int NumberOfEvaluations { get; set; }

        public virtual IEnumerable<ProjectSummaryAppViewModel> Summaries { get; set; }

        public string[] Genres { get; set; }

        public string ProducerName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public bool ProjectSubmitted { get; set; }
        public bool Pitching { get; set; }
        public virtual DateTime DateSending { get; set; }

        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayers { get; set; }

        public IEnumerable<ProjectInterestAppViewModel> Interest { get; set; }
        public string SinopsePt { get; set; }
        public string SinopseEn { get; set; }
        public string Status { get; set; }
        public string ResponsavelAprovacao { get; set; }
        public string PlayerStatus { get; set; }

        #endregion

        #region ctor

        public ProjectExcelItemListAppViewModel()
            : base()
        {

        }

        public ProjectExcelItemListAppViewModel(Project entity)
            : base(entity)
        {
            Id = entity.Id;

            if (entity != null)
            {
                Uid = entity.Uid;

                if (entity.Titles.Any())
                {
                    Titles = ProjectTitleAppViewModel.MapList(entity.Titles);
                    TitlePt = string.Join("", Titles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value));
                    TitleEn = string.Join("", Titles.Where(e => e.LanguageCode == "En").Select(e => e.Value));
                }


                //if (entity.Producer != null)
                //{
                //    ProducerName = entity.Producer.Name;
                //}

                //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
                //{
                //    ProjectSubmitted = entity.PlayersRelated.Any(e => e.Sent);

                //    RelatedPlayers = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);

                //    DateSending = Convert.ToDateTime(entity.PlayersRelated.Select(x => x.DateSending).FirstOrDefault());

                //    NumberOfEvaluations = entity.PlayersRelated.Count(e => e.Evaluation != null);

                //    PlayerStatus = "";
                //    foreach (var item in entity.PlayersRelated)
                //    {
                //        PlayerStatus += item.Player.Name + " | ";
                //        if (item.Evaluation != null)
                //        {
                //            PlayerStatus += item.Evaluation.Status.Name;
                //        }
                //        else
                //        {
                //            PlayerStatus += "Não avaliado";
                //        }
                //        PlayerStatus += " / ";

                //    }
                //}

                //if (entity.Interests != null && entity.Interests.Any())
                //{
                //    Interest = ProjectInterestAppViewModel.MapList(entity.Interests);
                //}

                //if (entity.Pitching != null)
                //{
                //    Pitching = entity.Pitching.Value;
                //}

                //if (entity.Producer.EventsCollaborators != null && entity.Producer.EventsCollaborators.Any())
                //{
                //    Email = string.Join(", ", entity.Producer.EventsCollaborators.Select(x => x.Collaborator).Select(x => x.User).Select(x => x.Email));
                //    Name = string.Join(", ", entity.Producer.EventsCollaborators.Select(x => x.Collaborator).Select(x => x.User).Select(x => x.Name));
                //}
                //if (entity.Summaries != null && entity.Summaries.Any())
                //{

                //    SinopseEn = entity.Summaries.Where(x => x.LanguageId == 1).Select(x => x.Value).FirstOrDefault();
                //    SinopsePt = entity.Summaries.Where(x => x.LanguageId == 2).Select(x => x.Value).FirstOrDefault();
                //}
                //if (NumberOfEvaluations != 0)
                //{
                //    Status = entity.PlayersRelated.Where(x => x.EvaluationId != null).Select(x => x.Evaluation).FirstOrDefault().Status.Name;
                //    ResponsavelAprovacao = entity.PlayersRelated.Where(x => x.EvaluationId != null).Select(x => x.Player).FirstOrDefault().Name;
                //}
            }
        }

        #endregion

        #region Public methods

        #endregion


    }



    public class ProjectExcelListAppViewModel : EntityViewModel<ProjectExcelListAppViewModel, Project>
    {
        #region props

        public virtual IEnumerable<ProjectTitleAppViewModel> Titles { get; set; }

        public string TitlePt { get; set; }

        public string TitleEn { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //ProjectPlayer
        public bool Sent { get; set; }
        public virtual DateTime DateSending { get; set; }
        public virtual DateTime DateSaved { get; set; }

        //Producer
        public int ProducerId { get; set; }
        public string ProducerCnpj { get; set; }
        public string ProducerName { get; set; }
        public string ProducerTradeName { get; set; }


        public IEnumerable<PlayerProjectStatusAppViewModel> RelatedPlayers { get; set; }

        #endregion

        #region ctor

        public ProjectExcelListAppViewModel()
            : base()
        {

        }

        public ProjectExcelListAppViewModel(Project entity)
            : base(entity)
        {
            Id = entity.Id;

            if (entity != null)
            {
                Uid = entity.Uid;

                if (entity.Titles.Any())
                {
                    Titles = ProjectTitleAppViewModel.MapList(entity.Titles);
                    TitlePt = string.Join("", Titles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value));
                    TitleEn = string.Join("", Titles.Where(e => e.LanguageCode == "En").Select(e => e.Value));
                }


                //if (entity.Producer != null)
                //{
                //    ProducerId = entity.Producer.Id;
                //    ProducerCnpj = entity.Producer.CNPJ;
                //    ProducerName = entity.Producer.Name;
                //    ProducerTradeName = entity.Producer.TradeName;
                //}

                //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
                //{

                //    RelatedPlayers = PlayerProjectStatusAppViewModel.MapList(entity.PlayersRelated);

                //    Sent = entity.PlayersRelated.Any(e => e.Sent);

                //    DateSending = Convert.ToDateTime(entity.PlayersRelated.Select(x => x.DateSending).FirstOrDefault());

                //    DateSaved = Convert.ToDateTime(entity.PlayersRelated.Select(x => x.DateSaved).FirstOrDefault());

                //}

                //if (entity.Producer.EventsCollaborators != null && entity.Producer.EventsCollaborators.Any())
                //{
                //    Email = string.Join(", ", entity.Producer.EventsCollaborators.Select(x => x.Collaborator).Select(x => x.User).Select(x => x.Email));
                //    Name = string.Join(", ", entity.Producer.EventsCollaborators.Select(x => x.Collaborator).Select(x => x.User).Select(x => x.Name));
                //}
            }
        }

        #endregion

    }
}
