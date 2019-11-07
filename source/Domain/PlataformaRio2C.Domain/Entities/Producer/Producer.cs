using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Producer : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly int WebSiteMaxLength = 100;
        public static readonly int SocialMediaMaxLength = 256;
        public static readonly int TradeNameMaxLength = 100;
        public static readonly int ImageDpi = 300;
        public static readonly int ImageWidth = 472;
        public static readonly int ImageHeight = 472;
        public static readonly int ProjectCountMax = 3;

        public string Name { get; private set; }
        public string TradeName { get; private set; }
        public string CNPJ { get; private set; }
        public string Website { get; private set; }
        public string SocialMedia { get; private set; }
        public string PhoneNumber { get; private set; }
        public int? ImageId { get; private set; }
        public virtual ImageFile Image { get; private set; }
        public int? AddressId { get; private set; }
        public virtual Address Address { get; private set; }

        public virtual ICollection<ProducerDescription> Descriptions { get; private set; }

        public virtual ICollection<ProducerActivity> ProducerActivitys { get; private set; }

        public virtual ICollection<ProducerTargetAudience> ProducerTargetAudience { get; private set; }

        public virtual ICollection<CollaboratorProducer> EventsCollaborators { get; private set; }

        //public virtual ICollection<Project> Projects { get; private set; }


        protected Producer()
        {

        }

        public Producer(string cnpj)
        {
            SetCNPJ(cnpj);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetSocialMedia(string value)
        {
            SocialMedia = value;
        }

        public void SetTradeName(string value)
        {
            TradeName = value;
        }

        public void SetCNPJ(string cnpj)
        {
            CNPJ = cnpj;
        }

        public void SetWebsite(string value)
        {
            Website = value;
        }

        public void SetPhoneNumber(string value)
        {
            PhoneNumber = value;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }

        public void SetDescriptions(IEnumerable<ProducerDescription> descriptions)
        {
            Descriptions = descriptions.ToList();
        }

        public void SetImage(ImageFile image)
        {
            ImageId = null;
            Image = image;
            if (image != null)
            {
                ImageId = image.Id;
            }
        }

        public void SetProducerActivitys(IEnumerable<ProducerActivity> activities)
        {
            if (ProducerActivitys != null && ProducerActivitys.Any())
            {
                ProducerActivitys.Clear();
                ProducerActivitys = null;
            }

            if (activities != null && activities.Any())
            {
                ProducerActivitys = activities.ToList();
            }
        }

        public void SetProducerTargetAudience(IEnumerable<ProducerTargetAudience> targetAudience)
        {
            if (ProducerTargetAudience != null && ProducerTargetAudience.Any())
            {
                ProducerTargetAudience.Clear();
                ProducerTargetAudience = null;
            }

            if (targetAudience != null && targetAudience.Any())
            {
                ProducerTargetAudience = targetAudience.ToList();
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new ProducerIsConsistent().Valid(this));

            if (Image != null)
            {
                ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
                ValidationResult.Add(new ProducerImageIsConsistent().Valid(this));
            }

            return ValidationResult.IsValid;
        }


        public string GetLintCnpj()
        {
            var value = GetLintCnpj(CNPJ);
            return value;
        }

        public static string GetLintCnpj(string value)
        {
            if (value != null)
            {


                StringBuilder sb = new StringBuilder();
                foreach (char c in value)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }

            return null;

            //if (value != null)
            //{
            //    value = value.Trim().Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            //}

            //return value;
        }

        public string GetDescription()
        {
            if (Descriptions != null && Descriptions.Any())
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                string descriptionPt = Descriptions.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
                string descriptionEn = Descriptions.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

                if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(descriptionPt))
                {
                    return descriptionPt;
                }
                else if (!string.IsNullOrWhiteSpace(descriptionEn))
                {
                    return descriptionEn;
                }
            }

            return null;
        }

        public string LintCnpj
        {
            get
            {
                return GetLintCnpj();
            }
        }
    }
}
