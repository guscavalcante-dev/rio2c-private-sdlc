namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerActivity : Entity
    {
        public int PlayerId { get; private set; }        
        public virtual Player Player { get; private set; }

        public int ActivityId { get; private set; }
        public virtual Activity Activity { get; private set; }

        protected PlayerActivity()
        {
        }

        public PlayerActivity(Player player, Activity activity)
        {
            SetPlayer(player);
            SetActivity(activity);
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            if (player != null)
            {
                PlayerId = player.Id;                
            }
        }

        public void SetActivity(Activity activity)
        {
            Activity = activity;
            if (activity != null)
            {
                ActivityId = activity.Id;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
