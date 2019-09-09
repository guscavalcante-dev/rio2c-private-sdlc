// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="InterestGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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

        /// <summary>Initializes a new instance of the <see cref="InterestGroup"/> class.</summary>
        protected InterestGroup()
        {
        }

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
    }
}
