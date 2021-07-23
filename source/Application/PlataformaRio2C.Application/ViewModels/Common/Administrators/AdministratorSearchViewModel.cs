// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-20-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2021
// ***********************************************************************
// <copyright file="AdministratorSearchViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>
    /// AdministratorSearchViewModel
    /// </summary>
    public class AdministratorSearchViewModel
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministratorSearchViewModel"/> class.
        /// </summary>
        public AdministratorSearchViewModel()
        {
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateDropdownProperties(
            List<Role> roles,
            List<CollaboratorType> collaboratorTypes,
            string userInterfaceLanguage)
        {
            this.UpdateRoles(roles, userInterfaceLanguage);
            this.UpdateCollaboratorTypes(collaboratorTypes, userInterfaceLanguage);
        }

        #region Private Methods

        /// <summary>
        /// Updates the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        private void UpdateRoles(List<Role> roles, string userInterfaceLanguage)
        {
            this.Roles = roles?
                            .GetSeparatorTranslation(r => r.Description, userInterfaceLanguage, '|')?
                            .OrderBy(r => r.Description);
        }

        /// <summary>
        /// Updates the collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        private void UpdateCollaboratorTypes(List<CollaboratorType> collaboratorTypes, string userInterfaceLanguage)
        {
            this.CollaboratorTypes = collaboratorTypes?
                            .GetSeparatorTranslation(ct => ct.Description, userInterfaceLanguage, '|')?
                            .OrderBy(ct => ct.Description);
        }

        #endregion
    }
}