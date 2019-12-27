//using OfficeOpenXml;
//using PlataformaRio2C.Application.Dtos;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;

//namespace PlataformaRio2C.Application.Interfaces.Services
//{
//    public interface IProjectAppService : IAppService<ProjectBasicAppViewModel, ProjectDetailAppViewModel, ProjectEditAppViewModel, ProjectItemListAppViewModel>
//    {
//        IEnumerable<ProjectAdminItemListAppViewModel> AllByAdmin();
//        IEnumerable<ProjectExcelItemListAppViewModel> GetAllExcel();
//        void MapEntity(ref Project entity, ProjectEditAppViewModel project);
//        IEnumerable<ProjectItemListAppViewModel> GetAllByUserProducerId(int id);
//        //IEnumerable<ProjectPlayerItemListAppViewModel> GetAllByUserPlayerId(ProjectPlayerFilterAppDto filter, int id);
//        ViewModels.Admin.ProjectDetailAppViewModel GetPlayerSelectionByUidProject(Guid uid);
//        AppValidationResult Create(ProjectEditAppViewModel viewModel, int userId);
//        AppValidationResult Update(ProjectEditAppViewModel viewModel, int userId);
//        ProjectEditAppViewModel GetEditViewModelProducerProject(int userId);
//        ProjectEditAppViewModel GetEditByUserId(int id);
//        ProjectEditAppViewModel GetByEdit(Guid projectUid, int userId);
//        ProjectDetailAppViewModel GetByDetails(Guid projectUid, int userId);
//        AppValidationResult SavePlayerSelection(Guid playerUid, Guid uidProject, int userId);
//        AppValidationResult RemovePlayerSelection(Guid playerUid, Guid uidProject, int userId);
//        AppValidationResult SendToPlayers(Guid playerUid, Guid[] uidsPlayers, int userId);
//        bool ProjectSendToPlayer(Guid uidProject, int userId);
//        ICollection<SelectListItem> GetStatusOption();
//        ProjectPlayerDetailAppViewModel GetForEvaluationPlayer(int userId, Guid uidProject);
//        AppValidationResult AcceptByPlayer(Guid playerUid, Guid uidProject, int userId);
//        AppValidationResult RejectByPlayer(Guid playerUid, Guid uidProject, int userId, string reason);
//        AppValidationResult DeleteProjectPlayer(int id);
//        AppValidationResult ResetEvaluation(int id);

//        IEnumerable<ProjectOptionAppViewModel> GetAllOption(ProjectOptionFilterAppViewModel filter);

//        bool ExceededProjectMaximumPerProducer(int userId);

//        bool PreRegistrationProducerDisabled();
//        bool RegistrationDisabled();
//        bool SendToPlayersDisabled();

//        ExcelPackage DownloadExcelProject();

//        ExcelPackage DownloadExcel();
//        ExcelPackage DownloadExcelProjectPitching();
//    }
//}
