// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>UserAppViewModel</summary>
    public class UserAppViewModel : EntityViewModel<UserAppViewModel, User>, IEntityViewModel<User>
    {
        
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }
        public bool Active { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public virtual string Email { get; set; }

        //[Display(Name = "EmailConfirmed", ResourceType = typeof(Labels))]
        //public virtual bool EmailConfirmed { get; set; }

        //[Display(Name = "Password", ResourceType = typeof(Labels))]
        //public virtual string PasswordHash { get; set; }
        //public virtual string SecurityStamp { get; set; }
        //[Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        public virtual string PhoneNumber { get; set; }
        //[Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(Labels))]
        //public virtual bool PhoneNumberConfirmed { get; set; }
        //public virtual bool TwoFactorEnabled { get; set; }
        //public virtual DateTime? LockoutEndDateUtc { get; set; }
        //public virtual bool LockoutEnabled { get; set; }
        //public virtual int AccessFailedCount { get; set; }
        [Display(Name = "User", ResourceType = typeof(Labels))]
        public virtual string UserName { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinimumNumberOfCharacters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public virtual string Password { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinimumNumberOfCharacters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public virtual string PasswordNew { get; set; }

        [Compare("PasswordNew", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordConfirmationDoesNotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Labels))]
        public virtual string ConfirmPassword { get; set; }

        public UserAppViewModel()
        {

        }

        public UserAppViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public UserAppViewModel(User entity)
        {
            Uid = entity.Uid;
            CreationDate = entity.CreateDate;
            Name = entity.Name;
            UserName = entity.UserName;
            Email = entity.Email;            
        }

        public User MapReverse()
        {
            var email = Email != null ? Email.Trim() : Email;
            var name = Name != null ? Name.Trim() : null;

            var entity = new User(email);
            entity.SetName(name);            

            return entity;
        }

        public User MapReverse(User entity)
        {
            var email = Email != null ? Email.Trim() : Email;
            var name = Name != null ? Name.Trim() : null;

            if (email != null)
            {
                entity.SetEmail(email);
            }            

            entity.SetName(name);            
            return entity;
        }
    }
}
