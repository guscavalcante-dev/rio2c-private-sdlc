using PlataformaRio2C.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        System.Linq.IQueryable<Player> GetAllNoImageWithInterest(Expression<Func<Player, bool>> filter);
        System.Linq.IQueryable<Player> GetAllNoImage(Expression<Func<Player, bool>> filter);

        Player GetImage(Expression<Func<Player, bool>> filter);
        IQueryable<Player> GetImageAll(Expression<Func<Player, bool>> filter);
        IQueryable<Player> GetImageAll();

        Player GetSimple(Expression<Func<Player, bool>> filter);

        IQueryable<Player> GetAllWithHoldingSimple();

        //System.Linq.IQueryable<Collaborator> GetAllCollaborators(Expression<Func<Player, bool>> filter);

        IQueryable<Player> GetAllWithAddress();

        IQueryable<Player> GetAllApi();

        Player GetAllWithInterests(Guid uid);
    }    
}
