//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Linq;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerActivityAppViewModel : EntityViewModel<PlayerActivityAppViewModel, PlayerActivity>, IEntityViewModel<PlayerActivity>
//    {

//        public int PlayerId { get; set; }
//        public PlayerAppViewModel Player { get; set; }

//        public int ActivityId { get; set; }
//        public string ActivityName { get; set; }
//        public virtual ActivityAppViewModel Activity { get;  set; }

//        public bool Selected { get; set; }

//        public PlayerActivityAppViewModel()
//        {

//        }

//        public PlayerActivityAppViewModel(Activity  entity)
//        {
//            ActivityId = entity.Id;

//            ActivityName = entity.Name;
//        }

//        public PlayerActivityAppViewModel(PlayerActivity entity)
//        {
//            PlayerId = entity.PlayerId;
//            ActivityId = entity.ActivityId;

//            ActivityName = entity.Activity.Name;

//            Selected = entity.Player != null && entity.Player.PlayerActivitys != null && entity.Player.PlayerActivitys.Any(e => e.Activity.Name == entity.Activity.Name);
//        }

//        public static IEnumerable<PlayerActivityAppViewModel> MapList(IEnumerable<Activity> activities, Player player)
//        {
//            foreach (var activity in activities)
//            {
//                yield return new PlayerActivityAppViewModel(new PlayerActivity(player, activity));
//            }
//        }

//        public static IEnumerable<PlayerActivityAppViewModel> MapList(IEnumerable<Activity> activities)
//        {
//            foreach (var activity in activities)
//            {
//                yield return new PlayerActivityAppViewModel(activity);
//            }
//        }


//        public PlayerActivity MapReverse()
//        {
//            var entity = new PlayerActivity(Player.MapReverse(), Activity.MapReverse());
//            return entity;
//        }

//        public PlayerActivity MapReverse(Player p, Activity a)
//        {
//            var entity = new PlayerActivity(p, a);
//            return entity;
//        }

//        public PlayerActivity MapReverse(PlayerActivity entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
