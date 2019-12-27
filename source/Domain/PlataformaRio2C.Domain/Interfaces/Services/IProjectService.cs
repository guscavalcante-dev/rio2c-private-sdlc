//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Validation;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Domain.Interfaces
//{
//    public interface IProjectService : IService<Project>
//    {
//        IEnumerable<Project> GetAllByAdmin();
//        IEnumerable<Project> GetAllExcel();
//        IEnumerable<Project> GetDataExcel();
//        ValidationResult UpdateByAdmin(Project entity);
//        ValidationResult SavePlayerSelection(Project entity, Guid uidPlayer, int userId);
//        ValidationResult RemovePlayerSelection(Project entity, Guid uidPlayer, int userId);
//        ValidationResult SendToPlayers(Project entity, Guid[] uidsPlayers, int userId);
//        ValidationResult AcceptByPlayer(Project entity, Guid uidPlayer, int userId);
//        ValidationResult RejectByPlayer(Project entity, Guid uidPlayer, int userId, string reason);
//        bool ExceededProjectMaximumPerProducer(int userId);
//        Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter);
//        Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter);

//        IEnumerable<Project> GetAllOption(Expression<Func<Project, bool>> filter);

//        Project GetWithPlayerSelection(Guid uid);

//        int CountUnsent();
//        int CountSent();

//    }
//}
