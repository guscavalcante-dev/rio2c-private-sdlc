//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class NegotiationRoomConfigAppViewModel : EntityViewModel<NegotiationRoomConfigAppViewModel, NegotiationRoomConfig>, IEntityViewModel<NegotiationRoomConfig>
//    {
//        public int RoomId { get; set; }
//        public Guid RoomUid { get; set; }
//        public string RoomName { get; set; }
//        public int CountAutomaticTables { get; set; }
//        public int CountManualTables { get; set; }

//        public NegotiationRoomConfigAppViewModel()
//            : base()
//        {

//        }

//        public NegotiationRoomConfigAppViewModel(NegotiationRoomConfig entity)
//           : base(entity)
//        {
//            if (entity.Room != null)
//            {
//                RoomUid = entity.Room.Uid;
//                RoomName = entity.Room.GetName();
//            }

//            CountAutomaticTables = entity.CountAutomaticTables;
//            CountManualTables = entity.CountManualTables;
//        }

//        public NegotiationRoomConfig MapReverse()
//        {
//            var entity = new NegotiationRoomConfig(CountAutomaticTables);
//            entity.SetCountManualTables(CountManualTables);

//            return entity;
//        }

//        public NegotiationRoomConfig MapReverse(NegotiationRoomConfig entity)
//        {
//            entity.SetCountAutomaticTables(CountManualTables);
//            entity.SetCountManualTables(CountManualTables);

//            return entity;
//        }
//    }


//}
