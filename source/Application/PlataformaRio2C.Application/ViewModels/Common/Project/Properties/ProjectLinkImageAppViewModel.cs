// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProjectLinkImageAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ProjectLinkImageAppViewModel</summary>
    public class ProjectLinkImageAppViewModel : EntityViewModel<ProjectLinkImageAppViewModel, ProjectImageLink>, IEntityViewModel<ProjectImageLink>
    {
        [AllowHtml]
        public string Value { get; set; }       
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectLinkImageAppViewModel()
        {

        }

        public ProjectLinkImageAppViewModel(Domain.Entities.ProjectImageLink entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Value = entity.Value;
        }

        public ProjectImageLink MapReverse()
        {
            return null;
            //var entity = new ProjectImageLink(Value);

            //return entity;
        }

        public void SetProject(ProjectBasicAppViewModel project)
        {
            Project = project;
        }

        public ProjectImageLink MapReverse(ProjectImageLink entity)
        {
            return entity;
        }
    }
}
