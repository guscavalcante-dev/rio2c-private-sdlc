// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
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

        /// <summary>Initializes a new instance of the <see cref="Activity"/> class.</summary>
        protected Activity()
        {
        }

        public Activity(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}