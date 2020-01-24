//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="ProjectLinkTeaserAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Web.Mvc;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    /// <summary>ProjectLinkTeaserAppViewModel</summary>
//    public class ProjectLinkTeaserAppViewModel : EntityViewModel<ProjectLinkTeaserAppViewModel, ProjectTeaserLink>, IEntityViewModel<ProjectTeaserLink>
//    {
//        [AllowHtml]
//        public string Value { get; set; }       
//        public  ProjectBasicAppViewModel Project { get; set; }

//        public ProjectLinkTeaserAppViewModel()
//        {

//        }

//        public ProjectLinkTeaserAppViewModel(Domain.Entities.ProjectTeaserLink entity)
//        {
//            CreationDate = entity.CreateDate;
//            Uid = entity.Uid;
//            Value = entity.Value;
//        }

//        public ProjectTeaserLink MapReverse()
//        {
//            return null;
//            //var entity = new ProjectTeaserLink(Value);

//            //return entity;
//        }

//        public void SetProject(ProjectBasicAppViewModel project)
//        {
//            Project = project;
//        }

//        public ProjectTeaserLink MapReverse(ProjectTeaserLink entity)
//        {
//            return entity;
//        }
//    }
//}
