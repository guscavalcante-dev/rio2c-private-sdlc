//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="ConferenceDetailAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels.Api
//{
//    /// <summary>ConferenceDetailAppViewModel</summary>
//    public class ConferenceDetailAppViewModel
//    {
//        public Guid Uid { get; set; }
//        public string Title { get; set; }
//        public string Room { get; set; }
//        public DateTime? Date { get; set; }
//        public string StartTime { get; set; }
//        public string EndTime { get; set; }
//        public DateTime CreationDate { get; set; }
//        public string Synopsis { get; set; }

//        public IEnumerable<LecturerGroupAppViewModel> Lecturers{ get; set; }

//        public ConferenceDetailAppViewModel()
//        {

//        }

//        public ConferenceDetailAppViewModel(Conference entity)
//        {
//            Uid = entity.Uid;
//            CreationDate = entity.CreateDate;

//            Title = entity.GetTitle();

//            if (entity.Room != null)
//            {
//                Room = entity.Room.GetName();
//            }

//            Date = entity.Date;

//            if (entity.StartTime != null)
//            {
//                StartTime = entity.StartTime.Value.ToString("hh':'mm");
//            }

//            if (entity.EndTime != null)
//            {
//                EndTime = entity.EndTime.Value.ToString("hh':'mm");
//            }

//            Synopsis = entity.GetSinopsis();

//            if (entity.Lecturers != null && entity.Lecturers.Any())
//            {                
//                Lecturers = LecturerGroupAppViewModel.MapList(entity.Lecturers);
//            }
//        }

//        public static IEnumerable<ConferenceDetailAppViewModel> MapList(IEnumerable<Conference> entities)
//        {
//            foreach (var entity in entities)
//            {

//                yield return new ConferenceDetailAppViewModel(entity);
//            }
//        }
//    }
//}
