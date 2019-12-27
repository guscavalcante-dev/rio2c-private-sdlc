//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.Interfaces.Services
//{
//    public interface IPlayerAppService : IAppService<PlayerBasicAppViewModel, PlayerDetailAppViewModel, PlayerEditAppViewModel, PlayerItemListAppViewModel>
//    {
//        AppValidationResult UpdateByPortal(PlayerEditAppViewModel viewModel);

//        IEnumerable<PlayerAppViewModel> GetAllByUserId(int id);       

//        PlayerAppViewModel GetAllByUserId(int id, Guid playerUid);

//        IEnumerable<PlayerEditAppViewModel> GetAllEditByUserId(int id);

//        PlayerEditAppViewModel GetAllEditByUserId(int id, Guid playerUid);

//        IEnumerable<PlayerDetailAppViewModel> GetAllDetailByUserId(int id);

//        PlayerDetailAppViewModel GetAllDetailByUserId(int id, Guid playerUid);

//        AppValidationResult SaveInterests(PlayerAppViewModel player);

//        IEnumerable<PlayerSelectOptionAppViewModel> GetAllOption(PlayerSelectOptionFilterAppViewModel filter, int userId);

//        IEnumerable<GroupPlayerSelectOptionAppViewModel> GetAllOptionGroupByHolding(PlayerSelectOptionFilterAppViewModel filter, int userId);

//        IEnumerable<PlayerSelectOptionAppViewModel> GetAllOptionByUser(PlayerSelectOptionFilterAppViewModel filter, int userId);

//        IEnumerable<PlayerProducerAreaAppViewModel> GetAllWithGenres(PlayerSelectOptionFilterAppViewModel filter, int userId);

//        IEnumerable<GroupPlayerAppViewModel> GetAllWithGenresGroupByHolding(PlayerSelectOptionFilterAppViewModel filter, int userId);

//        ImageFileAppViewModel GetThumbImage(Guid playerUid);
//        ImageFileAppViewModel GetImage(Guid uid);
//        IEnumerable<PlayerSelectOptionAppViewModel> GetAllPlayersWithImages();

//        PlayerEditInterstsAppViewModel GetEditIntersts(Guid playerUid);

//        AppValidationResult UpdateEditIntersts(PlayerEditInterstsAppViewModel playerinterestViewModel);


//        PlayerDetailWithInterestAppViewModel GetByDetailsWithInterests(Guid playerUid);

//        IEnumerable<object> GetAllApi();
//    }
//}
