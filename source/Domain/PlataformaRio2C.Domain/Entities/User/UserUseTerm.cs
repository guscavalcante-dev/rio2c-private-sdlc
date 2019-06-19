namespace PlataformaRio2C.Domain.Entities
{
    public class UserUseTerm : Entity
    {
        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int EventId { get; private set; }
        public virtual Event Event { get; private set; }
      
        public int RoleId { get; private set; }
        public virtual Role Role { get; private set; }

        protected UserUseTerm()
        {

        }

        public UserUseTerm(User user)
        {
            SetUser(user);
        }

        public UserUseTerm(int userId, int eventId)
        {            
            SetUserId(userId);
            SetEventId(eventId);            
        }

        public void SetUser(User user)
        {
            User = user;
            if (user != null)
            {                
                SetUserId(user.Id);
            }
        }     

        public void SetRole(Role role)
        {
            Role = role;

            if (role != null)
            {
                RoleId = role.Id;
            }
        }

        public void SetEvent(Event eventValue)
        {
            Event = eventValue;
            if (eventValue != null)
            {
                SetEventId(eventValue.Id);
            }
        }

        public void SetEventId(int eventId)
        {
            EventId = eventId;
        }


        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
