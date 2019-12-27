//using OfficeOpenXml;
//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.Interfaces.Services
//{
//    public interface IMessageAppService : IAppService<MessageChatAppViewModel, MessageChatAppViewModel, MessageChatAppViewModel, MessageChatAppViewModel>
//    {
//        AppValidationResult Send(int userId, ref MessageChatAppViewModel viewModel);

//        IEnumerable<MessageChatAppViewModel> GetAll(int userId, string email);

//        IEnumerable<MessageChatAppViewModel> GetUnreadsMessages(int userId);

//        AppValidationResult MarkMessageAsRead(int userId, Guid[] uid);

//        ExcelPackage DownloadNetwork(int userId);
//        ExcelPackage DownloadNetwork();
//    }
//}
