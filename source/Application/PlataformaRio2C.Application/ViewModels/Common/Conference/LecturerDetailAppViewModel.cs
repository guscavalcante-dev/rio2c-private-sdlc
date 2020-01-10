//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Web;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class LecturerDetailAppViewModel : EntityViewModel<LecturerDetailAppViewModel, ConferenceLecturer>, IEntityViewModel<ConferenceLecturer>
//    {
//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public ImageBase64AppViewModel Image { get; set; }

//        public string ImageName { get; set; }
//        public string ImageBase64 { get; set; }

//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public HttpPostedFileBase ImageUpload { get; set; }

//        [Display(Name = "Photo", ResourceType = typeof(Labels))]
//        public ImageFileAppViewModel NewImage { get; set; }
//        public bool IsPreRegistered { get; set; }

//        public string Name { get; set; }

//        public Guid RoleLecturerUid { get; set; }
//        public string RoleLecturerName { get; set; }

//        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
//        public IEnumerable<LecturerJobTitleAppViewModel> JobTitles { get; set; }

//        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
//        public string JobTitle { get; set; }

//        public string Email { get; set; }
//        public string CompanyName { get; set; }

//        public CollaboratorOptionAppViewModel Collaborator { get; set; }

//        public LecturerDetailAppViewModel()
//            : base()
//        {
//            Uid = Guid.NewGuid();
//        }

//        public LecturerDetailAppViewModel(ConferenceLecturer entity)
//            : base(entity)
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            IsPreRegistered = entity.IsPreRegistered;
//            if (entity.RoleLecturer != null)
//            {
//                RoleLecturerUid = entity.RoleLecturer.Uid;
//                if (currentCulture != null && currentCulture.Name == "pt-BR")
//                {
//                    RoleLecturerName = entity.RoleLecturer.Titles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
//                }
//                else
//                {
//                    RoleLecturerName = entity.RoleLecturer.Titles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();
//                }
//            }




//            if (IsPreRegistered && entity.Collaborator != null)
//            {
//                Collaborator = new CollaboratorOptionAppViewModel(entity.Collaborator);
//                //Name = entity.Collaborator.Name;

//                //CompanyName = "";

//                //if (entity.Collaborator.Players != null && entity.Collaborator.Players.Any())
//                //{
//                //    CompanyName = string.Format("{0} - {1}", string.Join(",", entity.Collaborator.Players.Select(e => e.Holding.Name)), string.Join(", ", entity.Collaborator.Players.Select(e => e.Name)));
//                //}

//                //if (entity.Collaborator.ProducersEvents != null && entity.Collaborator.ProducersEvents.Any(e => !string.IsNullOrWhiteSpace(e.Producer.Name)))
//                //{
//                //    var namesProducers = entity.Collaborator.ProducersEvents.Select(e => e.Producer.Name).Where(e => !string.IsNullOrWhiteSpace(e)).Distinct();
//                //    CompanyName += string.Format("{0}", string.Join(", ", namesProducers));
//                //}


//                //if (entity.Collaborator.JobTitles != null && entity.Collaborator.JobTitles.Any())
//                //{
//                //    var jobTitles = CollaboratorJobTitleAppViewModel.MapList(entity.Collaborator.JobTitles).ToList();

//                //    if (currentCulture != null && currentCulture.Name == "pt-BR")
//                //    {
//                //        JobTitle = jobTitles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value).FirstOrDefault();
//                //    }
//                //    else
//                //    {
//                //        JobTitle = jobTitles.Where(e => e.LanguageCode == "En").Select(e => e.Value).FirstOrDefault();
//                //    }
//                //}
//            }
//            else
//            {
//                if (entity.Lecturer != null)
//                {
//                    Name = entity.Lecturer.Name;
//                    Email = entity.Lecturer.Email;
//                    CompanyName = entity.Lecturer.CompanyName;

//                    if (entity.Lecturer.Image != null)
//                    {
//                        Image = new ImageBase64AppViewModel(entity.Lecturer.Image);
//                    }

//                    if (entity.Lecturer.JobTitles != null && entity.Lecturer.JobTitles.Any())
//                    {
//                        JobTitles = LecturerJobTitleAppViewModel.MapList(entity.Lecturer.JobTitles).ToList();

//                        if (currentCulture != null && currentCulture.Name == "pt-BR")
//                        {
//                            JobTitle = JobTitles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value).FirstOrDefault();
//                        }
//                        else
//                        {
//                            JobTitle = JobTitles.Where(e => e.LanguageCode == "En").Select(e => e.Value).FirstOrDefault();
//                        }
//                    }
//                }
//            }
//        }

//        public ConferenceLecturer MapReverse()
//        {
//            var entity = new ConferenceLecturer(IsPreRegistered);

//            entity = MapReverse(entity);

//            return entity;
//        }

//        public ConferenceLecturer MapReverse(ConferenceLecturer entity)
//        {
//            if (!IsPreRegistered)
//            {
//                var entityLecturer = new Lecturer(Name);

//                entityLecturer.SetName(Name);
//                entityLecturer.SetEmail(Email);
//                entityLecturer.SetCompanyName(CompanyName);

//                if (ImageUpload != null && Image == null)
//                {
//                    Image = new ImageBase64AppViewModel(ImageUpload);

//                    if (NewImage != null && Image != null)
//                    {
//                        Image.File = Convert.ToBase64String(NewImage.File);
//                        Image.ContentLength = NewImage.File.Length;
//                    }

//                    entityLecturer.SetImage(Image.MapReverse());
//                }
//                else if (ImageUpload == null && Image != null)
//                {

//                    if (NewImage != null && Image != null)
//                    {
//                        Image.File = Convert.ToBase64String(NewImage.File);
//                        Image.ContentLength = NewImage.File.Length;
//                    }

//                    entityLecturer.SetImage(Image.MapReverse());
//                }

//                entity.SetLecturer(entityLecturer);
//            }

//            return entity;
//        }
//    }
//}
