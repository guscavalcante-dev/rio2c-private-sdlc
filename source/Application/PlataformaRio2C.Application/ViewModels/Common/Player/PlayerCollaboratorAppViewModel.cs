//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerCollaboratorAppViewModel
//    {
//        public string HoldingName { get; set; }

//        [Display(Name = "Player", ResourceType = typeof(Labels))]
//        public Guid Uid { get; set; }

//        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        public bool RegisterComplete { get; set; }

//        public bool InterestFilled { get; set; }

//        public PlayerCollaboratorAppViewModel()
//        {

//        }

//        public PlayerCollaboratorAppViewModel(Player entity)
//        {
//            Uid = entity.Uid;
//            Name = entity.Name;

//            if (entity.Address != null)
//            {              
//                //RegisterComplete = entity.Address.ZipCode != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode) && entity.CompanyName != null && !string.IsNullOrWhiteSpace(entity.CompanyName);
//            }

//            InterestFilled = entity.Interests != null && entity.Interests.Any();

//            HoldingName = entity.Holding.Name;
//        }

//        public static IEnumerable<PlayerCollaboratorAppViewModel> MapList(IEnumerable<Player> entities)
//        {
//            foreach (var entity in entities)
//            {
//                yield return new PlayerCollaboratorAppViewModel(entity);
//            }
//        }
//    }
//}
