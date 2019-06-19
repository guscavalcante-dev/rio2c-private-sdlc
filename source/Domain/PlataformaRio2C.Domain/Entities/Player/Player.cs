using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Player : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly int CompanyNameMaxLength = 100;
        public static readonly int WebSiteMaxLength = 100;
        public static readonly int SocialMediaMaxLength = 256;
        public static readonly int TradeNameMaxLength = 100;
        public static readonly double ImageMinMByteSize = 0.001f;
        public static readonly double ImageMaxMByteSize = 10f;

        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string TradeName { get; private set; }
        public string CNPJ { get; private set; }
        public string Website { get; private set; }
        public string SocialMedia { get; private set; }
        public string PhoneNumber { get; private set; }
        public int? ImageId { get; private set; }
        public virtual ImageFile Image { get; private set; }
        public int? AddressId { get; private set; }
        public virtual Address Address { get; private set; }
        public int HoldingId { get; private set; }
        public virtual Guid HoldingUid { get; private set; }
        public virtual Holding Holding { get; private set; }
        public virtual ICollection<PlayerDescription> Descriptions { get; private set; }
        public virtual ICollection<PlayerInterest> Interests { get; private set; }
        public virtual ICollection<Collaborator> Collaborators { get; private set; }
        public virtual ICollection<Collaborator> CollaboratorsOld { get; private set; }
        public virtual ICollection<PlayerActivity> PlayerActivitys { get; private set; }
        public virtual ICollection<PlayerTargetAudience> PlayerTargetAudience { get; private set; }
        public virtual ICollection<PlayerRestrictionsSpecifics> RestrictionsSpecifics { get; private set; }

        protected Player()
        {

        }

        public Player(string name, Holding holding)
        {
            SetName(name);
            SetHolding(holding);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetCompanyName(string value)
        {
            CompanyName = value;
        }

        public void SetSocialMedia(string value)
        {
            SocialMedia = value;
        }

        public void SetTradeName(string value)
        {
            TradeName = value;
        }

        public void SetHolding(Holding holding)
        {
            Holding = holding;
            if (holding != null)
            {
                HoldingId = holding.Id;
                HoldingUid = holding.Uid;
            }
        }

        public void SetHoldingUid(Guid uid)
        {
            HoldingUid = uid;
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

        public void SetDescriptions(IEnumerable<PlayerDescription> descriptions)
        {
            Descriptions = descriptions.ToList();
        }

        public void SetPlayerActivitys(IEnumerable<PlayerActivity> activities)
        {
            if (PlayerActivitys != null && PlayerActivitys.Any())
            {
                PlayerActivitys.Clear();
                PlayerActivitys = null;
            }

            if (activities != null && activities.Any())
            {
                PlayerActivitys = activities.ToList();
            }
        }

        public void SetPlayerTargetAudience(IEnumerable<PlayerTargetAudience> targetAudience)
        {
            if (PlayerTargetAudience != null && PlayerTargetAudience.Any())
            {
                PlayerTargetAudience.Clear();
                PlayerTargetAudience = null;
            }

            if (targetAudience != null && targetAudience.Any())
            {
                PlayerTargetAudience = targetAudience.ToList();
            }
        }

        public void SetRestrictionsSpecifics(IEnumerable<PlayerRestrictionsSpecifics> entities)
        {
            if (RestrictionsSpecifics != null && RestrictionsSpecifics.Any())
            {
                RestrictionsSpecifics.Clear();
                RestrictionsSpecifics = null;
            }

            if (entities != null && entities.Any())
            {
                RestrictionsSpecifics = entities.ToList();
            }
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

        public string GetRestrictionSpecifics()
        {
            if (RestrictionsSpecifics != null && RestrictionsSpecifics.Any())
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                string descriptionPt = RestrictionsSpecifics.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
                string descriptionEn = RestrictionsSpecifics.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

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

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new PlayerIsConsistent().Valid(this));

            if (Image != null)
            {
                ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
                ValidationResult.Add(new PlayerImageIsConsistent().Valid(this));
            }

            return ValidationResult.IsValid;
        }
    }
}
