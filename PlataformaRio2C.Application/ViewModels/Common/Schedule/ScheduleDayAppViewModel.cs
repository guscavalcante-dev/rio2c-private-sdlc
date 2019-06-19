using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ScheduleDayAppViewModel
    {
        public string Date { get; set; }

        public string DayOfWeek { get; set; }

        public IEnumerable<ScheduleEventAppViewModel> Items { get; set; }

        public ScheduleDayAppViewModel()
        {

        }

        public ScheduleDayAppViewModel(DateTime date)
        {
            Date = date.ToString("dd/MM/yyyy");

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            var dayOfWeek = currentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
            DayOfWeek = dayOfWeek;
        }

        public ScheduleDayAppViewModel(DateTime date, IEnumerable<Negotiation> negotiations)
        {
            Date = date.ToString("dd/MM/yyyy");

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            var dayOfWeek = currentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
            DayOfWeek = dayOfWeek;

            Items = ScheduleEventAppViewModel.MapList(negotiations);

        }

        public ScheduleDayAppViewModel(DateTime date, IEnumerable<Conference> conferences)
        {
            Date = date.ToString("dd/MM/yyyy");

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            var dayOfWeek = currentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
            DayOfWeek = dayOfWeek;

            Items = ScheduleEventAppViewModel.MapList(conferences);

        }

        public ScheduleDayAppViewModel(IEnumerable<Conference> conferences)
        {

        }  


        public static IEnumerable<ScheduleDayAppViewModel> GetByNegotiations(IEnumerable<Negotiation> negotiations)
        {
            var negotiationsGroupDate = negotiations.GroupBy(e => e.Date);

            foreach (var item in negotiationsGroupDate)
            {
                yield return new ScheduleDayAppViewModel(item.Key.Value, item.ToList());
            }
        }

        public static IEnumerable<ScheduleDayAppViewModel> GetByConference(IEnumerable<Conference> conferences)
        {
            var conferencesGroupDate = conferences.GroupBy(e => e.Date);

            foreach (var item in conferencesGroupDate)
            {
                yield return new ScheduleDayAppViewModel(item.Key.Value, item.ToList());
            }
        }           
    }
}
