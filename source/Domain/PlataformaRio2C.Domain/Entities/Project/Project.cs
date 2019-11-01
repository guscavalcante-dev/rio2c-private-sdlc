// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
// ***********************************************************************
// <copyright file="Project.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Project</summary>
    public class Project : Entity
    {
        public static readonly int SendPlayerCountMax = 5;

        public int ProjectTypeId { get; private set; }
        public int SellerAttendeeOrganizationId { get; private set; }
        public int NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; } //TODO: Fix field name on database
        public string ValuePerEpisode { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public string ValueAlreadyRaised { get; private set; }
        public string ValueStillNeeded { get; private set; }
        public bool? Pitching { get; private set; }

        public virtual ProjectType ProjectType { get; private set; }
        public virtual AttendeeOrganization SellerAttendeeOrganization { get; private set; }

        public virtual ICollection<ProjectTitle> Titles { get; private set; }
        public virtual ICollection<ProjectSummary> Summaries { get; private set; }
        public virtual ICollection<ProjectLogLine> LogLines { get; private set; }
        public virtual ICollection<ProjectProductionPlan> ProductionPlans { get; private set; }
        public virtual ICollection<ProjectAdditionalInformation> AdditionalInformations { get; private set; }
        public virtual ICollection<ProjectImageLink> ImageLinks { get; private set; }
        public virtual ICollection<ProjectTeaserLink> TeaserLinks { get; private set; }
        public virtual ICollection<ProjectInterest> Interests { get; private set; }
        //public virtual ICollection<ProjectPlayer> PlayersRelated { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Project"/> class.</summary>
        protected Project()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            //this.ValidateName();
            //this.ValidateDescriptions();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>Validates the name.</summary>
        //public void ValidateName()
        //{
        //    if (string.IsNullOrEmpty(this.Name?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
        //    }

        //    if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
        //    }
        //}

        ///// <summary>Validates the descriptions.</summary>
        //public void ValidateDescriptions()
        //{
        //    foreach (var description in this.Descriptions?.Where(d => !d.IsValid())?.ToList())
        //    {
        //        this.ValidationResult.Add(description.ValidationResult);
        //    }
        //}

        #endregion

        #region Old methods

        //public Project(IEnumerable<ProjectTitle> titles)
        //{
        //    SetTitles(titles);
        //}

        //public void SetTitles(IEnumerable<ProjectTitle> titles)
        //{
        //    if (titles != null)
        //    {
        //        Titles = titles.ToList();
        //    }
        //    else
        //    {
        //        Titles = null;
        //    }
        //}

        //public void SetLogLines(IEnumerable<ProjectLogLine> entities)
        //{
        //    if (entities != null)
        //    {
        //        LogLines = entities.ToList();
        //    }
        //    else
        //    {
        //        LogLines = null;
        //    }
        //}

        //public void SetSummaries(IEnumerable<ProjectSummary> entities)
        //{
        //    if (entities != null)
        //    {
        //        Summaries = entities.ToList();
        //    }
        //    else
        //    {
        //        Summaries = null;
        //    }
        //}

        //public void SetProductionPlans(IEnumerable<ProjectProductionPlan> entities)
        //{
        //    if (entities != null)
        //    {
        //        ProductionPlans = entities.ToList();
        //    }
        //    else
        //    {
        //        ProductionPlans = null;
        //    }
        //}

        //public void SetInterests(IEnumerable<ProjectInterest> entities)
        //{
        //    if (entities != null)
        //    {
        //        Interests = entities.ToList();
        //    }
        //    else
        //    {
        //        Interests = null;
        //    }
        //}

        //public void SetLinksImage(IEnumerable<ProjectLinkImage> entities)
        //{
        //    if (entities != null)
        //    {
        //        LinksImage = entities.ToList();
        //    }
        //    else
        //    {
        //        LinksImage = null;
        //    }
        //}

        //public void SetLinksTeaser(IEnumerable<ProjectLinkTeaser> entities)
        //{
        //    if (entities != null)
        //    {
        //        LinksTeaser = entities.ToList();
        //    }
        //    else
        //    {
        //        LinksTeaser = null;
        //    }
        //}

        //public void SetAdditionalInformations(IEnumerable<ProjectAdditionalInformation> entities)
        //{
        //    if (entities != null)
        //    {
        //        AdditionalInformations = entities.ToList();
        //    }
        //    else
        //    {
        //        AdditionalInformations = null;
        //    }
        //}

        //public void SetNumberOfEpisodes(int value)
        //{
        //    NumberOfEpisodes = value;
        //}

        //public void SetEachEpisodePlayingTime(string value)
        //{
        //    EachEpisodePlayingTime = value;
        //}

        //public void SetValuePerEpisode(string value)
        //{
        //    ValuePerEpisode = value;
        //}

        //public void SetTotalValueOfProject(string value)
        //{
        //    TotalValueOfProject = value;
        //}

        //public void SetValueAlreadyRaised(string value)
        //{
        //    ValueAlreadyRaised = value;
        //}

        //public void SetValueStillNeeded(string value)
        //{
        //    ValueStillNeeded = value;
        //}

        //public void SetPitching(bool? value)
        //{
        //    Pitching = value;
        //}

        //public void SetProducer(Producer producer)
        //{
        //    Producer = producer;
        //    if (producer != null)
        //    {
        //        ProducerId = producer.Id;
        //    }
        //}

        //public string GetName()
        //{
        //    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

        //    string titlePt = Titles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
        //    string titleEn = Titles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

        //    if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
        //    {
        //        return titlePt;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(titleEn))
        //    {
        //        return titleEn;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion
    }
}
