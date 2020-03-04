namespace PlataformaRio2C.Domain.Entities
{
    public class NegotiationRoomConfig : Entity
    {
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int CountAutomaticTables { get; set; }
        public int CountManualTables { get; set; }
        public int NegotiationConfigId { get; set; }
        public virtual NegotiationConfig NegotiationConfig { get; set; }

        protected NegotiationRoomConfig()
        {

        }

        public NegotiationRoomConfig(int countAutomaticTables)
        {
            SetCountAutomaticTables(countAutomaticTables);
        }

        public NegotiationRoomConfig(Room room)
        {
            SetRoom(room);
        }

        public void SetRoom(Room room)
        {
            Room = room;
            if (room != null)
            {
                RoomId = room.Id;
            }            
        }        

        public void SetCountAutomaticTables(int val)
        {
            CountAutomaticTables = val;
        }

        public void SetCountManualTables(int val)
        {
            CountManualTables = val;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
