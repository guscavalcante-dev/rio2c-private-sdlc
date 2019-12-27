//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Application
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="PlayerInterestAppViewModel.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Collections.Generic;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    /// <summary>PlayerInterestAppViewModel</summary>
//    public class PlayerInterestAppViewModel : EntityViewModel<PlayerInterestAppViewModel, PlayerInterest>, IEntityViewModel<PlayerInterest>
//    {
//        public int PlayerId { get; set; }
//        public PlayerAppViewModel Player { get; set; }
//        public int EventId { get; set; }
//        public EventAppViewModel Event { get; set; }

//        public int InterestId { get; set; }        

//        public string InterestName { get; set; }
//        public string InterestGroupName { get; set; }
//        public string InterestGroupType { get; set; }
//        public InterestAppViewModel Interest { get; set; }
//        public bool Selected { get; set; }

//        public PlayerInterestAppViewModel()
//        {

//        }

//        public PlayerInterestAppViewModel(PlayerInterest entity)
//        {
//            PlayerId = entity.PlayerId;            
//            InterestId = entity.InterestId;

//            InterestName = entity.Interest.Name;
//            InterestGroupName = entity.Interest.InterestGroup.Name;
//            InterestGroupType = entity.Interest.InterestGroup.Type;

//            Player = new PlayerAppViewModel(entity.Player);            
//            Interest = new InterestAppViewModel(entity.Interest);

//            Selected = entity.Player != null && entity.Player.Interests != null && entity.Player.Interests.Any(e => e.Interest.Name == entity.Interest.Name);
//        }

//        public static IEnumerable<PlayerInterestAppViewModel> MapList(IEnumerable<Interest> interests, Player player)
//        {
//            foreach (var interest in interests)
//            {
//                yield return new PlayerInterestAppViewModel(new PlayerInterest(player, interest));
//            }
//        }

//        public PlayerInterest MapReverse()
//        {
//            var entity = new PlayerInterest(Player.MapReverse(), Interest.MapReverse());
//            return entity;
//        }

//        public PlayerInterest MapReverse(Player p, Interest i, Edition v)
//        {
//            var entity = new PlayerInterest(p, i);
//            return entity;
//        }

//        public PlayerInterest MapReverse(PlayerInterest entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
