// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorItemListAppViewModel.cs" company="Softo">
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
    /// <summary>CollaboratorItemListAppViewModel</summary>
    public class CollaboratorItemListAppViewModel: EntityViewModel<CollaboratorItemListAppViewModel, Collaborator>
    {
        [Display(Name = "FullName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        public string CellPhone { get; set; }

        [Display(Name = "Player", ResourceType = typeof(Labels))]        
        public string PlayersName { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        public string HoldingsName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        public int? SpeakerId { get; set; }

        public int? MusicalCommissionId { get; set; }

        public CollaboratorItemListAppViewModel()
        {

        }

        public CollaboratorItemListAppViewModel(Collaborator entity)
            :base(entity)
        {
            CreationDate = entity.CreateDate;
            Uid = entity.Uid;
            Name = entity.FirstName;
            PhoneNumber = entity.PhoneNumber;
            CellPhone = entity.CellPhone;

            if (entity.User != null)
            {
                Email = entity.User.Email;
            }

            //if (entity.Players != null && entity.Players.Any())
            //{
            //    PlayersName = string.Join(", ", entity.Players.Select(e => e.Name));
            //}

            //if (entity.Players != null && entity.Players.Any())
            //{
            //    HoldingsName = string.Join(", ", entity.Players.Select(e => e.Holding.Name).Distinct());
            //}

            //if(entity.SpeakerId != null)
            //{
            //    SpeakerId = entity.SpeakerId;
            //}

            //if (entity.MusicalCommissionId != null)
            //{
            //    MusicalCommissionId = entity.MusicalCommissionId;
            //}
        }


        
    }
}
