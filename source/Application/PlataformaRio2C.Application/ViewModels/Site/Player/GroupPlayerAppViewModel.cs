//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class GroupPlayerAppViewModel
//    {
//        public string  Title { get; set; }

//        public IEnumerable<PlayerProducerAreaAppViewModel> Players { get; set; }

//        public GroupPlayerAppViewModel()
//        {

//        }

//        public GroupPlayerAppViewModel(string title, IEnumerable<Player> players)
//        {
//            Title = title;
//            Players = PlayerProducerAreaAppViewModel.MapList(players);
//        }       

//        public static IEnumerable<GroupPlayerAppViewModel> MapList(IEnumerable<IGrouping<string, Player>> gropEntities)
//        {
//            foreach (var group in gropEntities)
//            {
//                var countPlayersDistinct = group.Select(e => e.Name).Distinct().Count();

//                var title = countPlayersDistinct > 1 ? group.Key : null;


//                var players = group.OrderBy(e => e.Name).ToList();

//                yield return new GroupPlayerAppViewModel(title, players);
//            }
//        }
//    }
//}
