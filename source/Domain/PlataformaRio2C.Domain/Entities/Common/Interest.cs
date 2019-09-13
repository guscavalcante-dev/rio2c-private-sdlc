// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="Interest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Interest</summary>
    public class Interest : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 150;

        public int InterestGroupId { get; private set; }
        public virtual Guid InterestGroupUid { get; private set; }
        public int DisplayOrder { get; private set; }

        public virtual InterestGroup InterestGroup { get; private set; }

        public string Name { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Interest"/> class.</summary>
        protected Interest()
        {
        }
        
        public Interest(string name, InterestGroup interestGroup)
        {
            SetName(name);
            SetInterestGroup(interestGroup);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetInterestGroup(InterestGroup interestGroup)
        {
            InterestGroup = interestGroup;
            if (interestGroup != null)
            {
                InterestGroupId = interestGroup.Id;
                InterestGroupUid = interestGroup.Uid;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
