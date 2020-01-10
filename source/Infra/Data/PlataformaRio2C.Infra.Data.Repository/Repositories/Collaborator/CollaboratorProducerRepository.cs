//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Infra.Data.Repository
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="CollaboratorProducerRepository.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using System.Data.Entity;
//using System.Linq;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    /// <summary>CollaboratorProducerRepository</summary>
//    public class CollaboratorProducerRepository : Repository<PlataformaRio2CContext, CollaboratorProducer>, ICollaboratorProducerRepository
//    {
//        /// <summary>Initializes a new instance of the <see cref="CollaboratorProducerRepository"/> class.</summary>
//        /// <param name="context">The context.</param>
//        public CollaboratorProducerRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//        }

//        /// <summary>Método que traz todos os registros</summary>
//        /// <param name="readonly"></param>
//        /// <returns></returns>
//        public override IQueryable<CollaboratorProducer> GetAll(bool @readonly = false)
//        {
//            var consult = this.dbSet
//                                 .Include(i => i.Edition)
//                                .Include(i => i.Producer)
//                                .Include(i => i.Collaborator)
//                                //.Include(i => i.Collaborator.ProducersEvents)
//                                //.Include(i => i.Collaborator.ProducersEvents.Select(p => p.Edition))
//                                //.Include(i => i.Collaborator.ProducersEvents.Select(p => p.Producer))
//                                .Include(i => i.Collaborator.User)
//                                .Include(i => i.Collaborator.User.UserUseTerms);


//            return @readonly
//              ? consult.AsNoTracking()
//              : consult;
//        }

//        /// <summary>Gets all collaborators.</summary>
//        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
//        /// <returns></returns>
//        public IQueryable<Collaborator> GetAllCollaborators(bool @readonly = false)
//        {
//            var consult = this.dbSet
//                                .Include(i => i.Edition)
//                                .Include(i => i.Producer)
//                                .Include(i => i.Collaborator)
//                                //.Include(i => i.Collaborator.ProducersEvents)
//                                //.Include(i => i.Collaborator.ProducersEvents.Select(p => p.Edition))
//                                //.Include(i => i.Collaborator.ProducersEvents.Select(p => p.Producer))
//                                .Include(i => i.Collaborator.User)
//                                .Include(i => i.Collaborator.User.UserUseTerms)
//                                .Select(e => e.Collaborator);


//            return @readonly
//              ? consult.AsNoTracking()
//              : consult;
//        }
//    }
//}