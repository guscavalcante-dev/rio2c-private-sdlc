// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="Role.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Statics
{
    /// <summary>Role</summary>
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Role"/> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Role(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Role Admin = new Role(1, "Admin");
        public static Role User = new Role(2, "User");
    }
}