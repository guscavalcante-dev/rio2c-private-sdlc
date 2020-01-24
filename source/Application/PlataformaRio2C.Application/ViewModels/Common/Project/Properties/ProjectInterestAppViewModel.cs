//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="UserUseTermAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    /// <summary>ProjectInterestAppViewModel</summary>
//    public class ProjectInterestAppViewModel : EntityViewModel<ProjectInterestAppViewModel, ProjectInterest>, IEntityViewModel<ProjectInterest>
//    {
//        public int ProjectId { get; set; }
//        public ProjectBasicAppViewModel Project { get; set; }
//        public int EventId { get; set; }
//        public EventAppViewModel Event { get; set; }

//        public int InterestId { get; set; }        

//        public string InterestName { get; set; }
//        public string InterestGroupName { get; set; }
//        public string InterestGroupType { get; set; }
//        public InterestAppViewModel Interest { get; set; }
//        public bool Selected { get; set; }

//        public ProjectInterestAppViewModel()
//        {

//        }

//        public ProjectInterestAppViewModel(ProjectInterest entity)
//        {
//            ProjectId = entity.ProjectId;            
//            InterestId = entity.InterestId;

//            InterestName = entity.Interest.Name;
//            InterestGroupName = entity.Interest.InterestGroup.Name;
//            InterestGroupType = entity.Interest.InterestGroup.Type;

//            //Player = new PlayerAppViewModel(entity.Project);            
//            Interest = new InterestAppViewModel(entity.Interest);

//            Selected = entity.Project != null && entity.Project.ProjectInterests != null && entity.Project.ProjectInterests.Any(e => e.Interest.Name == entity.Interest.Name);
//        }

//        public static IEnumerable<ProjectInterestAppViewModel> MapList(IEnumerable<Interest> interests, Project project, Edition _event)
//        {
//            return null;
//            //foreach (var interest in interests)
//            //{
//            //    yield return new ProjectInterestAppViewModel(new ProjectInterest(project, interest));
//            //}
//        }

//        public ProjectInterest MapReverse()
//        {
//            return null;
//            //var entity = new ProjectInterest(Project.MapReverse(), Interest.MapReverse());
//            //return entity;
//        }

//        public ProjectInterest MapReverse(Project p, Interest i, Edition v)
//        {
//            return null;
//            //var entity = new ProjectInterest(p, i);
//            //return entity;
//        }

//        public ProjectInterest MapReverse(ProjectInterest entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
