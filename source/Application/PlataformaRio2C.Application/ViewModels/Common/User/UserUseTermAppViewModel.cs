using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.ViewModels
{
    public class UserUseTermAppViewModel : EntityViewModel<UserUseTermAppViewModel, UserUseTerm>, IEntityViewModel<UserUseTerm>
    {
        public int UserId { get; set; }
        public int EventId { get; set; }

        public string Role { get; set; }

        public UserUseTermAppViewModel()
        {

        }

        public UserUseTermAppViewModel(UserUseTerm entity)
        {
            Uid = entity.Uid;
            CreationDate = entity.CreationDate;

            if (entity.User != null)
            {
                UserId = entity.User.Id;
            }

            if (entity.Event != null)
            {
                EventId = entity.Event.Id;
            }           

            if (entity.Role != null)
            {
                Role = entity.Role.Name;
            }
        }              

        public UserUseTerm MapReverse()
        {
            var entity = new UserUseTerm(UserId, EventId);

            return entity;
        }

        public UserUseTerm MapReverse(UserUseTerm entity)
        {
            entity.SetUserId(UserId);

            entity.SetEventId(EventId);

            return entity;
        }
    }
}
