using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities
{
    public class Conference : Entity
    {
        public static readonly int LocalMinLength = 2;
        public static readonly int LocalMaxLength = 1000;
        public static readonly int InfoMaxLength = 3000;

        public DateTime? Date { get; private set; }
        public TimeSpan? StartTime { get; private set; }
        public TimeSpan? EndTime { get; private set; }
        public string Info { get; private set; }
        
        public virtual ICollection<ConferenceLecturer> Lecturers { get; private set; }
        public virtual ICollection<ConferenceTitle> Titles { get; private set; }
        public virtual ICollection<ConferenceSynopsis> Synopses { get; private set; }

        public int? RoomId { get; private set; }
        public virtual Room Room { get; private set; }

        protected Conference()
        {

        }

        public Conference(DateTime? date)
        {
            SetDate(date);            
        }     


        public void SetDate(DateTime? date)
        {
            Date = date;
        }

        public void SetStartTime(TimeSpan? val)
        {
            StartTime = val;
        }

        public void SetEndTime(TimeSpan? val)
        {
            EndTime = val;
        }      

        public void SetInfo(string info)
        {
            Info = info;
        }

        public void SetLecturers(IEnumerable<ConferenceLecturer> lecturers)
        {
            if (lecturers != null && lecturers.Any())
            {
                Lecturers = lecturers.ToList();
            }
            
        }

        public void SetTitles(IEnumerable<ConferenceTitle> titles)
        {            
            if (titles != null && titles.Any())
            {
                Titles = titles.ToList();
            }
        }

        public void SetSynopses(IEnumerable<ConferenceSynopsis> synopses)
        {            
            if (synopses != null && synopses.Any())
            {
                Synopses = synopses.ToList();
            }
        }

        public void SetRoom(Room room)
        {
            Room = room;

            if (room != null)
            {
                RoomId = room.Id;
            }
        }

        public string GetTitle()
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

        public string GetSinopsis()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string titlePt = Synopses.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            string titleEn = Synopses.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

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

            ValidationResult.Add(new ConferenceIsConsistent().Valid(this));

            return ValidationResult.IsValid;
        }
    }
}
