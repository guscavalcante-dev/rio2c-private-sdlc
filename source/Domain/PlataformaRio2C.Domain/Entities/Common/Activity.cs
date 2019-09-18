// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="Activity.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Activity</summary>
    public class Activity : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool HasAdditionalInfo { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Activity"/> class.</summary>
        /// <param name="name">The name.</param>
        public Activity(string name)
        {
            SetName(name);
        }

        /// <summary>Initializes a new instance of the <see cref="Activity"/> class.</summary>
        protected Activity()
        {
        }

        /// <summary>Sets the name.</summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}