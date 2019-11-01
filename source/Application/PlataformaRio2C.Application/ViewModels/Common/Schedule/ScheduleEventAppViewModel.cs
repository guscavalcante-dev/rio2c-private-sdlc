using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ScheduleEventAppViewModel
    {
        public Guid Uid { get; set; }
        public string Type { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Genre { get; set; }

        public string StartHour { get; set; }
        public string EndHour { get; set; }

        public double Duration { get; set; }

        public string Room { get; set; }

        public int Table { get; set; }

        public PlayerOptionAppViewModel Player { get; set; }

        public ProducerOptionAppViewModel Producer { get; set; }

        public IEnumerable<LecturerAppViewModel> Lecturers { get; set; }
        public IEnumerable<LecturerGroupAppViewModel> LecturersGroup { get; set; }

        public ScheduleEventAppViewModel()
        {

        }

        public ScheduleEventAppViewModel(Negotiation entity)
        {
            Uid = entity.Project.Uid;
            Type = "meetings";
            StartHour = entity.StarTime.ToString("hh':'mm");
            EndHour = entity.EndTime.ToString("hh':'mm");

            Duration = (entity.EndTime - entity.StarTime).TotalMinutes;

            //Title = entity.Project.GetName();
            Room = entity.Room.GetName();
            Table = entity.TableNumber;

            Player = new PlayerOptionAppViewModel(entity.Player);
            //Producer = new ProducerOptionAppViewModel(entity.Project.Producer);
        }

        public ScheduleEventAppViewModel(Conference entity)
        {
            Uid = entity.Uid;
            Type = "conference";
            StartHour = entity.StartTime.Value.ToString("hh':'mm");
            EndHour = entity.EndTime.Value.ToString("hh':'mm");

            Duration = (entity.EndTime.Value - entity.StartTime.Value).TotalMinutes;

            Title = entity.GetTitle();
            Room = entity.Room.GetName();

            if (entity.Lecturers != null && entity.Lecturers.Any())
            {
                Lecturers = LecturerAppViewModel.MapList(entity.Lecturers);
                LecturersGroup = LecturerGroupAppViewModel.MapList(entity.Lecturers);
            }
        }


        public static IEnumerable<ScheduleEventAppViewModel> MapList(IEnumerable<Negotiation> entities)
        {
            foreach (var item in entities)
            {
                yield return new ScheduleEventAppViewModel(item);
            }
        }


        public static IEnumerable<ScheduleEventAppViewModel> MapList(IEnumerable<Conference> entities)
        {
            foreach (var item in entities)
            {
                yield return new ScheduleEventAppViewModel(item);
            }
        }

    }

    public class LecturerGroupAppViewModel
    {
        public string RoleLecturer { get; set; }

        public IEnumerable<LecturerAppViewModel> Lecturers { get; set; }

        public LecturerGroupAppViewModel(string roleLecturer, IEnumerable<ConferenceLecturer> lectures)
        {
            RoleLecturer = roleLecturer;
            Lecturers = LecturerAppViewModel.MapList(lectures);
        }       

        public static IEnumerable<LecturerGroupAppViewModel> MapList(IEnumerable<ConferenceLecturer> entities)
        {
            entities = entities.Where(e => e.RoleLecturer != null);
            
            if (entities.Any(o => o.RoleLecturer != null && o.RoleLecturer.Titles.Any(e => e.Value != null && e.Value.ToLower().Contains("ouvinte"))))
            {
                entities = entities.OrderBy(o => o.RoleLecturer.Titles.Any(e => e.Value != null && e.Value.ToLower().Contains("ouvinte")));
            }
           
            var entitiesGroup = entities.GroupBy(e => e.RoleLecturerId);

            foreach (var itemGroup in entitiesGroup)
            {
                yield return new LecturerGroupAppViewModel(itemGroup.FirstOrDefault().RoleLecturer.GetTitle(), itemGroup.ToList());
            }
        }
    }
}
