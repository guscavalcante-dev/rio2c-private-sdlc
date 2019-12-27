//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Linq;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerTargetAudienceAppViewModel : EntityViewModel<PlayerTargetAudienceAppViewModel, PlayerTargetAudience>, IEntityViewModel<PlayerTargetAudience>
//    {
//        public int PlayerId { get; set; }
//        public PlayerAppViewModel Player { get; set; }

//        public int TargetAudienceId { get; set; }
//        public string TargetAudienceName { get; set; }
//        public virtual TargetAudienceAppViewModel TargetAudience { get; set; }

//        public bool Selected { get; set; }

//        public PlayerTargetAudienceAppViewModel()
//        {

//        }

//        public PlayerTargetAudienceAppViewModel(TargetAudience entity)
//        {
//            TargetAudienceId = entity.Id;
//            TargetAudienceName = entity.Name;
//        }

//        public PlayerTargetAudienceAppViewModel(PlayerTargetAudience entity)
//        {
//            PlayerId = entity.PlayerId;
//            TargetAudienceId = entity.TargetAudienceId;
//            TargetAudienceName = entity.TargetAudience.Name;

//            Selected = entity.Player != null && entity.Player.PlayerTargetAudience != null && entity.Player.PlayerTargetAudience.Any(e => e.TargetAudience.Name == entity.TargetAudience.Name);
//        }

//        public static IEnumerable<PlayerTargetAudienceAppViewModel> MapList(IEnumerable<TargetAudience> activities, Player player)
//        {
//            foreach (var activity in activities)
//            {
//                yield return new PlayerTargetAudienceAppViewModel(new PlayerTargetAudience(player, activity));
//            }
//        }

//        public static IEnumerable<PlayerTargetAudienceAppViewModel> MapList(IEnumerable<TargetAudience> entities)
//        {
//            foreach (var entity in entities)
//            {
//                yield return new PlayerTargetAudienceAppViewModel(entity);
//            }
//        }

//        public PlayerTargetAudience MapReverse(Player p, TargetAudience t)
//        {
//            var entity = new PlayerTargetAudience(p, t);
//            return entity;
//        }

//        public PlayerTargetAudience MapReverse()
//        {
//            var entity = new PlayerTargetAudience(Player.MapReverse(), TargetAudience.MapReverse());
//            return entity;
//        }

//        public PlayerTargetAudience MapReverse(PlayerTargetAudience entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
