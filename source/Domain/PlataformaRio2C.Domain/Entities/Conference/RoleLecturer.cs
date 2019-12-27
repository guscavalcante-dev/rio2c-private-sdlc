//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities
//{
//    public class RoleLecturer : Entity
//    {
//        public virtual ICollection<RoleLecturerTitle> Titles { get; private set; }

//        protected RoleLecturer()
//        {

//        }

//        public RoleLecturer(IEnumerable<RoleLecturerTitle> titles)
//        {
//            SetTitles(titles);
//        }

//        public void SetTitles(IEnumerable<RoleLecturerTitle> titles)
//        {
//            if (titles != null)
//            {
//                Titles = titles.ToList();
//            }
            
//        }

//        public string GetTitle()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            string titlePt = Titles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
//            string titleEn = Titles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

//            if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
//            {
//                return titlePt;
//            }
//            else if (!string.IsNullOrWhiteSpace(titleEn))
//            {
//                return titleEn;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
