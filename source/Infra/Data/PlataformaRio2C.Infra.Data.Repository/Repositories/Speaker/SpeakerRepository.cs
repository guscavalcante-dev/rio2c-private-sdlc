//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Infra.Data.Repository
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="SpeakerRepository.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    /// <summary>SpeakerRepository</summary>
//    public class SpeakerRepository : Repository<PlataformaRio2CContext, Speaker>, ISpeakerRepository
//    {
//        private PlataformaRio2CContext _context;

//        /// <summary>Initializes a new instance of the <see cref="SpeakerRepository"/> class.</summary>
//        /// <param name="context">The context.</param>
//        public SpeakerRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//            _context = context;
//        }

//        /// <summary>Método que adiciona a entidade ao Contexto</summary>
//        /// <param name="entity">Entidade</param>
//        public override void Create(Speaker entity)
//        {
//            base.Create(entity);
//        }
//    }
//}