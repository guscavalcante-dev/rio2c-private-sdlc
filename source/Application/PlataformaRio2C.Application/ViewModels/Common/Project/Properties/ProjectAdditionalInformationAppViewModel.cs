// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProjectAdditionalInformationAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ProjectAdditionalInformationAppViewModel</summary>
    public class ProjectAdditionalInformationAppViewModel : EntityViewModel<ProjectAdditionalInformationAppViewModel, ProjectAdditionalInformation>, IEntityViewModel<ProjectAdditionalInformation>
    {
        public static readonly int ValueMaxLength = ProjectAdditionalInformation.ValueMaxLength;

        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectAdditionalInformationAppViewModel()
        {

        }

        public ProjectAdditionalInformationAppViewModel(Domain.Entities.ProjectAdditionalInformation entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectAdditionalInformation MapReverse()
        {
            return null;
            //var entity = new ProjectAdditionalInformation(Value, LanguageCode);

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

        public ProjectAdditionalInformation MapReverse(ProjectAdditionalInformation entity)
        {
            return entity;
        }
    }
}
