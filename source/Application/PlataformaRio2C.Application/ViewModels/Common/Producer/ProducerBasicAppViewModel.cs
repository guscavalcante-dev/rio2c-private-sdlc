//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerBasicAppViewModel : EntityViewModel<ProducerBasicAppViewModel, Producer>, IEntityViewModel<Producer>
//    {
//        #region props
//        [Display(Name = "Producer", ResourceType = typeof(Labels))]
//        public override Guid Uid { get; set; }

//        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
//        public string Name { get; set; }
        
//        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
//        public string TradeName { get; set; }

//        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
//        public string SocialMedia { get; set; }
        
//        [Display(Name = "CNPJ", ResourceType = typeof(Labels))]
//        public string CNPJ { get; set; }

//        [Display(Name = "Website", ResourceType = typeof(Labels))]
//        public string Website { get; set; }

//        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
//        public string PhoneNumber { get; set; }

//        [Display(Name = "Address", ResourceType = typeof(Labels))]
//        public AddressAppViewModel Address { get; set; }       

//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public ImageFileAppViewModel Image { get; set; }

//        [Display(Name = "Photo", ResourceType = typeof(Labels))]
//        public ImageFileAppViewModel NewImage { get; set; }

//        [Display(Name = "Image", ResourceType = typeof(Labels))]
//        public HttpPostedFileBase ImageUpload { get; set; }

//        [Display(Name = "CompanyProfile", ResourceType = typeof(Labels))]
//        public IEnumerable<ProducerDescriptionAppViewModel> Descriptions { get; set; }

//        public bool RegisterComplete { get; set; }
//        public bool InterestFilled { get; set; }

//        #endregion

//        #region ctor

//        public ProducerBasicAppViewModel()
//        {
//            Address = new AddressAppViewModel();
//            Descriptions = new List<ProducerDescriptionAppViewModel>();           
//        }

//        public ProducerBasicAppViewModel(Producer entity)
//            :base(entity)
//        {
//            Name = entity.Name;
//            CNPJ = entity.CNPJ;
//            Website = entity.Website;
//            SocialMedia = entity.SocialMedia;
//            TradeName = entity.TradeName;
//            PhoneNumber = entity.PhoneNumber;

//            if (entity.Address != null)
//            {
//                Address = new AddressAppViewModel(entity.Address);
//                RegisterComplete = Address.ZipCode != null && !string.IsNullOrWhiteSpace(Address.ZipCode);
//            }
//            else
//            {
//                Address = new AddressAppViewModel();
//            }

//            if (entity.Descriptions != null && entity.Descriptions.Any())
//            {
//                Descriptions = ProducerDescriptionAppViewModel.MapList(entity.Descriptions).ToList();
//            }

//            //if (entity.Image != null)
//            //{
//            //    Image = new ImageFileAppViewModel(entity.Image);
//            //}            
//        }

//        #endregion

//        #region Public methods

//        public Producer MapReverse()
//        {
//            var entity = new Domain.Entities.Producer(this.Name);
//            if (Uid != Guid.Empty)
//            {
//                entity.SetUid(Uid);
//            }

//            entity.SetCNPJ(CNPJ);
//            entity.SetWebsite(Website);
//            entity.SetPhoneNumber(PhoneNumber);
//            entity.SetSocialMedia(SocialMedia);
//            entity.SetTradeName(TradeName);           

//            if (Address != null)
//            {
//                entity.SetAddress(this.Address.MapReverse());
//            }

//            if (ImageUpload != null && Image == null)
//            {
//                Image = new ImageFileAppViewModel(ImageUpload);

//                if (NewImage != null && Image != null)
//                {
//                    Image.File = NewImage.File;
//                    Image.ContentLength = NewImage.File.Length;
//                }


//                entity.SetImage(Image.MapReverse());
//            }           

//            return entity;
//        }

//        public Producer MapReverse(Producer entity)
//        {
//            var tt = this;
//            entity.SetName(Name);
//            entity.SetCNPJ(CNPJ);
//            entity.SetWebsite(Website);
//            entity.SetPhoneNumber(PhoneNumber);
//            entity.SetSocialMedia(SocialMedia);
//            entity.SetTradeName(TradeName);

//            if (Address != null)
//            {
//                if (entity.Address != null)
//                {
//                    entity.SetAddress(this.Address.MapReverse(entity.Address));
//                }
//                else
//                {
//                    entity.SetAddress(this.Address.MapReverse());
//                }
//            }           

//            if (ImageUpload != null && Image == null)
//            {
//                Image = new ImageFileAppViewModel(ImageUpload);
//            }

//            if (NewImage != null && Image != null)
//            {
//                Image.File = NewImage.File;
//                Image.ContentLength = NewImage.File.Length;
//            }

//            if (Image != null)
//            {
//                if (entity.Image != null)
//                {
//                    entity.SetImage(Image.MapReverse(entity.Image));
//                }
//                else
//                {
//                    entity.SetImage(Image.MapReverse());
//                }
//            }
//            else
//            {
//                entity.SetImage(null);
//            }           

//            return entity;
//        }

//        #endregion
               
//    }
//}
