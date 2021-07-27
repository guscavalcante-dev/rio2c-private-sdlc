// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-28-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-28-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationBaseCommandHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationBaseCommandHandler.
    /// Implements the <see cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Application.CQRS.CommandsHandlers.BaseCommandHandler" />
    public class AttendeeInnovationOrganizationBaseCommandHandler : BaseCommandHandler
    {
        protected readonly IAttendeeInnovationOrganizationRepository AttendeeInnovationOrganizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationBaseCommandHandler"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="AttendeeInnovationOrganizationRepo">The innovation organization repo.</param>
        public AttendeeInnovationOrganizationBaseCommandHandler(
            IMediator commandBus, 
            IUnitOfWork uow,
            IAttendeeInnovationOrganizationRepository AttendeeInnovationOrganizationRepository)
            : base(commandBus, uow)
        {
            this.AttendeeInnovationOrganizationRepo = AttendeeInnovationOrganizationRepository;
        }

        /// <summary>Gets the music project by uid.</summary>
        /// <param name="attendeeInnovationOrganizationUid">The project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganization> GetAttendeeInnovationOrganizationByUid(Guid attendeeInnovationOrganizationUid)
        {
            var attendeeInnovationOrganization = await this.AttendeeInnovationOrganizationRepo.GetAsync(attendeeInnovationOrganizationUid);
            if (attendeeInnovationOrganization == null || attendeeInnovationOrganization.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Startup, Labels.FoundM), new string[] { "ToastrError" }));
                return null;
            }

            return attendeeInnovationOrganization;
        }
    }
}