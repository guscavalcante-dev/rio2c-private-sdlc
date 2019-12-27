//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class RoleLecturerAppViewModel : EntityViewModel<RoleLecturerAppViewModel, RoleLecturer>, IEntityViewModel<Domain.Entities.RoleLecturer>
//    {
//        public IEnumerable<RoleLecturerTitleAppViewModel> Titles { get; set; }
//        public string Name { get; set; }

//        public RoleLecturerAppViewModel()
//            :base()
//        {

//        }

//        public RoleLecturerAppViewModel(Domain.Entities.RoleLecturer entity)
//            :base(entity)
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            Name = "";

//            if (entity.Titles != null && entity.Titles.Any())
//            {
//                Titles = RoleLecturerTitleAppViewModel.MapList(entity.Titles);

//                if (currentCulture != null && currentCulture.Name == "pt-BR")
//                {
//                    Name = Titles.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value).FirstOrDefault();
//                }
//                else
//                {
//                    Name = Titles.Where(e => e.LanguageCode == "En").Select(e => e.Value).FirstOrDefault();
//                }
//            }            
//        }

//        public RoleLecturer MapReverse()
//        {
//            return new Domain.Entities.RoleLecturer(null);
//        }

//        public RoleLecturer MapReverse(RoleLecturer entity)
//        {
//            return entity;
//        }
//    }
//}
