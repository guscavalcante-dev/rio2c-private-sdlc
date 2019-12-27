//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerOptionAppViewModel
//    {
//        public Guid Uid { get; set; }

//        [Display(Name = "Name", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        public string HoldingName { get; set; }

//        public PlayerOptionAppViewModel()
//        {

//        }

//        public PlayerOptionAppViewModel(Player entity)
//        {
//            Uid = entity.Uid;
//            Name = entity.Name;
//            HoldingName = entity.Holding.Name;
//        }

//        public static IEnumerable<PlayerOptionAppViewModel> MapList(IEnumerable<Player> entities)
//        {
//            foreach (var entity in entities)
//            {
//                yield return new PlayerOptionAppViewModel(entity);
//            }
//        }
//    }
//}
