//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels.Api
//{   
//    public class LecturerGroupAppViewModel
//    {
//        public string RoleLecturer { get; set; }

//        public IEnumerable<LecturerAppViewModel> Lecturers { get; set; }

//        public LecturerGroupAppViewModel(string roleLecturer, IEnumerable<ConferenceLecturer> lectures)
//        {
//            RoleLecturer = roleLecturer;
//            Lecturers = LecturerAppViewModel.MapList(lectures);
//        }

//        public static IEnumerable<LecturerGroupAppViewModel> MapList(IEnumerable<ConferenceLecturer> entities)
//        {
//            entities = entities.Where(e => e.RoleLecturer != null);

//            if (entities.Any(o => o.RoleLecturer != null && o.RoleLecturer.Titles.Any(e => e.Value != null && e.Value.ToLower().Contains("ouvinte"))))
//            {
//                entities = entities.OrderBy(o => o.RoleLecturer.Titles.Any(e => e.Value != null && e.Value.ToLower().Contains("ouvinte")));
//            }

//            var entitiesGroup = entities.GroupBy(e => e.RoleLecturerId);

//            foreach (var itemGroup in entitiesGroup)
//            {
//                yield return new LecturerGroupAppViewModel(itemGroup.FirstOrDefault().RoleLecturer.GetTitle(), itemGroup.ToList());
//            }
//        }
//    }
//}
