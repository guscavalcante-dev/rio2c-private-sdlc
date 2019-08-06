// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorProducerItemListAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CollaboratorProducerItemListAppViewModel</summary>
    public class CollaboratorProducerItemListAppViewModel : EntityViewModel<CollaboratorProducerItemListAppViewModel, Collaborator>
    {
        [Display(Name = "FullName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        public bool HasAcceptTerm { get; set; }

        public string ProducerName { get; set; }

        public CollaboratorProducerItemListAppViewModel()
        {

        }

        public CollaboratorProducerItemListAppViewModel(Collaborator entity)
            : base(entity)
        {
            Name = entity.Name;

            if (entity.User != null)
            {
                Email = entity.User.Email;

                HasAcceptTerm = entity.User.UserUseTerms.Any();
            }            

            if (entity.ProducersEvents.Any(e => e.Edition.Name.Contains("2018")))
            {
                ProducerName = entity.ProducersEvents.Where(e => e.Edition.Name.Contains("2018")).Select(e => e.Producer.Name).FirstOrDefault();
            }
        }
    }
}
