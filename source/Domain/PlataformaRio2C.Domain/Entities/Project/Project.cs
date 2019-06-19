using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;
using System.Globalization;

namespace PlataformaRio2C.Domain.Entities
{
    public class Project : Entity
    {
        public static readonly int SendPlayerCountMax = 5;

        public int ProducerId { get; private set; }
        public virtual Producer Producer { get; private set; }
        public virtual ICollection<ProjectTitle> Titles { get; private set; }
        public virtual ICollection<ProjectLogLine> LogLines { get; private set; }
        public virtual ICollection<ProjectSummary> Summaries { get; private set; }
        public virtual ICollection<ProjectProductionPlan> ProductionPlans { get; private set; }
        public virtual ICollection<ProjectInterest> Interests { get; private set; }

        public bool Any()
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<ProjectLinkImage> LinksImage { get; private set; }
        public virtual ICollection<ProjectLinkTeaser> LinksTeaser { get; private set; }
        public virtual ICollection<ProjectAdditionalInformation> AdditionalInformations { get; private set; }
        public virtual ICollection<ProjectPlayer> PlayersRelated { get; private set; }
        public int NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string ValuePerEpisode { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public string ValueAlreadyRaised { get; private set; }
        public string ValueStillNeeded { get; private set; }
        public bool? Pitching { get; private set; }

       


        protected Project()
        {

        }

        public Project(IEnumerable<ProjectTitle> titles)
        {
            SetTitles(titles);
        }

        public void SetTitles(IEnumerable<ProjectTitle> titles)
        {
            if (titles != null)
            {
                Titles = titles.ToList();
            }
            else
            {
                Titles = null;
            }
        }

        public void SetLogLines(IEnumerable<ProjectLogLine> entities)
        {
            if (entities != null)
            {
                LogLines = entities.ToList();
            }
            else
            {
                LogLines = null;
            }
        }

        public void SetSummaries(IEnumerable<ProjectSummary> entities)
        {
            if (entities != null)
            {
                Summaries = entities.ToList();
            }
            else
            {
                Summaries = null;
            }
        }

        public void SetProductionPlans(IEnumerable<ProjectProductionPlan> entities)
        {
            if (entities != null)
            {
                ProductionPlans = entities.ToList();
            }
            else
            {
                ProductionPlans = null;
            }
        }

        public void SetInterests(IEnumerable<ProjectInterest> entities)
        {
            if (entities != null)
            {
                Interests = entities.ToList();
            }
            else
            {
                Interests = null;
            }
        }

        public void SetLinksImage(IEnumerable<ProjectLinkImage> entities)
        {
            if (entities != null)
            {
                LinksImage = entities.ToList();
            }
            else
            {
                LinksImage = null;
            }
        }

        public void SetLinksTeaser(IEnumerable<ProjectLinkTeaser> entities)
        {
            if (entities != null)
            {
                LinksTeaser = entities.ToList();
            }
            else
            {
                LinksTeaser = null;
            }
        }

        public void SetAdditionalInformations(IEnumerable<ProjectAdditionalInformation> entities)
        {
            if (entities != null)
            {
                AdditionalInformations = entities.ToList();
            }
            else
            {
                AdditionalInformations = null;
            }
        }

        public void SetNumberOfEpisodes(int value)
        {
            NumberOfEpisodes = value;
        }

        public void SetEachEpisodePlayingTime(string value)
        {
            EachEpisodePlayingTime = value;
        }

        public void SetValuePerEpisode(string value)
        {
            ValuePerEpisode = value;
        }

        public void SetTotalValueOfProject(string value)
        {
            TotalValueOfProject = value;
        }

        public void SetValueAlreadyRaised(string value)
        {
            ValueAlreadyRaised = value;
        }

        public void SetValueStillNeeded(string value)
        {
            ValueStillNeeded = value;
        }

        public void SetPitching(bool? value)
        {
            Pitching = value;
        }

        public void SetProducer(Producer producer)
        {
            Producer = producer;
            if (producer != null)
            {
                ProducerId = producer.Id;
            }
        }


        public string GetName()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string titlePt = Titles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            string titleEn = Titles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

            if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
            {
                return titlePt;
            }
            else if (!string.IsNullOrWhiteSpace(titleEn))
            {
                return titleEn;
            }
            else
            {
                return null;
            }
        }


        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new ProjectIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
