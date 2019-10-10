// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="UpdateOrganizationAddress.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationAddress</summary>
    public class UpdateOrganizationAddress : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CountryUid { get; set; }

        public AddressBaseCommand Address { get; set; }

        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        //public UserBaseDto UpdaterBaseDto { get; set; }
        //public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationAddress"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        public UpdateOrganizationAddress(
            AttendeeOrganizationSiteAddressWidgetDto entity,
            List<CountryBaseDto> countriesBaseDtos,
            bool isAddressRequired)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.CountryUid = entity.Country?.Uid;
            this.UpdateAddress(entity, isAddressRequired);
            this.UpdateModelsAndLists(countriesBaseDtos);
            //this.UpdaterBaseDto = entity.UpdaterDto;
            //this.UpdateDate = entity.UpdateDate;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationAddress"/> class.</summary>
        public UpdateOrganizationAddress()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateModelsAndLists(List<CountryBaseDto> countriesBaseDtos)
        {
            this.CountriesBaseDtos = countriesBaseDtos;
        }

        #region Private Methods

        /// <summary>Updates the address.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        private void UpdateAddress(AttendeeOrganizationSiteAddressWidgetDto entity, bool isAddressRequired)
        {
            this.Address = new AddressBaseCommand(entity?.GetAddressBaseDto(), isAddressRequired);
        }

        #endregion
    }
}