using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Collaborator : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;


        public string Name { get; private set; }
        public string Badge { get; private set; }

        public string PhoneNumber { get; private set; }

        public string CellPhone { get; private set; }

        public int? AddressId { get; private set; }
        public virtual Address Address { get; private set; }

        public int? PlayerId { get; private set; }
        public virtual Guid PlayerUid { get; private set; }
        public virtual Player Player { get; private set; }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }

        public int? ImageId { get; private set; }
        public virtual ImageFile Image { get; private set; }

        public virtual ICollection<CollaboratorJobTitle> JobTitles { get; private set; }

        public virtual ICollection<CollaboratorMiniBio> MiniBios { get; private set; }

        public virtual ICollection<Player> Players { get; private set; }

        public virtual ICollection<CollaboratorProducer> ProducersEvents { get; private set; }

        public int? SpeakerId { get;  set; }
        //public int? MusicalCommissionId { get;  set; }
        //public virtual ICollection<Speaker> Speaker { get;  set; }

        protected Collaborator()
        {

        }

        public Collaborator(string name, User user)
        {
            SetName(name);
            SetUser(user);
        }

        public Collaborator(string name, Player player, User user)
        {
            SetName(name);
            SetPlayer(player);
            SetUser(user);
        }
        
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetBadge(string badge)
        {
            Badge = badge;
        }

        public void SetPhoneNumber(string value)
        {
            PhoneNumber = value;
        }

        public void SetCellPhone(string value)
        {
            CellPhone = value;
        }

        public void SetPlayer(Player player)
        {
            Player = player;
            if (player != null)
            {
                PlayerId = player.Id;
                PlayerUid = player.Uid;
            }
        }

        public void SetImage(ImageFile image)
        {
            ImageId = null;
            Image = image;
            if (image != null)
            {
                ImageId = image.Id;
            }
        }

        public void SetPlayerUid(Guid value)
        {
            PlayerUid = value;
        }

        public void SetUser(User user)
        {
            User = user;
            if (user != null)
            {
                UserId = user.Id;
            }
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }

        public void SetJobTitles(IEnumerable<CollaboratorJobTitle> jobTitles)
        {
            JobTitles = jobTitles.ToList();
        }

        public void SetMiniBios(IEnumerable<CollaboratorMiniBio> miniBios)
        {
            MiniBios = miniBios.ToList();
        }

        public void SetPlayers(IEnumerable<Player> players)
        {
            if (Players != null)
            {
                Players.Clear();
            }

            if (players != null && players.Any())
            {
                Players = players.ToList();
            }
            else
            {
                Players = null;
            }
        }

        public void AddEventsProducers(CollaboratorProducer collaboratorProducer)
        {
            if (ProducersEvents == null)
            {
                ProducersEvents = new List<CollaboratorProducer> { collaboratorProducer };
            }
            else
            {
                ProducersEvents.Add(collaboratorProducer);
            }
        }

        public string GetJobTitle()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string titlePt = JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            string titleEn = JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

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

        public string GetMiniBio()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string titlePt = MiniBios.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            string titleEn = MiniBios.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

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

        public string GetCompanyName()
        {
            if (Players != null && Players.Any())
            {
                return string.Join(", ", Players.Select(e => e.Name));
            }

            if (ProducersEvents != null && ProducersEvents.Any())
            {
                return string.Join(", ", ProducersEvents.Select(e => e.Producer.Name));
            }

            return null;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new CollaboratorIsConsistent().Valid(this));

            if (this.User != null)
            {
                ValidationResult.Add(new UserIsConsistent().Valid(this.User));
            }

            if (Image != null)
            {
                ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
                ValidationResult.Add(new CollaboratorImageIsConsistent().Valid(this));
            }

            return ValidationResult.IsValid;
        }
    }
}
