using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels
{
    public class RoomEditAppViewModel : RoomAppViewModel
    {
        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }


        public RoomEditAppViewModel()
            :base()
        {

        }

        public RoomEditAppViewModel(Domain.Entities.Room entity)
            :base(entity)
        {
           
        }        
    }
}
