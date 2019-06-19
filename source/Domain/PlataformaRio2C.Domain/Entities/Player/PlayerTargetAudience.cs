namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerTargetAudience : Entity
    {
        public int PlayerId { get; private set; }
        public virtual Player Player { get; private set; }

        public int TargetAudienceId { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }

        protected PlayerTargetAudience()
        {            
        }

        public PlayerTargetAudience(Player player, TargetAudience targetAudience)
        {
            SetPlayer(player);
            SetTargetAudience(targetAudience);
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            if (player != null)
            {
                PlayerId = player.Id;
            }
        }

        public void SetTargetAudience(TargetAudience targetAudience)
        {
            TargetAudience = targetAudience;
            if (targetAudience != null)
            {
                TargetAudienceId = targetAudience.Id;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
