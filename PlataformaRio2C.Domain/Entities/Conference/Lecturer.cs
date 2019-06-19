using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Lecturer : Entity
    {
        public static readonly int NameMaxLength = 1000;
        public static readonly int LecturerRoleMaxLength = 200;        
        
        public string Name { get; private set; }
        public int? ImageId { get; private set; }
        public virtual ImageFile Image { get; private set; }        
        public string Email { get; private set; }
        public string CompanyName { get; private set; }
        public virtual ICollection<LecturerJobTitle> JobTitles { get; private set; }
        protected Lecturer() { }

        public Lecturer(string name)
        {
            SetName(name);
        }       

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetEmail(string val)
        {
            Email = val;
        }

        public void SetCompanyName(string val)
        {
            CompanyName = val;
        }       

        public void SetImage(ImageFile image)
        {
            Image = image;
            if (image != null)
            {
                ImageId = image.Id;
            }
        }
        
        public void SetJobTitles(IEnumerable<LecturerJobTitle> jobTitles)
        {
            JobTitles = jobTitles.ToList();
        }

        public string GetJobTitle()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string titlePt = JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            string titleEn = JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

            if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
            {
                return titlePt;
            }
            else if (!string.IsNullOrWhiteSpace(titleEn))
            {
                return titleEn;
            }
            else
            {
                return null;
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();            

            if (Image != null)
            {
                ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
                ValidationResult.Add(new ImageResolutionIsConsistent().Valid(this.Image));
            }

            return ValidationResult.IsValid;
        }
    }
}
