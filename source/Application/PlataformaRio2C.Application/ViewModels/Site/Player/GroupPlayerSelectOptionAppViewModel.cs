//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class GroupPlayerSelectOptionAppViewModel
//    {
//        public string Title { get; set; }

//        public IEnumerable<PlayerSelectOptionAppViewModel> Players { get; set; }

//        public GroupPlayerSelectOptionAppViewModel()
//        {

//        }

//        public GroupPlayerSelectOptionAppViewModel(string title, IEnumerable<Player> players)
//        {
//            Title = title;
//            Players = PlayerSelectOptionAppViewModel.MapList(players);
//        }

//        public static IEnumerable<GroupPlayerSelectOptionAppViewModel> MapList(IEnumerable<IGrouping<string, Player>> gropEntities)
//        {
//            foreach (var group in gropEntities)
//            {
//                var countPlayersDistinct = group.Select(e => e.Name).Distinct().Count();

//                var title = countPlayersDistinct > 1 ? group.Key : null;

//                var players = group.OrderBy(e => e.Name).ToList();

//                yield return new GroupPlayerSelectOptionAppViewModel(title, players);
//            }
//        }

//    }
//}
