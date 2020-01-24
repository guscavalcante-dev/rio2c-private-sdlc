//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class CollaboratorStatusRegisterAppViewModel
//    {
//        public bool RegisterComplete { get; set; }
//        public bool PlayersRegisterComplete { get; set; }
//        public bool PlayersInterestFilled { get; set; }
//        public bool ProducersRegisterComplete { get; set; }

//        public CollaboratorStatusRegisterAppViewModel()
//        {
//        }

//        public CollaboratorStatusRegisterAppViewModel(Collaborator entity)
//        {
//            //RegisterComplete = entity.Address != null && !string.IsNullOrWhiteSpace(entity.Address.ZipCode);

//            //if (entity.Players != null && entity.Players.Any())
//            //{
//            //    //PlayersRegisterComplete = !entity.Players.Any(p => p.TradeName == null  || p.Address == null || string.IsNullOrWhiteSpace(p.Address.ZipCode));

//            //    //PlayersRegisterComplete = !entity.Players.Any(p => p.CompanyName == null || p.Address == null || string.IsNullOrWhiteSpace(p.Address.ZipCode));
//            //    PlayersInterestFilled = !entity.Players.Any(p => p.Interests == null || !p.Interests.Any());
//            //}

//            //if (entity.ProducersEvents != null && entity.ProducersEvents.Any())
//            //{
//            //    var producers = entity.ProducersEvents.Select(e => e.Producer);

//            //    //ProducersRegisterComplete = !producers.Any(p => p.Address == null || string.IsNullOrWhiteSpace(p.Address.ZipCode));
//            //}
//        }
//    }
//}
