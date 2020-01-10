//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class CollaboratorBasicDetailAppViewModel : EntityViewModel<CollaboratorBasicDetailAppViewModel, Collaborator>, IEntityViewModel<Collaborator>
//    {
//        [Display(Name = "FullName", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
//        public IEnumerable<CollaboratorJobTitleAppViewModel> JobTitles { get; set; }

//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public ImageFileAppViewModel Image { get; set; }

//        public bool RegisterComplete { get; set; }

//        public CollaboratorBasicDetailAppViewModel()
//            :base()
//        {

//        }

//        public CollaboratorBasicDetailAppViewModel(Collaborator entity)
//            : base(entity)
//        {
//            Name = entity.FirstName;

//            if (entity.Address != null)
//            {
//                //RegisterComplete = entity.Address.ZipCode != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode);
//            }

//            //if (entity.JobTitles != null && entity.JobTitles.Any())
//            //{
//            //    JobTitles = CollaboratorJobTitleAppViewModel.MapList(entity.JobTitles).ToList();
//            //}

//            //if (entity.Image != null)
//            //{
//            //    Image = new ImageFileAppViewModel(entity.Image);
//            //}
//        }

//        public Collaborator MapReverse()
//        {
//            throw new NotImplementedException();
//        }

//        public Collaborator MapReverse(Collaborator entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
