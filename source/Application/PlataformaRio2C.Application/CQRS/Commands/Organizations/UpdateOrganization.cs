// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="UpdateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganization</summary>
    public class UpdateOrganization : OrganizationBaseCommand
    {
        public Guid OrganizationUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganization"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        public UpdateOrganization(OrganizationDto entity, List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos, List<CountryBaseDto> countriesBaseDtos, bool? isAddingToCurrentEdition)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM));
            }

            this.OrganizationUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateBaseProperties(entity, holdingBaseDtos, languagesDtos, countriesBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganization"/> class.</summary>
        public UpdateOrganization()
        {
        }
    }
}