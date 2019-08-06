// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="NegotiationResultProcessAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>NegotiationResultProcessAppViewModel</summary>
    public class NegotiationResultProcessAppViewModel
    {
        public int NumberScheduledNegotiations{ get; set; }
        public int NumberUnscheduledNegotiations { get; set; }
        public string DateProcess { get; set; }
        public IEnumerable<ProjectPlayerAppViewModel> UnscheduledNegotiations { get; set; }

        public NegotiationResultProcessAppViewModel()
        {

        }

        public NegotiationResultProcessAppViewModel(IEnumerable<Negotiation> negotiations, IEnumerable<ProjectPlayer> submissionsError)
        {
            var lastNegotiation = negotiations.LastOrDefault(e => e.RoundNumber > 0 && e.Project != null);
            if (lastNegotiation != null)
            {
                DateProcess = lastNegotiation.CreateDate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                DateProcess = DateTime.Now.ToString("dd/MM/yyyy");
            }
            
            NumberScheduledNegotiations = negotiations.Count(e => e.RoundNumber > 0 && e.Project != null);
            NumberUnscheduledNegotiations = submissionsError.Count();
            UnscheduledNegotiations = ProjectPlayerAppViewModel.MapList(submissionsError);
        }
    }

    public class ProjectPlayerAppViewModel
    {
        public string PlayerName { get; set; }
        public string ProjectName { get; set; }
        public string ProducerName { get; set; }

        public ProjectPlayerAppViewModel()
        {

        }

        public ProjectPlayerAppViewModel(ProjectPlayer entity)
        {
            PlayerName = entity.Player.Name;
            ProducerName = entity.Project.Producer.Name;
            ProjectName = entity.Project.GetName();

        }

        public static IEnumerable<ProjectPlayerAppViewModel> MapList(IEnumerable<ProjectPlayer> entities)
        {
            foreach (var item in entities)
            {
                yield return new ProjectPlayerAppViewModel(item);
            }
        }
    }
}
