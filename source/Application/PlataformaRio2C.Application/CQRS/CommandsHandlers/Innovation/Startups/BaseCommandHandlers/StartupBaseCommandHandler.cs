// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
// ***********************************************************************
// <copyright file="StartupBaseCommandHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>Class StartupBaseCommandHandler.
    /// Implements the <see cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler"/></summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    public class StartupBaseCommandHandler : BaseCommandHandler
    {
        //protected readonly IMusicBandRepository MusicBandRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="musicBandRepository">The music band repository.</param>
        public StartupBaseCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow,
            IMusicBandRepository musicBandRepository
            )
            : base(commandBus, uow)
        {
            //this.MusicBandRepo = musicBandRepository;
        }
    }
}