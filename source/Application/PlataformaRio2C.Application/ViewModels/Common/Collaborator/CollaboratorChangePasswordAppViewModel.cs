//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class CollaboratorChangePasswordAppViewModel
//    {
//        public Guid Uid { get; set; }
//        [Display(Name = "Photo", ResourceType = typeof(Labels))]
//        public ImageFileAppViewModel Image { get; set; }

//        [Display(Name = "FullName", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [Display(Name = "CurrentPassword", ResourceType = typeof(Labels))]
//        public string OldPassword { get; set; }

//        [Required]
//        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinimumNumberOfCharacters")]
//        [DataType(DataType.Password)]
//        [Display(Name = "NewPassword", ResourceType = typeof(Labels))]
//        public string NewPassword { get; set; }

//        [DataType(DataType.Password)]
//        [Display(Name = "ConfirmPassword", ResourceType = typeof(Labels))]
//        [Compare("NewPassword", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordConfirmationDoesNotMatch")]
//        public string ConfirmPassword { get; set; }

//        public CollaboratorChangePasswordAppViewModel()
//        {

//        }
//    }
//}
