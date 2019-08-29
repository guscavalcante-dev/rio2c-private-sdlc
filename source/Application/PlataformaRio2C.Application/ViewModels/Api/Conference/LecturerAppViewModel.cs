using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Globalization;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels.Api
{
    public class LecturerAppViewModel : EntityViewModel<LecturerAppViewModel, ConferenceLecturer>
    {
        public bool HasImage { get; set; }
        public bool IsPreRegistered { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }

        public CollaboratorOptionAppViewModel Collaborator { get; set; }

        public LecturerAppViewModel()
            : base()
        {
            Uid = Guid.NewGuid();
        }

        public LecturerAppViewModel(ConferenceLecturer entity)
            : base(entity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            IsPreRegistered = entity.IsPreRegistered;
                  
            if (IsPreRegistered)
            {
                if (entity.Collaborator != null)
                {
                    Collaborator = new CollaboratorOptionAppViewModel(entity.Collaborator);
                    Name = entity.Collaborator.FirstName;
                    Email = entity.Collaborator.User.Email;
                    JobTitle = entity.Collaborator.GetJobTitle();
                    //HasImage = entity.Collaborator.ImageId > 0;
                    CompanyName = entity.Collaborator.GetCompanyName();
                }                         
            }
            else
            {
                if (entity.Lecturer != null)
                {
                    Name = entity.Lecturer.Name;
                    Email = entity.Lecturer.Email;
                    CompanyName = entity.Lecturer.CompanyName;
                    JobTitle = entity.Lecturer.GetJobTitle();
                    HasImage = entity.Lecturer.ImageId > 0;
                }
            }
        }
    }
}
