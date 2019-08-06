// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserUseTerm.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>UserUseTerm</summary>
    public class UserUseTerm : Entity
    {
        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int EventId { get; private set; }
        public virtual Edition Edition { get; private set; }
      
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

        public void SetEvent(Edition eventValue)
        {
            Edition = eventValue;
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
