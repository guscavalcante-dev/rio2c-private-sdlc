// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorBasicAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>CollaboratorBasicAppViewModel</summary>
    public class CollaboratorBasicAppViewModel : EntityViewModel<CollaboratorBasicAppViewModel, Collaborator>, IEntityViewModel<Collaborator>
    {
        public override Guid Uid { get; set; }
        public Guid UidByEdit { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "BadgeName", ResourceType = typeof(Labels))]
        public string Badge { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        public string CellPhone { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel Image { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Labels))]
        public ImageFileAppViewModel NewImage { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Labels))]
        public HttpPostedFileBase ImageUpload { get; set; }

        public UserAppViewModel User { get; set; }

        public AddressAppViewModel Address { get; set; }

        [Display(Name = "JobTitle", ResourceType = typeof(Labels))]
        public IEnumerable<CollaboratorJobTitleAppViewModel> JobTitles { get; set; }

        public string JobTitle { get; set; }

        [Display(Name = "MiniBio", ResourceType = typeof(Labels))]
        public IEnumerable<CollaboratorMiniBioAppViewModel> MiniBios { get; set; }
        public string MiniBio { get; set; }

        public bool RegisterComplete { get; set; }

        public CollaboratorBasicAppViewModel()
        {
            User = new UserAppViewModel();
            Address = new AddressAppViewModel();
            JobTitles = new List<CollaboratorJobTitleAppViewModel>();
            MiniBios = new List<CollaboratorMiniBioAppViewModel>();            
        }

        public CollaboratorBasicAppViewModel(Domain.Entities.Collaborator entity)
        {
            UidByEdit = entity.Uid;
            Uid = entity.Uid;
            CreationDate = entity.CreateDate;
            Name = entity.Name;

            PhoneNumber = entity.PhoneNumber;
            CellPhone = entity.CellPhone;
            Badge = entity.Badge;

            if (entity.User != null)
            {
                User = new UserAppViewModel(entity.User);
            }

            if (entity.Address != null)
            {
                Address = new AddressAppViewModel(entity.Address);
                RegisterComplete = Address.ZipCode != null && !string.IsNullOrWhiteSpace(Address.ZipCode);
            }
            else
            {
                Address = new AddressAppViewModel();
            }

            if (entity.JobTitles != null && entity.JobTitles.Any())
            {
                JobTitles = CollaboratorJobTitleAppViewModel.MapList(entity.JobTitles).ToList();
                JobTitle = entity.GetJobTitle();
            }

            if (entity.MiniBios != null && entity.MiniBios.Any())
            {
                MiniBios = CollaboratorMiniBioAppViewModel.MapList(entity.MiniBios).ToList();
                MiniBio = entity.GetMiniBio();
            }

            //if (entity.Image != null)
            //{
            //    Image = new ImageFileAppViewModel(entity.Image);
            //}
        }        

        public virtual Collaborator MapReverse()
        {
            User user = null;
            var name = Name != null ? Name.Trim() : null;

            if (User != null)
            {
                User.Name = name;
                User.PhoneNumber = CellPhone;
                user = User.MapReverse();
            }

            var entity = new Domain.Entities.Collaborator(name, null, user);
            entity.SetPhoneNumber(PhoneNumber);
            entity.SetCellPhone(CellPhone);
            entity.SetBadge(Badge);

            if (ImageUpload != null && Image == null)
            {
                Image = new ImageFileAppViewModel(ImageUpload);
            }

            if (NewImage != null && Image != null)
            {
                Image.File = NewImage.File;
                Image.ContentLength = NewImage.File.Length;
            }

            if (Image != null)
            {
                entity.SetImage(Image.MapReverse());
            }

            if (Address != null)
            {
                entity.SetAddress(this.Address.MapReverse());
            }

            return entity;
        }
        public virtual Collaborator MapReverse(Collaborator entity)
        {
            var name = Name != null ? Name.Trim() : null;

            entity.SetName(name);

            if (Address != null)
            {
                if (entity.Address != null)
                {
                    entity.SetAddress(this.Address.MapReverse(entity.Address));
                }
                else
                {
                    entity.SetAddress(this.Address.MapReverse());
                }
            }

            User user = null;

            if (User != null)
            {
                User.Name = name;
                User.PhoneNumber = CellPhone;
                user = User.MapReverse(entity.User);
            }
            

            entity.SetPhoneNumber(PhoneNumber);
            entity.SetCellPhone(CellPhone);
            entity.SetBadge(Badge);

            if (ImageUpload != null && Image == null)
            {
                Image = new ImageFileAppViewModel(ImageUpload);
            }

            if (NewImage != null && Image != null)
            {
                Image.File = NewImage.File;
                Image.ContentLength = NewImage.File.Length;
            }

            if (Image != null)
            {
                if (entity.Image != null)
                {
                    entity.SetImage(Image.MapReverse(entity.Image));
                }
                else
                {
                    entity.SetImage(Image.MapReverse());
                }
            }
            else
            {
                entity.SetImage(null);
            }

            return entity;
        }
    }
}
