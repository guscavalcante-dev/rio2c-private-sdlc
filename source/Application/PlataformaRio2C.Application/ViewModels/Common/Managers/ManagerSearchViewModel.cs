// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-20-2021
// ***********************************************************************
// <copyright file="ManagerSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>ManagerSearchViewModel</summary>
    public class ManagerSearchViewModel
    {
        [Display(Name = "Search", ResourceType = typeof(Labels))]
        public string Search { get; set; }

        [Display(Name = "ShowAllEditions", ResourceType = typeof(Labels))]
        public bool ShowAllEditions { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(Labels))]
        public IEnumerable<Role> Roles { get; private set; }

        [Display(Name = "CollaboratorTypes", ResourceType = typeof(Labels))]
        public IEnumerable<CollaboratorType> CollaboratorTypes { get; private set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ManagerSearchViewModel"/> class.</summary>
        public ManagerSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateRoles(List<Role> roles, string userInterfaceLanguage)
        {
            roles.ForEach(r => r.Translate(userInterfaceLanguage));
            this.Roles = roles.OrderBy(ct => ct.Description);
        }

        /// <summary>
        /// Updates the collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateCollaboratorTypes(List<CollaboratorType> collaboratorTypes, string userInterfaceLanguage)
        {
            collaboratorTypes.ForEach(ct => ct.Translate(userInterfaceLanguage));
            this.CollaboratorTypes = collaboratorTypes.OrderBy(ct => ct.Description);
        }
    }
}