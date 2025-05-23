﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="ProjectType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>ProjectType</summary>
    public class ProjectType : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 50;

        #region Configurations

        public static ProjectType AudiovisualBusinessRound = new ProjectType(1, new Guid("3CE14508-8F6F-4D9D-B5F2-C7B53BA031E0"), Labels.AudiovisualBusinessRound);
        public static ProjectType Startup = new ProjectType(2, new Guid("736A7169-EF69-4AFB-BD43-D7C3BDA8BD74"), Labels.Startup);
        public static ProjectType Music = new ProjectType(3, new Guid("EA460BF8-B7B5-4BAD-AC3F-242F3B6BFA0E"), Labels.Music);
        public static ProjectType AudiovisualPitching = new ProjectType(5, new Guid("E5B7CBA4-10B5-4FCF-A778-09B40AEB6C01"), Labels.AudiovisualPitching);

        #endregion

        public string Name { get; private set; }

        public virtual ICollection<OrganizationType> OrganizationTypes { get; private set; }
        public virtual ICollection<InterestGroup> InterestGroups { get; private set; }
        public virtual ICollection<Activity> Activities { get; private set; }

        //public virtual Address Address { get; private set; }
        //public virtual ICollection<PlayerDescription> Descriptions { get; private set; }
        //public virtual ICollection<PlayerInterest> Interests { get; private set; }
        //public virtual ICollection<Collaborator> Collaborators { get; private set; }
        //public virtual ICollection<Collaborator> CollaboratorsOld { get; private set; }
        //public virtual ICollection<PlayerActivity> PlayerActivitys { get; private set; }
        //public virtual ICollection<PlayerTargetAudience> PlayerTargetAudience { get; private set; }
        //public virtual ICollection<PlayerRestrictionsSpecifics> RestrictionsSpecifics { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectType"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="name">The name.</param>
        public ProjectType(int id, Guid uid, string name)
        {
            this.Id = id;
            this.Uid = uid;
            this.Name = name;
        }

        /// <summary>Initializes a new instance of the <see cref="ProjectType"/> class.</summary>
        protected ProjectType()
        {
        }

        ///// <summary>Sets the name.</summary>
        ///// <param name="name">The name.</param>
        //public void SetName(string name)
        //{
        //    this.Name = name;
        //}

        ///// <summary>Sets the holding.</summary>
        ///// <param name="holding">The holding.</param>
        //public void SetHolding(Holding holding)
        //{
        //    this.Holding = holding;
        //    this.HoldingId = holding.Id;
        //}

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
            //ValidationResult = new ValidationResult();

            //ValidationResult.Add(new PlayerIsConsistent().Valid(this));

            //if (Image != null)
            //{
            //    ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
            //    ValidationResult.Add(new PlayerImageIsConsistent().Valid(this));
            //}

            //return ValidationResult.IsValid;
        }
    }
}
