// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-10-2019
// ***********************************************************************
// <copyright file="InterestGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>InterestGroup</summary>
    public class InterestGroup : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 150;
        public static readonly int TypeMaxLength = 100;

        public string Name { get; private set; }
        public string Type { get; private set; }
        public int DisplayOrder { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="InterestGroup"/> class.</summary>
        protected InterestGroup()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InterestGroup"/> class.</summary>
        /// <param name="uid">The uid.</param>
        private InterestGroup(Guid uid)
        {
            this.Uid = uid;
        }

        #region Statics

        public static InterestGroup Genre = new InterestGroup(new Guid("7B4A7C4A-EF10-483C-8854-87EBEB883583"));

        #endregion

        #region Old methods

        public InterestGroup(string name, InterestGroupTypeCodes value)
        {
            SetName(name);
            SetType(value);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(InterestGroupTypeCodes value)
        {
            Type = value.ToString();
        }

        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}
