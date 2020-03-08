// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="Country.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Country</summary>
    public class Country : Entity
    {
        public static readonly int NameMinLength = 3;
        public static readonly int NameMaxLength = 100;
        public static readonly int CodeMinLength = 1;
        public static readonly int CodeMaxLength = 3;

        public string Name { get; private set; }
        public string Code { get; private set; }
        public string CompanyNumberMask { get; set; }
        public string ZipCodeMask { get; private set; }
        public string PhoneNumberMask { get; private set; }
        public string MobileMask { get; private set; }
        public bool IsManual { get; private set; }
        public bool IsCompanyNumberRequired { get; private set; }

        public virtual ICollection<State> States { get; private set; }
        public virtual ICollection<Address> Addresses { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Country"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public Country(string name, string code, bool isManual, int userId)
        {
            this.Name = name?.Trim();
            this.Code = code?.Trim();
            this.IsManual = isManual;
            this.IsCompanyNumberRequired = false;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Country"/> class.</summary>
        protected Country()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, string code, bool isManual, int userId)
        {
            this.Name = name?.Trim();
            this.Code = code?.Trim();
            this.IsManual = isManual;
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.DeleteStates(userId);
            this.DeleteAddresses(userId);

            if (this.FindAllStatesNotDeleted()?.Any() == false && this.FindAllAddressesNotDeleted()?.Any() == false)
            {
                this.IsDeleted = true;
            }
        }

        #region States

        /// <summary>Finds the state.</summary>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public State FindState(
            Guid? stateUid,
            string stateName,
            bool isManual,
            int userId)
        {
            return this.FindOrCreateState(stateUid, stateName, isManual, userId);
        }

        /// <summary>Deletes the states.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteStates(int userId)
        {
            foreach (var state in this.FindAllStatesNotDeleted())
            {
                state?.Delete(userId);
            }
        }

        /// <summary>Finds all states not deleted.</summary>
        /// <returns></returns>
        private List<State> FindAllStatesNotDeleted()
        {
            return this.States?.Where(s => !s.IsDeleted)?.ToList();
        }

        /// <summary>Finds the state not deleted by uid.</summary>
        /// <param name="stateUid">The state uid.</param>
        /// <returns></returns>
        private State FindStateNotDeletedByUid(Guid stateUid)
        {
            return this.States?.FirstOrDefault(s => s.Uid == stateUid && !s.IsDeleted);
        }

        /// <summary>Finds the name of the state not deleted by.</summary>
        /// <param name="stateName">Name of the state.</param>
        /// <returns></returns>
        private State FindStateNotDeletedByName(string stateName)
        {
            return this.States?.FirstOrDefault(s => s.Name.Trim().ToLowerInvariant() == stateName?.Trim()?.ToLowerInvariant() && !s.IsDeleted);
        }

        /// <summary>Finds the state of the or create.</summary>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private State FindOrCreateState(Guid? stateUid, string stateName, bool isManual, int userId)
        {
            if (this.States == null)
            {
                this.States = new List<State>();
            }

            State state = null;
            if (stateUid.HasValue)
            {
                state = this.FindStateNotDeletedByUid(stateUid.Value);
            }
            else if (!string.IsNullOrEmpty(stateName?.Trim()))
            {
                state = this.FindStateNotDeletedByName(stateName) ?? 
                        new State(this, stateName, null, isManual, userId);
            }

            if (state == null)
            {
                throw new DomainException(string.Format(Messages.CouldNotCreate, Labels.TheM.ToLowerInvariant(), Labels.State.ToLowerInvariant()));
            }

            return state;
        }

        #endregion

        #region Cities

        /// <summary>Finds the city.</summary>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="isManual">if set to <c>true</c> [is manual].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public City FindCity(
            Guid? stateUid, 
            string stateName, 
            Guid? cityUid, 
            string cityName, 
            bool isManual,
            int userId)
        {
            var state = this.FindOrCreateState(stateUid, stateName, isManual, userId);
            return state?.FindCity(cityUid, cityName, isManual, userId);
        }

        #endregion

        #region Addresses

        /// <summary>Deletes the addresses.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAddresses(int userId)
        {
            foreach (var address in this.FindAllAddressesNotDeleted())
            {
                address?.Delete(userId);
            }
        }

        /// <summary>Finds all addresses not deleted.</summary>
        /// <returns></returns>
        private List<Address> FindAllAddressesNotDeleted()
        {
            return this.Addresses?.Where(n => !n.IsDeleted)?.ToList();
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateCode();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the code.</summary>
        public void ValidateCode()
        {
            if (string.IsNullOrEmpty(this.Code?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Code), new string[] { "Code" }));
            }

            if (this.Code?.Trim().Length < CodeMinLength || this.Code?.Trim().Length > CodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Code, CodeMaxLength, CodeMinLength), new string[] { "Code" }));
            }
        }

        #endregion
    }
}