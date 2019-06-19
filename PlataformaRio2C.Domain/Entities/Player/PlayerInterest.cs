using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerInterest : Entity
    {
        public int PlayerId { get; private set; }
        public virtual Guid PlayerUid { get; private set; }
        public virtual Player Player { get; private set; }            
        public int InterestId { get; private set; }
        public virtual Guid InterestUid { get; private set; }
        public virtual Interest Interest { get; private set; }

        protected PlayerInterest()
        {

        }

        public PlayerInterest(Player player, Interest interest)
        {
            SetPlayer(player);
            SetInterest(interest);
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            if (player != null)
            {
                PlayerId = player.Id;
                PlayerUid = player.Uid;
            }
        }

        public void SetInterest(Interest interest)
        {
            Interest = interest;
            if (interest != null)
            {
                InterestId = interest.Id;
                InterestUid = interest.Uid;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
