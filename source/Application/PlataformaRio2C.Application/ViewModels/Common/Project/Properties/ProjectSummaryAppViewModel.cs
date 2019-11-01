// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProjectSummaryAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ProjectSummaryAppViewModel</summary>
    public class ProjectSummaryAppViewModel : EntityViewModel<ProjectSummaryAppViewModel, ProjectSummary>, IEntityViewModel<ProjectSummary>
    {
        public static readonly int ValueMaxLength = ProjectSummary.ValueMaxLength;

        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectSummaryAppViewModel()
        {

        }

        public ProjectSummaryAppViewModel(Domain.Entities.ProjectSummary entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectSummary MapReverse()
        {
            return null;
            //var entity = new ProjectSummary(Value, LanguageCode);

            //return entity;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }

        public void SetProject(ProjectBasicAppViewModel project)
        {
            Project = project;
        }

        public ProjectSummary MapReverse(ProjectSummary entity)
        {
            return entity;
        }
    }
}
