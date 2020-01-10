//using System;
//using System.Collections.Generic;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.ViewModels;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using PlataformaRio2C.Infra.Data.Context.Interfaces;
//using System.Linq.Expressions;
//using LinqKit;
//using PlataformaRio2C.Domain.Entities;
//using System.Linq;
//using OfficeOpenXml;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using OfficeOpenXml.Style;
//using System.Drawing;
//using System.Globalization;
//using System.Threading;
//using PlataformaRio2C.Infra.CrossCutting.SystemParameter;

//namespace PlataformaRio2C.Application.Services
//{
//    public class MessageAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Message, MessageChatAppViewModel, MessageChatAppViewModel, MessageChatAppViewModel, MessageChatAppViewModel>, IMessageAppService
//    {
//        private readonly ICollaboratorRepository _collaboratorRepository;
//        private readonly IUserRepository _userRepository;
//        private readonly ICollaboratorService _collaboratorService;

//        public MessageAppService(IMessageService service, IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory, ICollaboratorService collaboratorService)
//            : base(unitOfWork, service)
//        {
//            _collaboratorRepository = repositoryFactory.CollaboratorRepository;
//            _userRepository = repositoryFactory.UserRepository;
//            _collaboratorService = collaboratorService;
//        }

//        public IEnumerable<MessageChatAppViewModel> GetAll(int userId, string email)
//        {
//            var s = service as IMessageService;

//            var entities = s.GetAll(userId, email);

//            if (entities != null && entities.Any())
//            {
//                return MessageChatAppViewModel.MapList(entities);
//            }

//            return null;
//        }

//        public IEnumerable<MessageChatAppViewModel> GetUnreadsMessages(int userId)
//        {
//            var s = service as IMessageService;

//            var entities = s.GetAllUnread(userId);

//            if (entities != null && entities.Any())
//            {
//                return MessageChatAppViewModel.MapList(entities);
//            }

//            return null;
//        }

//        public AppValidationResult MarkMessageAsRead(int userId, Guid[] uids)
//        {
//            BeginTransaction();

//            var entities = service.GetAll(e => uids.Contains(e.Uid));

//            if (entities != null && entities.Any())
//            {
//                var entitiesAlter = new List<Message>();

//                foreach (var entity in entities)
//                {
//                    if (!entity.ReadDate.HasValue)
//                    {
//                        entity.SetIsRead(true);
//                        entitiesAlter.Add(entity);
//                    }
//                }

//                ValidationResult.Add(service.UpdateAll(entitiesAlter));
//            }

//            if (ValidationResult.IsValid)
//                Commit();

//            return ValidationResult;
//        }



//        public AppValidationResult Send(int userId, ref MessageChatAppViewModel viewModel)
//        {
//            BeginTransaction();

//            var s = service as IMessageService;

//            var userSender = _userRepository.Get(e => e.Id == userId);
//            viewModel.SenderEmail = userSender.Email;

//            var recipientEmail = viewModel.RecipientEmail;
//            var userRecipient = _userRepository.Get(e => e.Email == recipientEmail);

//            var entity = new Message(viewModel.Text);
//            entity.SetRecipient(userRecipient);
//            entity.SetSender(userSender);

//            ValidationResult.Add(s.Create(entity));

//            if (ValidationResult.IsValid)
//                Commit();

//            viewModel = new MessageChatAppViewModel(entity);

//            return ValidationResult;
//        }


//        private Expression<Func<Collaborator, bool>> GetPredicateCollaboratorsPlayers(int userId, CollaboratorOptionMessageAppViewModel filter)
//        {
//            var predicate = PredicateBuilder.New<Collaborator>(true);

//            var predicateUser = PredicateBuilder.New<Collaborator>(false);

//            predicateUser = predicateUser.Or(c => c.Id != userId);

//            predicate = PredicateBuilder.And<Collaborator>(predicate, predicateUser);

//            if (filter != null)
//            {
//                if (!string.IsNullOrWhiteSpace(filter.Name))
//                {
//                    var predicateName = PredicateBuilder.New<Collaborator>(false);

//                    predicateName = predicateName.Or(c => c.FirstName.ToLower().Contains(filter.Name));

//                    predicate = PredicateBuilder.And<Collaborator>(predicate, predicateName);
//                }
//            }

//            return predicate;
//        }

//        private Expression<Func<Collaborator, bool>> GetPredicateCollaboratorsProducers(int userId, CollaboratorOptionMessageAppViewModel filter)
//        {
//            var predicate = PredicateBuilder.New<Collaborator>(true);

//            var predicateUser = PredicateBuilder.New<Collaborator>(false);

//            predicateUser = predicateUser.Or(c => c.Id != userId);

//            predicate = PredicateBuilder.And<Collaborator>(predicate, predicateUser);

//            if (filter != null)
//            {
//                if (!string.IsNullOrWhiteSpace(filter.Name))
//                {
//                    var predicateName = PredicateBuilder.New<Collaborator>(false);

//                    predicateName = predicateName.Or(c => c.FirstName.ToLower().Contains(filter.Name));

//                    predicate = PredicateBuilder.And<Collaborator>(predicate, predicateName);
//                }
//            }

//            return predicate;
//        }


//        public ExcelPackage DownloadNetwork(int userId)
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            ExcelPackage excelFile = new ExcelPackage();

//            ExcelWorksheet worksheetPlayers = excelFile.Workbook.Worksheets.Add(Labels.Players);

//            //var emailsHidden = _systemParameterRepository.Get<string>(SystemParameterCodes.NetworkRio2CEmailsThatShouldBeHidden);

//            var collaborators = _collaboratorService.GetOptionsChat(userId);

//            int row = 1;
//            int column = 1;

//            // Config reader row
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
//            worksheetPlayers.Cells[row, column, row, column + 4].Style.Font.Bold = true;

//            worksheetPlayers.Column(1).Width = 50; //Empresa
//            worksheetPlayers.Column(2).Width = 30; //Cargo
//            worksheetPlayers.Column(3).Width = 40; //Nome
//            //worksheetPlayers.Column(4).Width = 40; //Email


//            worksheetPlayers.Cells[row, column++].Value = Labels.Company;
//            worksheetPlayers.Cells[row, column++].Value = Labels.JobTitle;
//            worksheetPlayers.Cells[row, column++].Value = Labels.Name;
//            //worksheetPlayers.Cells[row, column++].Value = Labels.Email;

//            row++;

//            //foreach (var collaborator in collaborators.Where(e => e.Players != null && e.Players.Any()))
//            //{
//            //    column = 1;

//            //    worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.Players.Select(e => e.Name));

//            //    if (currentCulture != null && currentCulture.Name == "pt-BR")
//            //    {
//            //        worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value));
//            //    }
//            //    else
//            //    {
//            //        worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value));
//            //    }

//            //    worksheetPlayers.Cells[row, column++].Value = collaborator.Name;

//            //    //if (emailsHidden.Contains(collaborator.User.Email))
//            //    //{
//            //    //    worksheetPlayers.Cells[row, column++].Value = " - ";
//            //    //}
//            //    //else
//            //    //{
//            //    //    worksheetPlayers.Cells[row, column++].Value = collaborator.User.Email;
//            //    //}

//            //    row++;
//            //}


//            //ExcelWorksheet worksheetProducers = excelFile.Workbook.Worksheets.Add(Labels.Producers);


//            //row = 1;
//            //column = 1;

//            //// Config reader row
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
//            //worksheetProducers.Cells[row, column, row, column + 4].Style.Font.Bold = true;

//            //worksheetProducers.Column(1).Width = 50; //Empresa
//            //worksheetProducers.Column(2).Width = 30; //Cargo
//            //worksheetProducers.Column(3).Width = 40; //Nome
//            ////worksheetProducers.Column(4).Width = 40; //Email


//            //worksheetProducers.Cells[row, column++].Value = Labels.Company;
//            //worksheetProducers.Cells[row, column++].Value = Labels.JobTitle;
//            //worksheetProducers.Cells[row, column++].Value = Labels.Name;
//            ////worksheetProducers.Cells[row, column++].Value = Labels.Email;

//            //row++;

//            //foreach (var collaborator in collaborators.Where(e => e.ProducersEvents != null && e.ProducersEvents.Any()))
//            //{
//            //    column = 1;

//            //    worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.ProducersEvents.Select(e => e.Producer.Name));

//            //    if (currentCulture != null && currentCulture.Name == "pt-BR")
//            //    {
//            //        worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value));
//            //    }
//            //    else
//            //    {
//            //        worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value));
//            //    }

//            //    worksheetProducers.Cells[row, column++].Value = collaborator.Name;

//            //    //if (emailsHidden.Contains(collaborator.User.Email))
//            //    //{
//            //    //    worksheetProducers.Cells[row, column++].Value = " - ";
//            //    //}
//            //    //else
//            //    //{
//            //    //    worksheetProducers.Cells[row, column++].Value = collaborator.User.Email;
//            //    //}

//            //    row++;
//            //}


//            return excelFile;
//        }

//        public ExcelPackage DownloadNetwork()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            ExcelPackage excelFile = new ExcelPackage();

//            ExcelWorksheet worksheetPlayers = excelFile.Workbook.Worksheets.Add(Labels.Players);

//            //var emailsHidden = _systemParameterRepository.Get<string>(SystemParameterCodes.NetworkRio2CEmailsThatShouldBeHidden);

//            var collaborators = _collaboratorService.GetOptionsChat(0);

//            int row = 1;
//            int column = 1;

//            // Config reader row
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
//            worksheetPlayers.Cells[row, column, row, column + 7].Style.Font.Bold = true;

//            worksheetPlayers.Column(1).Width = 50; //Empresa
//            worksheetPlayers.Column(2).Width = 30; //Cargo
//            worksheetPlayers.Column(3).Width = 40; //Nome
//            worksheetPlayers.Column(4).Width = 40; //Email
//            worksheetPlayers.Column(5).Width = 40; //
//            worksheetPlayers.Column(6).Width = 40; //
//            worksheetPlayers.Column(7).Width = 40; //


//            worksheetPlayers.Cells[row, column++].Value = Labels.Company;
//            worksheetPlayers.Cells[row, column++].Value = Labels.JobTitle;
//            worksheetPlayers.Cells[row, column++].Value = Labels.Name;
//            worksheetPlayers.Cells[row, column++].Value = Labels.Email;
//            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Company, Labels.PhoneNumber);
//            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "Executivo", Labels.PhoneNumber);
//            worksheetPlayers.Cells[row, column++].Value = string.Format("{0} {1}", "Executivo", Labels.CellPhone);

//            row++;

//            //foreach (var collaborator in collaborators.Where(e => e.Players != null && e.Players.Any()))
//            //{
//            //    column = 1;

//            //    worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.Players.Select(e => e.Name));

//            //    if (currentCulture != null && currentCulture.Name == "pt-BR")
//            //    {
//            //        worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value));
//            //    }
//            //    else
//            //    {
//            //        worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value));
//            //    }

//            //    worksheetPlayers.Cells[row, column++].Value = collaborator.Name;

//            //    worksheetPlayers.Cells[row, column++].Value = collaborator.User.Email;

//            //    worksheetPlayers.Cells[row, column++].Value = string.Join(", ", collaborator.Players.Select(e => e.PhoneNumber));

//            //    worksheetPlayers.Cells[row, column++].Value = collaborator.PhoneNumber;

//            //    worksheetPlayers.Cells[row, column++].Value = collaborator.CellPhone;

//            //    row++;
//            //}

//            /*Aba de Produtoras*/
//            //ExcelWorksheet worksheetProducers = excelFile.Workbook.Worksheets.Add(Labels.Producers);


//            //row = 1;
//            //column = 1;

//            //// Config reader row
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
//            //worksheetProducers.Cells[row, column, row, column + 7].Style.Font.Bold = true;

//            //worksheetProducers.Column(1).Width = 50; //Empresa
//            //worksheetProducers.Column(2).Width = 30; //Cargo
//            //worksheetProducers.Column(3).Width = 40; //Nome
//            //worksheetProducers.Column(4).Width = 40; //Email
//            //worksheetProducers.Column(5).Width = 40; //
//            //worksheetProducers.Column(6).Width = 40; //
//            //worksheetProducers.Column(7).Width = 40; //


//            //worksheetProducers.Cells[row, column++].Value = Labels.Company;
//            //worksheetProducers.Cells[row, column++].Value = Labels.JobTitle;
//            //worksheetProducers.Cells[row, column++].Value = Labels.Name;
//            //worksheetProducers.Cells[row, column++].Value = Labels.Email;
//            //worksheetProducers.Cells[row, column++].Value = string.Format("{0} {1}", Labels.Company, Labels.PhoneNumber);
//            //worksheetProducers.Cells[row, column++].Value = string.Format("{0} {1}", "Executivo", Labels.PhoneNumber);
//            //worksheetProducers.Cells[row, column++].Value = string.Format("{0} {1}", "Executivo", Labels.CellPhone);

//            //row++;

//            //foreach (var collaborator in collaborators.Where(e => e.ProducersEvents != null && e.ProducersEvents.Any()))
//            //{
//            //    column = 1;

//            //    worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.ProducersEvents.Select(e => e.Producer.Name));

//            //    if (currentCulture != null && currentCulture.Name == "pt-BR")
//            //    {
//            //        worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value));
//            //    }
//            //    else
//            //    {
//            //        worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value));
//            //    }

//            //    worksheetProducers.Cells[row, column++].Value = collaborator.Name;


//            //    worksheetProducers.Cells[row, column++].Value = collaborator.User.Email;

//            //    worksheetProducers.Cells[row, column++].Value = string.Join(", ", collaborator.Players.Select(e => e.PhoneNumber));

//            //    worksheetProducers.Cells[row, column++].Value = collaborator.PhoneNumber;

//            //    worksheetProducers.Cells[row, column++].Value = collaborator.CellPhone;

//            //    row++;
//            //}


//            return excelFile;
//        }
//    }
//}
