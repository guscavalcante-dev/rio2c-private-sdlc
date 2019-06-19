using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerBasicAppViewModel : EntityViewModel<PlayerBasicAppViewModel, Player>, IEntityViewModel<Player>
    {
        #region props
        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public override Guid Uid { get; set; }

        [Display(Name = "Player", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        public string TradeName { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        public string SocialMedia { get; set; }
        
        [Display(Name = "CNPJ", ResourceType = typeof(Labels))]
        public string CNPJ { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        public string Website { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Labels))]
        public AddressAppViewModel Address { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        public string HoldingName { get; set; }

        public Guid HoldingUid { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel Image { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel NewImage { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Labels))]
        public HttpPostedFileBase ImageUpload { get; set; }

        [Display(Name = "CompanyProfile", ResourceType = typeof(Labels))]
        public IEnumerable<PlayerDescriptionAppViewModel> Descriptions { get; set; }
        
        [Display(Name = "RestrictionsSpecifics", ResourceType = typeof(Labels))]
        public IEnumerable<PlayerRestrictionsSpecificsAppViewModel> RestrictionsSpecifics { get; set; }

        public bool RegisterComplete { get; set; }
        public bool InterestFilled { get; set; }

        #endregion

        #region ctor

        public PlayerBasicAppViewModel()
        {
            Address = new AddressAppViewModel();
            Descriptions = new List<PlayerDescriptionAppViewModel>();            
            RestrictionsSpecifics = new List<PlayerRestrictionsSpecificsAppViewModel>();
        }

        public PlayerBasicAppViewModel(Player entity)
            :base(entity)
        {
            Name = entity.Name;
            CNPJ = entity.CNPJ;
            CompanyName = entity.CompanyName;
            Website = entity.Website;
            SocialMedia = entity.SocialMedia;
            TradeName = entity.TradeName;
            PhoneNumber = entity.PhoneNumber;

            if (entity.Address != null)
            {
                Address = new AddressAppViewModel(entity.Address);
                RegisterComplete = Address.ZipCode != null && !string.IsNullOrWhiteSpace(Address.ZipCode) && entity.TradeName != null && !string.IsNullOrWhiteSpace(entity.TradeName);
            }
            else
            {
                Address = new AddressAppViewModel();
            }

            if (entity.Holding != null)
            {
                HoldingName = entity.Holding.Name;
                HoldingUid = entity.Holding.Uid;
            }

            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                Descriptions = PlayerDescriptionAppViewModel.MapList(entity.Descriptions).ToList();
            }

            if (entity.RestrictionsSpecifics != null && entity.RestrictionsSpecifics.Any())
            {
                RestrictionsSpecifics = PlayerRestrictionsSpecificsAppViewModel.MapList(entity.RestrictionsSpecifics).ToList();
            }

            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}

            InterestFilled = entity.Interests != null && entity.Interests.Any();
        }

        #endregion

        #region Public methods

        public Player MapReverse()
        {
            Holding holding = null;

            var player = new Domain.Entities.Player(this.Name, holding);
            if (Uid != Guid.Empty)
            {
                player.SetUid(Uid);
            }

            player.SetCNPJ(CNPJ);
            player.SetCompanyName(CompanyName);
            player.SetWebsite(Website);
            player.SetPhoneNumber(PhoneNumber);
            player.SetSocialMedia(SocialMedia);
            player.SetTradeName(TradeName);

            if (HoldingUid != Guid.Empty)
            {
                player.SetHoldingUid(HoldingUid);
            }

            if (Address != null)
            {
                player.SetAddress(this.Address.MapReverse());
            }

            if (ImageUpload != null && Image == null)
            {
                Image = new ImageFileAppViewModel(ImageUpload);

                if (NewImage != null && Image != null)
                {
                    Image.File = NewImage.File;
                    Image.ContentLength = NewImage.File.Length;
                }


                player.SetImage(Image.MapReverse());
            }

            if (Descriptions != null && Descriptions.Any())
            {
                var descriptions = new List<PlayerDescription>();
                foreach (var description in Descriptions)
                {
                    description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
                    descriptions.Add(description.MapReverse());
                }

                player.SetDescriptions(descriptions);
            }

            return player;
        }

        public Player MapReverse(Player entity)
        {
            var tt = this;
            entity.SetName(Name);
            entity.SetCNPJ(CNPJ);
            entity.SetCompanyName(CompanyName);
            entity.SetWebsite(Website);
            entity.SetPhoneNumber(PhoneNumber);
            entity.SetSocialMedia(SocialMedia);
            entity.SetTradeName(TradeName);

            if (Address != null)
            {
                if (entity.Address != null)
                {
                    entity.SetAddress(this.Address.MapReverse(entity.Address));
                }
                else
                {
                    entity.SetAddress(this.Address.MapReverse());
                }
            }

            if (HoldingUid != Guid.Empty)
            {
                entity.SetHoldingUid(HoldingUid);
            }

            if (ImageUpload != null && Image == null)
            {
                Image = new ImageFileAppViewModel(ImageUpload);
            }

            if (NewImage != null && Image != null)
            {
                Image.File = NewImage.File;
                Image.ContentLength = NewImage.File.Length;
            }

            if (Image != null)
            {
                if (entity.Image != null)
                {
                    entity.SetImage(Image.MapReverse(entity.Image));
                }
                else
                {
                    entity.SetImage(Image.MapReverse());
                }
            }
            else
            {
                entity.SetImage(null);
            }

            if (Descriptions != null && Descriptions.Any())
            {
                var descriptions = new List<PlayerDescription>();
                foreach (var description in Descriptions)
                {
                    description.SetLanguage(new LanguageAppViewModel(description.LanguageCode));
                    descriptions.Add(description.MapReverse());
                }

                entity.SetDescriptions(descriptions);
            }

            return entity;
        }

        #endregion
               
    }
}
