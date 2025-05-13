// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-05-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UpdateUserEmailSettings.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateUserEmailSettings</summary>
    public class UpdateUserEmailSettings : BaseCommand
    {
        [Display(Name = "SubscribeList", ResourceType = typeof(Labels))]
        public List<Guid> SelectedSubscribeListUids { get; set; }

        public List<SubscribeList> SubscribeLists { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateUserEmailSettings"/> class.</summary>
        /// <param name="userEmailSettingsDto">The user email settings dto.</param>
        /// <param name="subscribeLists">The subscribe lists.</param>
        public UpdateUserEmailSettings(
            UserEmailSettingsDto userEmailSettingsDto,
            List<SubscribeList> subscribeLists)
        {
            this.UpdateSelectedSubscribeListUids(userEmailSettingsDto, subscribeLists);
            this.SubscribeLists = subscribeLists;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateUserEmailSettings"/> class.</summary>
        public UpdateUserEmailSettings()
        {
        }

        /// <summary>Updates the selected subscribe list uids.</summary>
        /// <param name="userEmailSettingsDto">The user email settings dto.</param>
        /// <param name="subscribeLists">The subscribe lists.</param>
        private void UpdateSelectedSubscribeListUids(
            UserEmailSettingsDto userEmailSettingsDto,
            List<SubscribeList> subscribeLists)
        {
            this.SelectedSubscribeListUids = subscribeLists?
                                                .Where(sl => userEmailSettingsDto?.UserUnsubscribedListDtos?.All(uuld => uuld.SubscribeList.Id != sl.Id) == true)?
                                                .Select(sl => sl.Uid)?.ToList();
        }
    }
}