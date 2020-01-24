//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class PlayerProjectStatusAppViewModel
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string TradeName { get; set; }
//        public string Cnpj { get; set; }
//        public string Status { get; set; }
//        public string StatusCode { get; set; }
//        public Guid Uid { get; set; }
//        public bool HasImage { get; set; }
//        public string Reason { get; set; }

//        public PlayerProjectStatusAppViewModel()
//        {

//        }

//        //public PlayerProjectStatusAppViewModel(ProjectPlayer entity)
//        //{
//        //    Id = entity.Player.Id;
//        //    Uid = entity.Player.Uid;
//        //    Name = entity.Player.Name;
//        //    TradeName = entity.Player.TradeName;
//        //    Cnpj = entity.Player.CNPJ;
//        //    if (entity.EvaluationId > 0 && entity.Evaluation != null && entity.Evaluation.StatusId > 0 && entity.Evaluation.Status != null)
//        //    {
//        //        StatusCode = entity.Evaluation.Status.Code;
//        //        Status = Labels.ResourceManager.GetString(StatusCode);
//        //        Reason = entity.Evaluation.Reason;
//        //    }
//        //    else
//        //    {
//        //        StatusCode = StatusProjectCodes.OnEvaluation.ToString();
//        //        Status = Labels.ResourceManager.GetString(StatusProjectCodes.OnEvaluation.ToString());
//        //    }

//        //    HasImage = entity.Player.ImageId > 0;
//        //}

//        //public static IEnumerable<PlayerProjectStatusAppViewModel> MapList(IEnumerable<ProjectPlayer> entities)
//        //{
//        //    foreach (var entity in entities)
//        //    {
//        //        yield return new PlayerProjectStatusAppViewModel(entity);
//        //    }
//        //}
//    }
//}
