// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorOptionMessageAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CollaboratorOptionMessageAppViewModel</summary>
    public class CollaboratorOptionMessageAppViewModel : EntityViewModel<CollaboratorOptionMessageAppViewModel, Collaborator>
    {
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public Guid ProducerUid { get; set; }
        public bool IsProducer { get; set; }
        public string PlayersName { get; set; }
        public Guid[] PlayersUids { get; set; }
        public bool IsPlayer { get; set; }
        public string Email { get; set; }
        public bool HasImage { get; set; }
        public string JobTitle { get; set; }


        public CollaboratorOptionMessageAppViewModel()
            : base()
        {

        }

        public CollaboratorOptionMessageAppViewModel(Collaborator entity)
            : base(entity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            Name = entity.Name;
            Email = entity.User.Email;
            HasImage = entity.ImageId > 0;

            IsProducer = entity.ProducersEvents != null && entity.ProducersEvents.Any(e => e.Producer != null && e.Edition.Name.Contains("2018"));

            if (IsProducer)
            {
                var producer = entity.ProducersEvents.Where(e => e.Producer != null && e.Edition.Name.Contains("2018")).Select(e => e.Producer).FirstOrDefault();

                if (producer != null)
                {
                    ProducerName = producer.Name;
                    ProducerUid = producer.Uid;
                }
            }

            IsPlayer = entity.Players != null && entity.Players.Any();
            if (IsPlayer)
            {
                PlayersName = string.Join(", ", entity.Players.Select(e => e.Name));
                PlayersUids = entity.Players.Select(e => e.Uid).ToArray();
            }

            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                JobTitle = string.Join(", ", entity.JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value));
            }
            else
            {
                JobTitle = string.Join(", ", entity.JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value));
            }
        }
    }
}
