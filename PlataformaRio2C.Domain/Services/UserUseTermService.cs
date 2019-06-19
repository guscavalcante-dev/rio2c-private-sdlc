using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Services
{
    public class UserUseTermService : Service<UserUseTerm>, IUserUseTermService
    {
        public UserUseTermService(IUserUseTermRepository repository)
            :base(repository)
        {

        }

        public IEnumerable<UserUseTerm> GetAllByUserId(int id)
        {
            return _repository.GetAll(e => e.UserId == id);
        }

        public UserUseTerm GetByUserId(int id)
        {
            return _repository.Get(e => e.UserId == id);
        }
    }
}
