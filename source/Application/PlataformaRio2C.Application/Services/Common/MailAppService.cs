//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using PlataformaRio2C.Infra.Data.Context.Interfaces;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
//using PlataformaRio2C.Application.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PlataformaRio2C.Application.Interfaces.Services;
//using PlataformaRio2C.Application.Interfaces;
//using PlataformaRio2C.Domain.Services;

//namespace PlataformaRio2C.Application.Services
//{
//    public class MailAppService : AppService<Infra.Data.Context.PlataformaRio2CContext, Mail, MailAppViewModel, MailAppViewModel, MailCollaboratorEditAppViewModel, MailAppViewModel>, IMailAppService
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly IMailCollaboratorRepository _mailCollaboratorRepository;
//        private readonly IMailRepository _mailRepository;
//        private readonly ICollaboratorRepository _collaboratorRepository;
//        //private readonly ICollaboratorPlayerRepository _collaboratorPlayerRepository;

//        public MailAppService(IMailService service, IUnitOfWork unitOfWork,
//            IRepositoryFactory repositoryFactory,
//            IMailRepository mailRepository, IMailCollaboratorRepository mailCollaboratorRepository,
//            ICollaboratorRepository collaboratorRepository)
//            : base(unitOfWork, service)
//        {
//            _userRepository = repositoryFactory.UserRepository;
//            _mailCollaboratorRepository = mailCollaboratorRepository;
//            _mailRepository = mailRepository;
//            _collaboratorRepository = collaboratorRepository;
//        }

//        public IEnumerable<CollaboratorPlayerItemListAppViewModel> AllMailCollaborator(List<CollaboratorPlayerItemListAppViewModel> collaborators, string subject)
//        {
//            Mail mail = _mailRepository.Get(a => a.Subject == subject);
//            //collaborators = collaborators.ToList();
//            for (int i = 0; i < collaborators.Count(); i++)

//            {
//                var s = service as IMailCollaboratorRepository;
//                var collaborator = collaborators[i];
//                //var obj = _mailCollaboratorRepository.GetAll(a => a.Collaborator.Uid == collaborator.Uid /*&& a.Mail.Uid == mail.Uid*/).OrderByDescending(b => b.SendDate).Select(c=> c.SendDate);//var obj = _mailCollaboratorRepository.GetAll(a => a.Collaborator.Uid == collaborator.Uid /*&& a.Mail.Uid == mail.Uid*/).OrderByDescending(b => b.SendDate).Select(c=> c.SendDate);
//                var obj = _mailCollaboratorRepository.GetAll(a => a.Mail.Uid == mail.Uid);
//                DateTime? sendDate = null;
//                var vm = new MailCollaboratorListItemAppViewModel();

//                collaborators[i].SendDate = sendDate;
//            }

//            return collaborators;
//        }

//        public IEnumerable<MailAppViewModel> GetAll()
//        {
//            var s = service as IMailService;

//            var entities = s.GetAll();

//            if (entities != null && entities.Any())
//            {
//                return MailAppViewModel.MapList(entities);
//            }

//            return null;
//        }

//        public MailAppViewModel Get(int? mailId, string subject = null)
//        {
//            MailAppViewModel vm = null;
//            var s = service as IMailService;

//            var entities = (mailId != null) ? s.Get(a => a.Id == mailId) : s.Get(a => a.Subject == subject);

//            if (entities != null)
//            {
//                vm = new MailAppViewModel(entities);
//            }

//            return vm;
//        }

//        AppValidationResult IMailAppService.CreateMailCollaborator(MailCollaboratorAppViewModel viewModel)
//        {
//            BeginTransaction();
//            //var ColaboratorService = new ICollaboratorRepository();

//            Collaborator c = _collaboratorRepository.GetById(viewModel.CollaboratorId);
//            Mail m = viewModel.Mail;
//            DateTime sendDate = DateTime.Now;
//            MailCollaborator model = new MailCollaborator(m, c, sendDate);
//            var s = service as IMailService;

//            ValidationResult.Add(s.CreateMailCollaborator(model));

//            if (ValidationResult.IsValid)
//                Commit();

//            //viewModel = new MailCollaborator(model);

//            return ValidationResult;

//            //return _mailCollaboratorRepository.Create(viewModel);

//        }

//        MailAppViewModel IAppService<MailAppViewModel, MailAppViewModel, MailAppViewModel, MailAppViewModel>.GetByEdit(Guid uid)
//        {
//            throw new NotImplementedException();
//        }

//        MailAppViewModel IAppService<MailAppViewModel, MailAppViewModel, MailAppViewModel, MailAppViewModel>.GetEditViewModel()
//        {
//            throw new NotImplementedException();
//        }

//        public AppValidationResult Create(MailAppViewModel viewModel)
//        {
//            throw new NotImplementedException();
//        }

//        public AppValidationResult Update(MailAppViewModel viewModel)
//        {
//            throw new NotImplementedException();
//        }

//    }
//}