//using OfficeOpenXml;
//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.Interfaces.Services
//{
//    public interface INegotiationAppService : IAppService<NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel, NegotiationAppViewModel>
//    {
//        AppValidationResult GenerateScheduleOneToOneMeetings(int userId);
//        AppValidationResult ProcessScheduleOneToOneMeetings(int userId);
//        NegotiationResultProcessAppViewModel ResultProcessScheduleOneToOneMeetings(int userId);
//        GroupDateNegotiationAppViewModel GetNegotiations(int userId);
//        IEnumerable<NegotiationAppViewModel> GetAllNegotiations();
//        GroupDateTableNegotiationAppViewModel GetAllNegotiationsGroupTable();
//        IEnumerable<NegotiationAppViewModel> GetTempNegociations(int userId);
//        GroupDateNegotiationAppViewModel GetGroupTempNegotiations(int userId);
//        AppValidationResult GenerateTemp(int userId);
//        AppValidationResult Delete(int userId, Guid uid);
//        IEnumerable<string> GetOptionsDates();
//        IEnumerable<RoomAppViewModel> GetOptionsRoomsByDate(string date);
//        IEnumerable<string> GetOptionsStartTime(string date);
//        AppValidationResult RegisterNegotiationManual(ManualNegotiationRegisterAppViewModel viewModel);
//        IEnumerable<int> GetOptionsTables(string date, string startTime, Guid room);
//        IEnumerable<PlayerOptionAppViewModel> GetPlayers();
//        IEnumerable<ProducerOptionAppViewModel> GetProducers();

//        AppValidationResult SendEmailToPlayers(Guid[] uidsPlayers);
//        AppValidationResult SendEmailToProducers(Guid[] uidsProducers);


//        ExcelPackage ExportExcel(string date, string roomName);
//    }
//}
